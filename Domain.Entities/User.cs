using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyrus.Domain.Entities
{
    public class User : EntityBase, ILoggable
    {
        public int UserId { get; private set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public GenderType? Gender { get; set; }
        public Address Address { get; set; }
        public string MobileNumber { get; set; }
        public UserType Role { get; set; }
        public int ProfileId { get; set; }

        public User() { }

        public User(int userId)
        {
            this.UserId = userId;
        }

        public bool validate()
        {
            var isvalid = true;

            if (string.IsNullOrWhiteSpace(FirstName)) isvalid = false;
            if (string.IsNullOrWhiteSpace(LastName)) isvalid = false;
            if (string.IsNullOrWhiteSpace(EmailAddress)) isvalid = false;
            if (string.IsNullOrWhiteSpace(Address.ToString())) isvalid = false;
            if (string.IsNullOrWhiteSpace(Password)) isvalid = false;
            if (string.IsNullOrWhiteSpace(Gender.ToString())) isvalid = false;
            if (string.IsNullOrWhiteSpace(MobileNumber)) isvalid = false;
            if (string.IsNullOrWhiteSpace(Role.ToString())) isvalid = false;

            return isvalid;
        }

        public string Log()
        {
            var logString = this.UserId + ": " +
                "Full Name: " + this.FirstName + " " + this.LastName + " " +
                "Email Address: " + this.EmailAddress + " " +
                "Status: " + this.EntityState.ToString();

            return logString;
        }
    }
}
