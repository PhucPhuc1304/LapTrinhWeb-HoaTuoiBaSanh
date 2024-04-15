using CF_HOATUOIBASANH.FormatHelper;
using CF_HOATUOIBASANH.Interfaces;
using CF_HOATUOIBASANH.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CF_HOATUOIBASANH.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[CustomAuthorize(Roles = "Admin")]
    public class CustomerController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IRoleRepository _roleRepository;

        public CustomerController(IAccountRepository accountRepository, ICustomerRepository customerRepository, IRoleRepository roleRepository)
        {
            _accountRepository = accountRepository;
            _customerRepository = customerRepository;
            _roleRepository = roleRepository;
        }
        public async Task<IActionResult> Index()
        {
            var customers = await _customerRepository.GetAllCustomersAsync();
            return View(customers);
        }
        public async Task<IActionResult> Add()
        {
            var roles = await _roleRepository.GetAllRolesAsync();
            ViewBag.Role = roles;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddCustomer(string firstName, string lastName, string address, string phone, string email, string type, string password, string role)
        {
            var existingAccount = await _accountRepository.GetAccountByEmailAsync(email);

            if (existingAccount != null)
            {
                return BadRequest(new { error = "Tài khoản đã được đăng ký. Vui lòng thử lại." });
            }

            var account = new Account
            {
                Username = email,
                Password = HashPassword.Hash(password),
                RoleID = int.Parse(role),
            };

            var accountId = await _accountRepository.AddAccountAsync(account);

            if (accountId > 0)
            {
                var customer = new Customer
                {
                    AccountID = accountId,
                    FirstName = firstName,
                    LastName = lastName,
                    Address = address,
                    Phone = phone,
                    Email = email,
                    Type = type
                };

                var customerId = await _customerRepository.AddCustomerAsync(customer);

                if (customerId > 0)
                {
                    return RedirectToAction("Index", "Customer");
                }
                else
                {
                    return BadRequest(new { error = "Thêm thông tin tài khoản thất bại." });
                }
            }
            else
            {
                return BadRequest(new { error = "Đăng ký tài khoản thất bại" });
            }
            
        }

        public async Task<IActionResult> Edit(int id)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(id);
            var account = await _accountRepository.GetAccountByEmailAsync(customer.Email);
            var roleAccount = await _roleRepository.GetRoleByIdAsync(account.RoleID);
            ViewBag.RoleAccount = roleAccount;
            var roles = await _roleRepository.GetAllRolesAsync();
            ViewBag.Roles = roles;
            ViewBag.Customer = customer;
            ViewBag.Account = account;

            return View();
        }
        public async Task<IActionResult> EditCustomer(int customerId, string firstName, string lastName, string address, string phone, string email, string type, string password, string role)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(customerId);
            customer.FirstName = firstName;
            customer.LastName = lastName;
            customer.Address = address;
            customer.Phone = phone;
            customer.Email = email;
            customer.Type = type;

            var account = await _accountRepository.GetAccountByIdAsync(customer.AccountID);
            account.RoleID = int.Parse(role);

            if (account.Username != email)
            {
                account.Username = email;
            }

            if (account.Password != password)
            {
                string hashedPassword = HashPassword.Hash(password);
                account.Password = hashedPassword;
            }
            await _customerRepository.UpdateCustomerAsync(customer);
            await _accountRepository.UpdateAccountAsync(account);

            return RedirectToAction("Index", "Customer");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(id);
            var account = await _accountRepository.GetAccountByEmailAsync(customer.Email);
            _accountRepository.DeleteAccountAsync(account.AccountID);
            _customerRepository.DeleteCustomerAsync(customer.CustomerID);

            return RedirectToAction("Index", "Customer");
        }
    }
}
