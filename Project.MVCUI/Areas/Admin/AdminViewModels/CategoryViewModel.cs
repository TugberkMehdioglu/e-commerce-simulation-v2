using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Project.MVCUI.Areas.Admin.AdminViewModels
{
    public class CategoryViewModel
    {
        public int? Id { get; set; }
        [Display(Name = "Ad")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz")]
        [StringLength(80, MinimumLength = 2, ErrorMessage = "{0} alanı {2} ile {1} karakter arasında olmalıdır")]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? FormerName { get; set; }
    }
}
