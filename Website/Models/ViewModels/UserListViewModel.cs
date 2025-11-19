namespace Website.Models.ViewModels
{
    public class UserListViewModel
    {
        public int IdUser { get; set; }
        public string NameUser { get; set; }
        public string EmailUser { get; set; }

        public string RoleName { get; set; }      
        public string RoleSpecificId { get; set; } 
    }
}