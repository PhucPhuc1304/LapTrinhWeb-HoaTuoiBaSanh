using CF_HOATUOIBASANH.Interfaces;

namespace CF_HOATUOIBASANH.Repositorys
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using CF_HOATUOIBASANH.Models;
    using Microsoft.EntityFrameworkCore;

    public class EFRoleRepository : IRoleRepository
    {
        private readonly HoaTuoiBaSanhContext _context;

        public EFRoleRepository(HoaTuoiBaSanhContext context)
        {
            _context = context;
        }

        public async Task<Role> GetRoleByIdAsync(int roleId)
        {
            return await _context.Roles.FindAsync(roleId);
        }

        public async Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<int> AddRoleAsync(Role role)
        {
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
            return role.RoleID;
        }

        public async Task UpdateRoleAsync(Role role)
        {
            _context.Entry(role).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRoleAsync(int roleId)
        {
            var role = await _context.Roles.FindAsync(roleId);
            if (role != null)
            {
                _context.Roles.Remove(role);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<string> GetRoleNameByAccountIdAsync(int accountId)
        {
            var roleId = await _context.Accounts
                .Where(account => account.AccountID == accountId)
                .Select(account => account.RoleID)
                .FirstOrDefaultAsync();

            if (roleId != null)
            {
                var roleName = await _context.Roles
                    .Where(role => role.RoleID == roleId)
                    .Select(role => role.RoleName)
                    .FirstOrDefaultAsync();

                return roleName;
            }
            return null;
        }
    }

}
