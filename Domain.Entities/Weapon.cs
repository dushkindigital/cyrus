using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cyrus.Common;

namespace Cyrus.Domain.Entities
{
    public class Weapon : EntityBase, ILoggable
    {
        public int WeaponId { get; private set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int AmmoId { get; set; }
        public int Amount { get; set; }
        public string PictureURL { get; set; }

        public Weapon()
        {

        }

        public Weapon(int weaponId)
        {
            this.WeaponId = weaponId;
        }


        public string Log()
        {
            var logString = this.AmmoId + ": " +
                 "Ammo Name: " + this.Name + " " +
                 "Status: " + this.EntityState.ToString();

            return logString;
        }
    }

}
