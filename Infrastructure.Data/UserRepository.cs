using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyrus.Infrastructure.Data
{
    public class UserRepository
    {
        public User Retreive(int userId)
        {
            // Create the instance of the Profile class
            User user = new User(userId);

            // Code that retreives the defined profile

            // Temp hard coded vals to return profile data
            if (userId == 1)
            {
                user.FirstName = "Paul";
                user.LastName = "Duncan";
                user.EmailAddress = "duncan@gmail.com";
                user.Gender = null;
                user.Role = UserType.User;
                user.ProfileId = 1;
            }

            return user;
        }
    }
}
