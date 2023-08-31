using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Project.MVCUI.ViewModels.WrapperClasses
{
    public class OrderWrapper
    {
        public AppUserViewModel? AppUser { get; set; }


        //--------------------Address Information-----------------
        [Display(Name ="Adres")]
        [Required(ErrorMessage ="{0} seçi")]
        public int AddressId { get; set; }
        public string FullAddress { get; set; } = null!;
        //--------------------------------------------------------


        //---------------------Card Informations-------------------


        [Display(Name = "Kart Sahibinin Adı Soyadı")]
        [Required(ErrorMessage = "Bu alan boş bırakılamaz")]
        [DataType(DataType.Text)]
        public string CardUserName { get; set; } = null!;

        [Display(Name = "Güvenlik Numarası")]
        [Required(ErrorMessage = "Bu alan boş bırakılamaz")]
        [RegularExpression(@"^\d{3}$", ErrorMessage = "Geçersiz format (222 vb.)")]
        public string SecurityNumber { get; set; } = null!;

        [Display(Name = "Kart Numarası")]
        [Required(ErrorMessage = "Bu alan boş bırakılamaz")]
        [StringLength(19, MinimumLength = 19, ErrorMessage = "19 karakter olmalıdır")]
        [RegularExpression(@"^\d{4} \d{4} \d{4} \d{4}$", ErrorMessage = "Bu alan sadece rakamlardan oluşmalı ve 16 haneli olmalıdır")]
        public string CardNumber { get; set; } = null!;

        [Display(Name = "Son kullanma Ayı")]
        [Required(ErrorMessage = "Bu alan boş bırakılamaz")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "2 karakter olmalıdır")]
        [RegularExpression(@"^\d{2}$", ErrorMessage = "Bu alan sadece rakamlardan oluşmalıdır")]
        public string CardExpiryMounth { get; set; } = null!;

        [Display(Name = "Son kullanma Yılı")]
        [Required(ErrorMessage = "Bu alan boş bırakılamaz")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "4 karakter olmalıdır")]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "Bu alan sadece rakamlardan oluşmalıdır")]
        public string CardExpiryYear { get; set; } = null!;
        public decimal ShoppingPrice { get; set; }
        //----------------------------------------------------------
    }
}
