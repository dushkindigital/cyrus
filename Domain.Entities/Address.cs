using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyrus.Domain.Entities
{
    public class Address
    {
        public int AddressId { get; private set; }
        public AddressType Type { get; set; }
        public string StreetLine1 { get; set; }
        public string StreetLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }

        public Address()
        {  
        }

        public Address(int addressId)
        {
            this.AddressId = addressId;
        }


    }
}
