using HomeCinema.Entities;

namespace HomeCinema.Data.Configurations {
    public class CustomerConfiguration : EntityBaseConfiguration<Customer> { 

        public CustomerConfiguration() {
            Property(c => c.FirstName).IsRequired().HasMaxLength(100);
            Property(c => c.LastName).IsRequired().HasMaxLength(100);
            Property(c => c.IdentityCard).IsRequired().HasMaxLength(50);
            Property(c => c.UniqueKey).IsRequired();
            Property(c => c.Mobile).HasMaxLength(10);
            Property(c => c.Email).IsRequired().HasMaxLength(200);
            Property(c => c.DateOfBirth).IsRequired();
        }
    }
}
