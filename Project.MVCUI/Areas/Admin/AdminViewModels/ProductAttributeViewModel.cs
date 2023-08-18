using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Project.MVCUI.Areas.Admin.AdminViewModels
{
    public class ProductAttributeViewModel
    {
        public int? Id { get; set; }

        [Display(Name = "Özellik Adı")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "{0} alanı {2} ile {1} karakter arasında olmalıdır")]
        public string Name { get; set; } = null!;

        [Display(Name = "Değer")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "{0} alanı {2} ile {1} karakter arasında olmalıdır")]
        public string Value { get; set; } = null!;
        public int? ProductId { get; set; }
    }
}
