using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Cyrus.Domain.Entities
{
    public class Profile : EntityBase, ILoggable
    {
        public int ProfileId { get; private set; }
        public string ProfileName { get; set; }
        public GenderType Gender { get; set; }
        public int AvatarId { get; set; }
        public List<Weapon> WeaponsList { get; set; }
        public List<Ammo> AmmoList { get; set; }
        public int Strength { get; set; }
        public ProfileType Role { get; set; }
        public int TribeId { get; set; }
        public DateTimeOffset CreateDate { get; private set; }
        public DateTimeOffset? LastUpdateDate { get; private set; }

        public Profile()
            : this(0)
        { 
        
        }

        public Profile(int profileId)
        {
            this.ProfileId = profileId;
                WeaponsList = new List<Weapon>();
                AmmoList = new List<Ammo>();
        }

        public bool validate()
        {
            var isvalid = true;

            if (string.IsNullOrWhiteSpace(ProfileName)) isvalid = false;
            if (string.IsNullOrWhiteSpace(Gender.ToString())) isvalid = false;

            return isvalid;
        }

        public string Log()
        {
            var logString = this.ProfileId + ": " +
                this.ProfileName + " " +
                "Status: " + this.EntityState.ToString();

            return logString;
        }
    }
}
