using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyrus.Domain.Entities
{
    public class Ammo : EntityBase
    {
        public int AmmoId { get; private set; }
        
        public Ammo()
        {

        }

        public Ammo(int ammoId)
        {
            this.AmmoId = ammoId;
        }
    }
}
