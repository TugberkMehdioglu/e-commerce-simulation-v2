using Project.ENTITIES.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Project.MVCUI.ViewModels
{
    public class AddressViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Adres Adı")]
        [Required(ErrorMessage = "{0} boş bırakılamaz")]
        [StringLength(40, MinimumLength = 2, ErrorMessage = "{0} {2} ile {1} karakter arası olmalıdır")]
        public string Name { get; set; } = null!;

        [Display(Name = "Ülke")]
        [Required(ErrorMessage = "{0} boş bırakılamaz")]
        public string Country { get; set; } = null!;

        [Display(Name = "Şehir")]
        [Required(ErrorMessage = "{0} boş bırakılamaz")]
        public string City { get; set; } = null!;

        [Display(Name = "Semt")]
        [Required(ErrorMessage = "{0} boş bırakılamaz")]
        public string District { get; set; } = null!;

        [Display(Name = "Mahalle")]
        [Required(ErrorMessage = "{0} boş bırakılamaz")]
        public string NeighborHood { get; set; } = null!;

        [Display(Name = "Sokak")]
        [Required(ErrorMessage = "{0} boş bırakılamaz")]
        public string Street { get; set; } = null!;

        [Display(Name = "Apartman No")]
        [Required(ErrorMessage = "{0} boş bırakılamaz")]
        [DataType(DataType.Text)]
        [Range(1, 500, ErrorMessage ="{0} {1} ile {2} arasında olmalıdır")]
        [RegularExpression(@"^\d+", ErrorMessage = "Sadece rakam giriniz")]
        public byte AptNo { get; set; }

        [Display(Name ="Daire")]
        [DataType(DataType.Text)]
        [RegularExpression(@"^\d+", ErrorMessage = "Sadece rakam giriniz")]
        public byte? Flat { get; set; }
        public string FullAddress
        {
            get
            {
                return $"{District} {NeighborHood} Mahallesi {Street} Sokak, No:{AptNo} Daire:{Flat ?? 0} - {City.ToUpper()}/{Country.ToUpper()}";
            }
        }

        public string? AppUserProfileId { get; set; }


        //Navigation Properties
        public AppUserProfileViewModel? AppUserProfile { get; set; }
    }
}
