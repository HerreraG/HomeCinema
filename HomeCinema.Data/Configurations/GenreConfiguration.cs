using HomeCinema.Entities;

namespace HomeCinema.Data.Configurations {
    public class GenreConfiguration : EntityBaseConfiguration<Genre> {

        public GenreConfiguration() {
            Property(g => g.Name).IsRequired().HasMaxLength(50);
        }
    }
}
