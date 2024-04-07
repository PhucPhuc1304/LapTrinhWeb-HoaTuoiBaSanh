using CF_HOATUOIBASANH.Models;

namespace CF_HOATUOIBASANH.Interfaces
{
    using System.Threading.Tasks;

    public interface IAccountRepository
    {
        Task<Account> GetAccountByIdAsync(int accountId);
        Task<IEnumerable<Account>> GetAllAccountsAsync();
        Task<int> AddAccountAsync(Account account);
        Task UpdateAccountAsync(Account account);
        Task DeleteAccountAsync(int accountId);
        Task<Account> GetAccountByEmailAsync(string email);

    }

}
