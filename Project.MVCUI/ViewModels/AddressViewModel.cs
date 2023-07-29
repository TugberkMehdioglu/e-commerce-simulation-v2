using Project.ENTITIES.Models;

namespace Project.MVCUI.ViewModels
{
    public class AddressViewModel
    {
        public string Name { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string City { get; set; } = null!;
        public string District { get; set; } = null!;
        public string NeighborHood { get; set; } = null!;
        public string Street { get; set; } = null!;
        public byte AptNo { get; set; }
        public byte? Flat { get; set; }
        public string FullAddress
        {
            get
            {
                return $"{District} {NeighborHood} Mahallesi {Street} Sokak, No:{AptNo} Daire:{Flat ?? 0} - {City.ToUpper()}/{Country.ToUpper()}";
            }
        }


        //Navigation Properties
        public AppUserProfileViewModel AppUserProfile { get; set; } = null!;
    }
}
