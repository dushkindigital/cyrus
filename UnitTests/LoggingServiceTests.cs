using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cyrus.Tests.UnitTests
{
    [TestClass]
    public class LoggingServiceTests
    {
        [TestMethod]
        public void WriteToFileTest()
        {
            //--Arrange
            var changedItems = new List<ILoggable>();

            var profile = new Profile(1)
            {
                ProfileName = "Alley Cat",
                Gender = GenderType.Male,
                Role = ProfileType.A,
                Strength = 16,
                WeaponsList = null,
                AmmoList = null
            };

            changedItems.Add(profile as ILoggable);

            var user = new User(2)
            {
                FirstName = "Peter",
                LastName = "Dushkin",
                EmailAddress = "dushkin@gmail.com",
                ProfileId = 1
            };

            changedItems.Add(user as ILoggable);

            //--Act
            LoggingService.WriteToFile(changedItems);

            //Nothing to assert - view the output.
        }
    }
}
