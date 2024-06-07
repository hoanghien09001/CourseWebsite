using KhoaHocApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Domain.InterfaceRepositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserByUsername(string username);
        Task<User> GetUserByPhoneNumber(string phoneNumber);
        Task AddRoleToUserAsync(User user, List<string> listRoles);
        Task<IEnumerable<string>> GetRoleOfUserAsync(User user);
        Task DeleteRoleAsync(User user, List<string> roles);
        Task<IQueryable<User>> GetUserHaveCertificate();

    }
}
