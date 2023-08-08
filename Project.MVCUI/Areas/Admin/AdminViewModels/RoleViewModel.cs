using System.ComponentModel.DataAnnotations;

namespace Project.MVCUI.Areas.Admin.AdminViewModels
{
    public class RoleViewModel
    {
        public string? Id { get; set; }

        [Display(Name ="Role Adı")]
        [Required(ErrorMessage ="{0} boş bırakılamaz")]
        public string Name { get; set; } = null!;
    }
}
