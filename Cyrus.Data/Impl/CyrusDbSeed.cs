using System.Collections.Generic;
using Cyrus.Core.DomainModels;
using Cyrus.Data.Identity;
using Cyrus.Data.Identity.Models;
using Microsoft.AspNet.Identity;

namespace Cyrus.Data.Impl
{
    public static class CyrusDbSeed
    {
        public static void Seed(ICyrusDbContext context)
        {

            // This is only for testing purposes
            const string name = "admin@cyrus.com";
            const string password = "Admin@12345";
            const string roleName = "Admin";

            var applicationRoleManager = IdentityFactory.CreateRoleManager((CyrusDbContext) context);
            var applicationUserManager = IdentityFactory.CreateUserManager((CyrusDbContext) context);
            
            //Create Role Admin if it does not exist
            var role = applicationRoleManager.FindByName(roleName);
            if (role == null)
            {
                role = new ApplicationIdentityRole { Name = roleName };
                applicationRoleManager.Create(role);
            }


            var user = applicationUserManager.FindByName(name);
            if (user == null)
            {
                user = new ApplicationIdentityUser
                {
                    UserName = name,
                    Email = name

                };

                IdentityResult createResult = applicationUserManager.Create(user, password);
                applicationUserManager.SetLockoutEnabled(user.Id, false);
            }

            // Add user admin to Role Admin if not already added
            var rolesForUser = applicationUserManager.GetRoles(user.Id);
            if (!rolesForUser.Contains(role.Name))
            {
                applicationUserManager.AddToRole(user.Id, role.Name);
            }

            var tribes = new List<Tribe>
            {
                new Tribe{Name = "Alley Cats 0", Description = "Scrappy cats 0", UserId = 1, IsActive = true, IsPublic = false},
                new Tribe{Name = "Alley Cats 1", Description = "Scrappy cats 1", UserId = 2, IsActive = true, IsPublic = false},
                new Tribe{Name = "Alley Cats 2", Description = "Scrappy cats 2", UserId = 3, IsActive = true, IsPublic = false},
                new Tribe{Name = "Alley Cats 3", Description = "Scrappy cats 3", UserId = 4, IsActive = true, IsPublic = false},
                new Tribe{Name = "Alley Cats 4", Description = "Scrappy cats 4", UserId = 5, IsActive = true, IsPublic = false},
                new Tribe{Name = "Alley Cats 5", Description = "Scrappy cats 5", UserId = 6, IsActive = true, IsPublic = false},
                new Tribe{Name = "Alley Cats 6", Description = "Scrappy cats 6", UserId = 4, IsActive = true, IsPublic = false},
                new Tribe{Name = "Alley Cats 7", Description = "Scrappy cats 7", UserId = 5, IsActive = true, IsPublic = false},
                new Tribe{Name = "Alley Cats 8", Description = "Scrappy cats 8", UserId = 8, IsActive = true, IsPublic = false},

            };

            tribes.ForEach(t => context.Set<Tribe>().Add(t));
            ((CyrusDbContext)context).SaveChanges();

            var members = new List<TribeMember> //add to tribe entity
            {
                new TribeMember {IsAdmin = false, IsApproved = false, TribeId = 2, UserId = 1 },
                new TribeMember {IsAdmin = false, IsApproved = true, TribeId = 2, UserId = 2 },
                new TribeMember {IsAdmin = false, IsApproved = false, TribeId = 2, UserId = 3 },
                new TribeMember {IsAdmin = false, IsApproved = false, TribeId = 2, UserId = 4 },
                new TribeMember {IsAdmin = false, IsApproved = true, TribeId = 2, UserId = 5 },
                new TribeMember {IsAdmin = false, IsApproved = true, TribeId = 2, UserId = 6 },
                new TribeMember {IsAdmin = true, IsApproved = true, TribeId = 2, UserId = 7 },
                new TribeMember {IsAdmin = false, IsApproved = false, TribeId = 2, UserId = 8 },
                new TribeMember {IsAdmin = false, IsApproved = true, TribeId = 2, UserId = 9 }
            };

            members.ForEach(tm => context.Set<TribeMember>().Add(tm));
            ((CyrusDbContext)context).SaveChanges();
            
        
        }
    }
}
