using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Project.MVCUI.Areas.Admin.AdminViewModels
{
    public class ProductViewModel
    {
        public int? Id { get; set; }

        [Display(Name = "Ürün Adı")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "{0} alanı {2} ile {1} karakter arasında olmalıdır")]
        public string Name { get; set; } = null!;
        public string? FormerName { get; set; }

        [Display(Name = "Fiyat")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz")]
        [RegularExpression(@"^[0-9]+(\.[0-9]{1,2})$", ErrorMessage = "{0} alanında noktadan sonra en fazla 2 basamak olmalıdır")]
        public decimal Price { get; set; }

        [Display(Name = "Stok")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz")]
        [Range(1, 100000, ErrorMessage = "{0} alanı {1} ile {2} arasında olmalıdır")]
        [DataType(DataType.Text)]
        public short Stock { get; set; }
        public string? ImagePath { get; set; }

        [Display(Name = "Ürün açıklaması")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz")]
        [StringLength(10000, MinimumLength = 2, ErrorMessage = "{0} alanı {2} ile {1} karakter arasında olmalıdır")]
        public string Description { get; set; } = null!;
        public IFormFile? Image { get; set; }

        [Display(Name = "Kategori")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz")]
        public int CategoryID { get; set; }

        [ValidateNever]
        public CategoryViewModel? Category { get; set; }

        [ValidateNever]
        public List<ProductAttributeViewModel>? ProductAttributes { get; set; }
    }
}
