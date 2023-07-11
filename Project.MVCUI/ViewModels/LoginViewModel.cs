using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Project.MVCUI.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "{0} boş bırakılamaz")]
        [EmailAddress(ErrorMessage = "{0} formatı yanlıştır")]
        [MaxLength(80, ErrorMessage = "{0} alanı en fazla {1} karakter olabilir")]
        [DataType(DataType.Text)]
        public string Email { get; set; } = null!;

        [Display(Name = "Şifre")]
        [Required(ErrorMessage = "{0} boş bırakılamaz")]
        [MinLength(6, ErrorMessage = "Şifre en az {1} karakter olabilir")]
        [DataType(DataType.Password)]
        public string PasswordHash { get; set; } = null!;

        [Display(Name = "Beni Hatırla")]
        public bool RememberMe { get; set; }
    }
}
