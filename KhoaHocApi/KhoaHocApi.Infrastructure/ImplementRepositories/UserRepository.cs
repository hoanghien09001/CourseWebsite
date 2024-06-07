using KhoaHocApi.Domain.Entities;
using KhoaHocApi.Domain.InterfaceRepositories;
using KhoaHocApi.Infrastructure.DataContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Infrastructure.ImplementRepositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        #region Xử lý chuỗi
        private Task<bool> CompareStringAsync(string str1, string str2)
        {
            return Task.FromResult(string.Equals(str1.ToLowerInvariant(),str2.ToLowerInvariant()));
        }
        private async Task<bool> IsStringInListAsync(string inputString, List<string> liststrings)
        {
            if(inputString == null)
            {
                throw new ArgumentNullException(nameof(inputString));
            }
            if(liststrings == null)
            {
                throw new ArgumentNullException(nameof(liststrings));
            }
            foreach (var item in liststrings)
            {
                if(await CompareStringAsync(inputString, item))
                {
                    return true;
                }
            }
            return false;
        }
        #endregion
        public async Task AddRoleToUserAsync(User user, List<string> listRoles)
        {
            if(user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if(listRoles == null)
            {
                throw new ArgumentNullException(nameof(listRoles));
            }
            foreach(var role in listRoles.Distinct()) 
            {
                var roleOfUser = await GetRoleOfUserAsync(user);
                if (await IsStringInListAsync(role, roleOfUser.ToList()))
                {
                    throw new ArgumentException("Người dùng đã có quyên này");
                }
                else
                {
                    var roleItem = await _context.Roles.SingleOrDefaultAsync(x => x.RoleCode.Equals(role));
                    if(roleItem == null)
                    {
                        throw new ArgumentException("Không tồn tại quyền này");
                    }
                    _context.Permissions.Add(new Permission
                    {
                        RoleId = roleItem.Id,
                        UserId = user.Id,
                    });

                }
            }
            _context.SaveChanges();
        }

        public async Task<IEnumerable<string>> GetRoleOfUserAsync(User user)
        {
            var roles = new List<string>(); 
            var listRoles = _context.Permissions.Where(x=>x.UserId == user.Id).AsQueryable();
            foreach (var item in listRoles.Distinct())
            {
                var role = _context.Roles.SingleOrDefault(x => x.Id == item.RoleId);
                roles.Add(role.RoleCode);
            }
            return roles.AsEnumerable();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x=>x.Email.ToLower().Equals(email.ToLower()));
            return user;
        }

        public async Task<User> GetUserByPhoneNumber(string phoneNumber)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.PhoneNumber.ToLower().Equals(phoneNumber.ToLower()));
            return user;
        }

        public async Task<User> GetUserByUsername(string username)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Username.ToLower().Equals(username.ToLower()));
            return user;
        }

        public async Task DeleteRoleAsync(User user, List<string> roles)
        {
            var listRoles = await GetRoleOfUserAsync(user);
            if(roles == null)
            {
                throw new ArgumentNullException(nameof(roles));
            }
            if(listRoles == null)
            {
                throw new ArgumentNullException(nameof(listRoles));
            }
            foreach(var roleItem in listRoles)
            {
                foreach(var role in roles)
                {
                    var roleObject = _context.Roles.SingleOrDefault(x => x.RoleCode.Equals(role));
                    var permission = _context.Permissions.SingleOrDefault(x=>x.RoleId==roleObject.Id && x.UserId==user.Id);
                    if(await CompareStringAsync(role, roleItem))
                    {
                        _context.Permissions.Remove(permission);
                    }
                }
            }
            _context .SaveChanges();
        }
        public async Task<IQueryable<User>> GetUserHaveCertificate()
        {
            var listUser = _context.Users.Include(x => x.Certificate).Where(x => x.CertificateId != null).AsQueryable();
            if (listUser == null)
            {
                return Enumerable.Empty<User>().AsQueryable();
            }
            return listUser;
        }
    }
}
