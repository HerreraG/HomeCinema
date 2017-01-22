using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCinema.Entities {
    public class Error : IEntityBase {

        public int Id { get; set; }
        public String Message { get; set; }
        public String StackTrace { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
