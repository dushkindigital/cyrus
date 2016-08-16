using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cyrus.Tests.UnitTests
{
    [TestClass]
    public class UserRepositoryTests
    {
        [TestMethod]
        public void RetreiveExisting()
        {
            //--Arrange
            var userRepository = new UserRepository();

            var expected = new User(1)
            {
                EmailAddress = "duncan@gmail.com",
                FirstName = "Paul",
                LastName = "Duncan",
                Gender = GenderType.Male,
                Address = new Address 
                { 
                    City = "Moscow",
                    Country = "Russia"
                },
                MobileNumber = "+7 (789) 738-73-23",
                Role = UserType.User
            };

            //--Act

            //--Assert

        }
    }
}
