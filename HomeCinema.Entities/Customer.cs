using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCinema.Entities {
    public class Customer : IEntityBase {

        public int Id { get; set; }
        public String FirtsName { get; set; }
        public String LastName {get; set;}
        public String Email { get; set; }
        public String IdentityCard { get; set; }
        public Guid UniqueKey { get; set; }
        public DateTime DateOfBirth { get; set; }
        public String Mobile { get; set; }
        public DateTime RegistrationDate { get; set; }  
    }
}
