using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyrus.Core.DomainModels
{
    public class Alert : BaseEntity
    {
        public int UserId { get; set; }
        public int AlertTypeId { get; set; }
        public bool IsHidden { get; set; }
        public string Message { get; set; }
    }
}
