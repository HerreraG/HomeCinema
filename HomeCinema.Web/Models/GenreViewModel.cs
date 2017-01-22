using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeCinema.Web.Models {
    public class GenreViewModel {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumberOfMovies { get; set; }
    }
}