﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCinema.Entities {
    public class Stock : IEntityBase {

        public Stock() {
            Rentals = new List<Rental>();
        }

        public int Id { get; set; }
        public int MovieId { get; set; }
        public virtual Movie Movie { get; set; }
        public Guid UniqueKey { get; set; }
        public bool IsAvailable { get; set; }
        public virtual ICollection<Rental> Rentals { get; set; }

    }
}
