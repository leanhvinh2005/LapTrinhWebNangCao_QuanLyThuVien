using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Website.Models;
using Website.Models.ViewModels; 
using Website.Services;
using System.Threading.Tasks;
using UserModel = Website.Models.User;


namespace Website.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Route("Admin/Account")]
    public class AccountController : Controller
    {
        private readonly UserService _userService;
        private readonly MemberService _memberService;
        private readonly LibrarianService _librarianService;
        private readonly CardService _cardService; 

        
        public AccountController(
            UserService userService,
            MemberService memberService,
            LibrarianService librarianService,
            CardService cardService)
        {
            _userService = userService;
            _memberService = memberService;
            _librarianService = librarianService;
            _cardService = cardService;
        }

        
        [Route("")]
        [Route("Index")]
        public async Task<IActionResult> Account(string search)
        {
            search ??= "";
            
            var users = await _userService.SearchUser(search);
            var userListVM = new List<UserListViewModel>();
            var allMembers = await _memberService.SearchMember("");
            var allLibrarians = await _librarianService.SearchLibrarian("");

            foreach (var user in users)
            {
                var vm = new UserListViewModel
                {
                    IdUser = user.idUser,
                    NameUser = user.nameUser,
                    EmailUser = user.emailUser,
                    RoleName = "Chưa phân quyền", // Mặc định
                    RoleSpecificId = "---"
                };

                // Kiểm tra xem User này có phải là Member không?
                var member = allMembers.FirstOrDefault(m => m.idUser == user.idUser);
                if (member != null)
                {
                    vm.RoleName = "Độc giả (Member)";
                    vm.RoleSpecificId = "Mã thẻ: " + member.idCard; // Hiển thị Mã thẻ
                }
                // Kiểm tra xem User này có phải là Librarian không?
                else
                {
                    var lib = allLibrarians.FirstOrDefault(l => l.idUser == user.idUser);
                    if (lib != null)
                    {
                        vm.RoleName = "Thủ thư (Librarian)";
                        vm.RoleSpecificId = "ID Thủ thư: " + lib.idLibrarian; // Hiển thị ID Thủ thư
                    }
                    // Nếu là Admin (thường check qua email hoặc bảng riêng, ở đây ví dụ check cứng)
                    else if (user.emailUser.ToLower().Contains("admin"))
                    {
                        vm.RoleName = "Quản trị viên (Admin)";
                        vm.RoleSpecificId = "Toàn quyền";
                    }
                }

                userListVM.Add(vm);
            }
            ViewData["CurrentSearch"] = search;
            return View(userListVM);
        }

        // GET: /Admin/Account/Create
        [Route("Create")]
        public IActionResult Create()
        {
           
            return View(new UserCreationViewModel());
        }

        // POST: /Admin/Account/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<IActionResult> Create(UserCreationViewModel model)
        {
            if (ModelState.IsValid)
            {
                // --- LOGIC KIỂM TRA DỮ LIỆU RIÊNG BIỆT ---

                // TRƯỜNG HỢP 1: LÀ ĐỘC GIẢ (MEMBER)
                if (model.Role == "Member")
                {
                    // a. Kiểm tra nhập mã thẻ chưa?
                    if (string.IsNullOrEmpty(model.LibraryCardId))
                    {
                        ModelState.AddModelError("LibraryCardId", "Vui lòng nhập Mã thẻ cho Độc giả");
                        return View(model);
                    }
                    // b. Kiểm tra thẻ có tồn tại trong kho thẻ không?
                    var card = await _cardService.GetCardByIdAsync(model.LibraryCardId);
                    if (card == null)
                    {
                        ModelState.AddModelError("LibraryCardId", "Mã thẻ không tồn tại trong hệ thống");
                        return View(model);
                    }
                    // c. Kiểm tra thẻ này đã có ai dùng chưa?
                    var linkedUsers = await _memberService.SearchMember(model.LibraryCardId);
                    if (linkedUsers.Count > 0)
                    {
                        ModelState.AddModelError("LibraryCardId", "Thẻ này đã được liên kết với tài khoản khác");
                        return View(model);
                    }
                }
                // TRƯỜNG HỢP 2: LÀ THỦ THƯ (LIBRARIAN)
                else if (model.Role == "Librarian")
                {
                    if (string.IsNullOrEmpty(model.JobTitle))
                    {
                        ModelState.AddModelError("JobTitle", "Vui lòng nhập Chức vụ cho Thủ thư");
                        return View(model);
                    }
                }

                // --- BẮT ĐẦU LƯU DỮ LIỆU ---

                // Bước 1: Tạo User (Account) trước
                var newUser = new UserModel
                {
                    emailUser = model.Email,
                    passwordUser = model.Password, // Lưu ý: Nên mã hóa mật khẩu ở đây
                    nameUser = model.FullName
                };

                int newUserId = await _userService.AddUser(newUser);

                // Bước 2: Tạo Role tương ứng
                if (model.Role == "Member")
                {
                    var newMember = new Member
                    {
                        idCard = model.LibraryCardId,
                        idUser = newUserId,
                        statusMember = "ACTIVE"
                    };
                    await _memberService.AddMember(newMember);
                }
                else if (model.Role == "Librarian")
                {
                    var newLibrarian = new Librarian
                    {
                        roleLibrarian = model.JobTitle,
                        hireLibrarian = DateOnly.FromDateTime(model.HireDate ?? DateTime.Now),
                        statusLibrarian = "ACTIVE",
                        idUser = newUserId
                    };
                    await _librarianService.AddLibrarian(newLibrarian);
                }

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: /Admin/Account/Edit/5
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: /Admin/Account/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit/{id}")]

        public async Task<IActionResult> Edit(int id, UserModel user)
        {
            if (id != user.idUser)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _userService.EditUser(user);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: /Admin/Account/Delete/5
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: /Admin/Account/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _userService.DeleteUser(id);
            return RedirectToAction(nameof(Index));
        }
    }
}