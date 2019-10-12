using HMS.Model.Auth;
using System.Collections.Generic;

namespace HMS.Service.Auth.Interfaces
{
    public interface IUsersService
    {
        string GetSerialNumber(int userId);
        IEnumerable<string> GetUserRoles(int userId);
        User FindUser(string username, string password);
        User FindUser(int userId);
        void UpdateUserLastActivityDate(int userId);
    }
}