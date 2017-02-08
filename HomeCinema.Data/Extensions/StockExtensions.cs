using HomeCinema.Data.Repositories;
using HomeCinema.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCinema.Data.Extensions {
    public static class StockExtensions {

        public static IEnumerable<Stock> GetAvailableItems(this IEntityBaseRepository<Stock> stockRepository, int movieId) {
            IEnumerable<Stock> availableItems;
            availableItems = stockRepository.GetAll().Where(s => s.MovieId == movieId && s.IsAvailable);
            return availableItems;
        }
    }
}
