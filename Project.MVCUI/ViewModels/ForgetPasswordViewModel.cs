using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Project.MVCUI.ViewModels
{
    public class ForgetPasswordViewModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz")]
        [EmailAddress(ErrorMessage = "{0} formatı yanlıştır")]
        [MaxLength(80, ErrorMessage = "{0} alanı en fazla {1} karakter olabilir")]
        [DataType(DataType.Text)]
        public string Email { get; set; } = null!;

        [Display(Name = "Şifre")]
        [Required(ErrorMessage = "{0} boş bırakılamaz")]
        [StringLength(40, MinimumLength = 6, ErrorMessage = "{0} {2} ile {1} karakter arası olmalıdır")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; } = null!;

        [Display(Name = "Şifre Tekrar")]
        [Required(ErrorMessage = "{0} boş bırakılamaz")]
        [StringLength(40, MinimumLength = 6, ErrorMessage = "{0} {2} ile {1} karakter arası olmalıdır")]
        [Compare(nameof(NewPassword), ErrorMessage = "Girmiş olduğunuz şifreler uyuşmuyor")]
        [DataType(DataType.Password)]
        public string NewPasswordConfirm { get; set; } = null!;
    }
}
