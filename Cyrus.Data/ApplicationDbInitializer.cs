using System;
using System.Collections.Generic;
using System.Data.Entity;
using Cyrus.Core.DomainModels;
using Cyrus.Data.Identity;
using Cyrus.Data.Identity.Models;
using Microsoft.AspNet.Identity;

namespace Cyrus.Data
{
    // This is useful if you do not want to tear down the database each time you run the application.
    // public class ApplicationDbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    // This example shows you how to create a new database if the Model changes
    public class ApplicationDbInitializer : DropCreateDatabaseAlways<CyrusDataContext>
    {
        protected override void Seed(CyrusDataContext context)
        {
            InitializeIdentityForEF(context);
            base.Seed(context);
        }

        //Create User=Admin@Admin.com with password=Admin@123456 in the Admin role        
        public void InitializeIdentityForEF(CyrusDataContext db)
        {
            // This is only for testing purpose
            const string name = "admin@admin.com";
            const string password = "Admin@123456";
            const string roleName = "Admin";
            var applicationRoleManager = IdentityFactory.CreateRoleManager(db);
            var applicationUserManager = IdentityFactory.CreateUserManager(db);
            
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
                    Email = name,
                    ProfileInfo = new UserProfile()
                    {
                        UserId    =  user.Id,
                        ProfileName = "Alley Cats",
                        Attributes =  new List<ProfileAttribute>()
                        {
                            new ProfileAttribute()
                            {
                                ProfileAttributeId = 1,
                                ProfileId = user.ProfileInfo.Id,
                                Response = "Abc"
                            }
                        }
                    }
                    
                };
                
                applicationUserManager.Create(user, password);
                applicationUserManager.SetLockoutEnabled(user.Id, false);
            }

            // Add user admin to Role Admin if not already added
            var rolesForUser = applicationUserManager.GetRoles(user.Id);
            if (!rolesForUser.Contains(role.Name))
            {
                applicationUserManager.AddToRole(user.Id, role.Name);
            }
            var context = new CyrusDataContext("name=AppContext", new DebugLogger());
            
            var tribe = new Tribe()
            {
                Description = "Alley Cats",
                IsActive = true,
                IsPublic = false
            };

            context.Set<Tribe>().Add(tribe);

            var member = new TribeMember() //add to tribe entity
            {
                IsAdmin = true,
                IsApproved = true,
                UserId = user.Id
            };

            context.Set<TribeMember>().Add(member);

            //for (var i = 0; i < 100; i++)
            //{
            //    var setAdmin = i == 50;        

            //    context.Set<TribeMember>().Add(new TribeMember
            //    {
            //        IsApproved = false,
            //        IsAdmin = setAdmin,
            //        TribeId = tribe.Id,
            //    });
            //}

            context.SaveChanges();
        }
        class DebugLogger : Core.Logging.ILogger
        {
            public void Log(string message)
            {

            }

            public void Log(Exception ex)
            {

            }
        }
    }
}