using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyrus.Domain.Entities
{
    public class Tribe : EntityBase, ILoggable
    {
        public int TribeId { get; private set; }
        public int AdminId { get; set; }
        public string Detail { get; set; }

        public Tribe()
        {

        }

        public Tribe(int tribeId)
        {
            this.TribeId = tribeId;
        }


        public string Log()
        {
            var logString = this.TribeId + ": " +
                "Status: " + this.EntityState.ToString();

            return logString;
        }
    }
}
