using HomeCinema.Entities;

namespace HomeCinema.Data.Configurations {
    public class UserRoleConfiguration : EntityBaseConfiguration<UserRole> {

        public UserRoleConfiguration() {
            Property(ur => ur.UserId).IsRequired();
            Property(ur => ur.RoleId).IsRequired();
        }
    }   
}
