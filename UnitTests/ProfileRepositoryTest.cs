using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cyrus.Tests.UnitTests
{
    [TestClass]
    public class ProfileRepositoryTests
    {
        [TestMethod]
        public void RetreiveExisting()
        {
            //-- Arrange
            var profileRepository = new ProfileRepository();
            
            var expected = new Profile(1)
            {
               ProfileName = "Friendly Cat",
               Role = ProfileType.A,
               Strength = 16,
               Gender = GenderType.Male,
               TribeId = 1
            };

            //-- Act
            var actual = profileRepository.Retreive(1);

            //-- Assert
            Assert.AreEqual(expected.ProfileId, actual.ProfileId);
            Assert.AreEqual(expected.ProfileName, actual.ProfileName);
            Assert.AreEqual(expected.Gender, actual.Gender);
            Assert.AreEqual(expected.AvatarId, actual.AvatarId);
            Assert.AreEqual(expected.Strength, actual.Strength);
            Assert.AreEqual(expected.Role, actual.Role);
            Assert.AreEqual(expected.TribeId, actual.TribeId);

        }

    }

}
