using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Project.MVCUI.ViewModels
{
    public class PasswordChangeViewModel
    {
        [Display(Name = "Mevcut Şifre")]
        [Required(ErrorMessage = "{0} boş bırakılamaz")]
        [StringLength(40, MinimumLength = 6, ErrorMessage = "{0} {2} ile {1} karakter arası olmalıdır")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Display(Name = "Yeni Şifre")]
        [Required(ErrorMessage = "{0} boş bırakılamaz")]
        [StringLength(40, MinimumLength = 6, ErrorMessage = "{0} {2} ile {1} karakter arası olmalıdır")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; } = null!;

        [Display(Name = "Yeni Şifre Tekrar")]
        [Required(ErrorMessage = "{0} boş bırakılamaz")]
        [Compare(nameof(NewPassword), ErrorMessage = "Girmiş olduğunuz şifreler uyuşmuyor")]
        [StringLength(40, MinimumLength = 6, ErrorMessage = "{0} {2} ile {1} karakter arası olmalıdır")]
        [DataType(DataType.Password)]
        public string NewPasswordConfirm { get; set; } = null!;
    }
}
