using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyrus.Infrastructure.Data
{
    public class ProfileRepository
    {
        /// <summary>
        /// Retreive one profile
        /// </summary>
        /// <param name="profileId"></param>
        /// <returns></returns>
        public Profile Retreive(int profileId)
        {
            // Create the instance of the Profile class
            Profile profile = new Profile(profileId);

            // Code that retreives the defined profile

            // Temp hard coded vals to return profile data
            if (profileId == 1)
            {
                profile.ProfileName = "Friendly Cat";
                profile.Role = ProfileType.A;
                profile.Strength = 16;
                profile.Gender = GenderType.Male;
                profile.Role = ProfileType.A;
                profile.TribeId = 1;
            }

            return profile;
        }

        /// <summary>
        /// Retreives all profiles
        /// </summary>
        /// <returns></returns>
        public List<Profile> Retreive()
        { 
            // code that retreives all customers
            return new List<Profile>();
        }


    }
}
