using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCinema.Entities {
    public class Movie : IEntityBase {

        public Movie() {
            Stocks = new List<Stock>();
        }

        public int Id { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }
        public String Image { get; set; }
        public int GenreId { get; set; }
        public virtual Genre Genre { get; set; }
        public String Director { get; set; }
        public String Writer { get; set; }
        public String Producer { get; set; }
        public DateTime ReleaseDate { get; set; }
        public Byte Rating { get; set; }
        public string TrailerURI { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }

    }
}
