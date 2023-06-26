using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class AppUserProfile : BaseEntity
    {
        public new string Id { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string FullName { get { return $"{FirstName} {LastName}"; } }//Will not be in database
        public string? ImagePath { get; set; }

        //Navigation Properties
        public AppUser AppUser { get; set; } = null!;
        public ICollection<Address>? Addresses { get; set; }
    }
}
