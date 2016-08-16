using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyrus.Domain.Interfaces
{
    interface IProfileRepository
    {
        void AddProfile(Profile newProfile);
        IEnumerable<Profile> GetProfiles();
        void UpdateProfile(string lastName, Profile updatedProfile);
        void UpdateProfiles(IEnumerable<Profile> updatedProfiles);
        void DeleteProfileById(int ProfileId);
    }
}
