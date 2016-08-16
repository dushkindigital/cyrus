using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyrus.Domain.Interfaces
{
    interface IUserRepository
    {
        void AddUser(User newUser);
        IEnumerable<User> GetUsers();
        void UpdateUser(string lastName, User updatedUser);
        void UpdateUsers(IEnumerable<User> updatedUsers);
        void DeleteUserByUserId(int userId);
    }
}
