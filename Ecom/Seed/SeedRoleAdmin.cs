using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Model;

namespace Ecommerce.Seed
{
    public class SeedRoleAdmin
    {
        private readonly ModelBuilder modelBuilder;


        public SeedRoleAdmin(ModelBuilder modelBuilder)
        {
            this.modelBuilder = modelBuilder;
        }
        public void Seed()
        {
            //Remove if Exists
            if (modelBuilder.Entity<IdentityRole>() == null)
            {
                modelBuilder.Entity<IdentityRole>().HasData(

                       new IdentityRole
                       {
                           Name = "Admin",
                           NormalizedName = "ADMIN",
                           Id = "90aed1eb-2271-4599-b43b-75022c33a0ab"
                       });
            }
            if (modelBuilder.Entity<ApplicationUser>() == null)
            {
                var hasher = new PasswordHasher<ApplicationUser>();
                modelBuilder.Entity<ApplicationUser>().HasData(
                        new ApplicationUser
                        {
                            Id = "b9a31127-dde2-4f61-ade0-3f67c84ed615",
                            Email = "vikasmalik8881@gmail.com",
                            NormalizedEmail = "VIKASMALIK8881@GMAIL.COM",
                            UserName = "vikasmalik8881@gmail.com",
                            NormalizedUserName = "vikasmalik8881@gmail.com",
                            FirstName = "Vikas",
                            LastName = "Malik",
                            PasswordHash = hasher.HashPassword(null, "Urmylifeak@0607")
                        }
                     
                 );
            }

            if (modelBuilder.Entity<ApplicationUser>() == null)
            {

            }
           
                modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                RoleId = "90aed1eb-2271-4599-b43b-75022c33a0ab",
                UserId = "b9a31127-dde2-4f61-ade0-3f67c84ed615"
            },
            new IdentityUserRole<string>
            {
                RoleId = "90aed1eb-2271-4599-b43b-75022c33a0ab",
                UserId = "c330472c-5045-47bd-b855-90add86ac5e8"
            }
            );

            //Insert Exists
        }
        public void seed2()
        {
            if (modelBuilder.Entity<IdentityRole>() != null)
            {
                modelBuilder.Entity<IdentityRole>().HasData(

                       new IdentityRole
                       {
                           Name = "Admin",
                           NormalizedName = "ADMIN",
                           Id = "90aed1eb-2271-4599-b43b-75022c33a0ab"
                       });
            }
            if (modelBuilder.Entity<ApplicationUser>() != null)
            {
                var hasher = new PasswordHasher<ApplicationUser>();
                modelBuilder.Entity<ApplicationUser>().HasData(
                        new ApplicationUser
                        {
                            Id = "b9a31127-dde2-4f61-ade0-3f67c84ed615",
                            Email = "vikasmalik8881@gmail.com",
                            NormalizedEmail = "VIKASMALIK8881@GMAIL.COM",
                            UserName = "vikasmalik8881@gmail.com",
                            NormalizedUserName = "vikasmalik8881@gmail.com",
                            FirstName = "Vikas",
                            LastName = "Malik",
                            PasswordHash = hasher.HashPassword(null, "Urmylifeak@0607")
                        }
                      
                 );
            }

            if (modelBuilder.Entity<ApplicationUser>() != null)
            {

            }
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                RoleId = "90aed1eb-2271-4599-b43b-75022c33a0ab",
                UserId = "b9a31127-dde2-4f61-ade0-3f67c84ed615"
            },
            new IdentityUserRole<string>
            {
                RoleId = "90aed1eb-2271-4599-b43b-75022c33a0ab",
                UserId = "c330472c-5045-47bd-b855-90add86ac5e8"
            }
            );

        
    }
    }
}
