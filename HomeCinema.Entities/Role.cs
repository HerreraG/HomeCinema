using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCinema.Entities {
    public class Role : IEntityBase {

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
