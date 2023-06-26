using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class Address : BaseEntity
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
        }//Will not be in database

        public string AppUserProfileId { get; set; } = null!;



        //Navigation Properties
        public AppUserProfile AppUserProfile { get; set; } = null!;
        public ICollection<Order>? Orders { get; set; }
    }
}
