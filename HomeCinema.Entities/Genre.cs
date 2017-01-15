using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCinema.Entities {
    public class Genre : IEntityBase {

        public Genre() {
            Movies = new List<Movie>();
        }

        public int Id { get; set; }
        public String Name { get; set; }
        public virtual ICollection<Movie> Movies { get; set; }


    }
}
