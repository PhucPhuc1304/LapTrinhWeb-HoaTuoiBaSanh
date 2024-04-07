using CF_HOATUOIBASANH.Models;

namespace CF_HOATUOIBASANH.Interfaces
{

    public interface IRoleRepository
    {
        Task<Role> GetRoleByIdAsync(int roleId);
        Task<IEnumerable<Role>> GetAllRolesAsync();
        Task<int> AddRoleAsync(Role role);
        Task UpdateRoleAsync(Role role);
        Task DeleteRoleAsync(int roleId);
        Task<string> GetRoleNameByAccountIdAsync(int accountId);


    }

}
