using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Website.Models;     
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

        public AccountController(UserService userService)
        {
            _userService = userService;
        }

      

        [Route("")]
        [Route("Index")]
        public async Task<IActionResult> Account(string search)
        {
            search ??= "";
            var users = await _userService.SearchUser(search);
            ViewData["CurrentSearch"] = search;
            return View(users);
        }

        // GET: /Admin/Account/Create
        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Admin/Account/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
       
        public async Task<IActionResult> Create(UserModel user)
        {
            if (ModelState.IsValid)
            {
                await _userService.AddUser(user);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
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