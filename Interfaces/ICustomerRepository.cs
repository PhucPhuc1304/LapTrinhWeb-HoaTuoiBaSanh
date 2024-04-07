using CF_HOATUOIBASANH.Models;

namespace CF_HOATUOIBASANH.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer> GetCustomerByIdAsync(int customerId);
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<int> AddCustomerAsync(Customer customer);
        Task UpdateCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(int customerId);
        Task<Customer> GetCustomerByEmailAsync(string email);

    }
}
