namespace Project.MVCUI.Areas.Admin.AdminViewModels
{
    public class UserViewModel
    {
        public string? Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public IList<string>? Roles { get; set; }
    }
}
