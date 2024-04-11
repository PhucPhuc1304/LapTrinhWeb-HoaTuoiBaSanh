using CF_HOATUOIBASANH.Interfaces;
using CF_HOATUOIBASANH.Models;
using Microsoft.AspNetCore.Mvc;
using CF_HOATUOIBASANH.FormatHelper;
using Newtonsoft.Json;
namespace CF_HOATUOIBASANH.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IRoleRepository _roleRepository;

        public AccountController(IAccountRepository accountRepository, ICustomerRepository customerRepository, IRoleRepository roleRepository)
        {
            _accountRepository = accountRepository;
            _customerRepository = customerRepository;
            _roleRepository = roleRepository;
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Logout()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(string fullName, string sex, string password, string confirmPassword, string address, string phoneNumber, string email)
        {
            var existingAccount = await _accountRepository.GetAccountByEmailAsync(email);
            (string firstName, string lastName) = NameParser.SplitFullName(fullName);

            if (existingAccount != null)
            {
                return BadRequest(new { error = "Tài khoản đã được đăng ký. Vui lòng thử lại." });
            }
            var account = new Account
            {
                Username = email,
                Password = HashPassword.Hash(password),
                RoleID = 1,
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
                    Phone = phoneNumber,
                    Email = email,
                    Type = "KH"
                };
                var customerId = await _customerRepository.AddCustomerAsync(customer);
                if (customerId > 0)
                {
                    return Ok(new { success = true });
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


        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            string hashedPassword = HashPassword.Hash(password);

            var account = await _accountRepository.GetAccountByEmailAsync(email);

            if (account != null)
            {
                if (account.Password == hashedPassword)
                {

                    var serializedAccount = JsonConvert.SerializeObject(account);
                    HttpContext.Session.SetString("LoggedInAccount", serializedAccount);

                    var customer = await _customerRepository.GetCustomerByEmailAsync(email);
                    {
                        HttpContext.Session.SetString("CustomerLastName", customer.LastName);
                    }
                    var role = await _roleRepository.GetRoleNameByAccountIdAsync(account.AccountID);
                    {
                        HttpContext.Session.SetString("RoleName", role);
                    }

                    return Ok(new { success = true });
                }
                else
                {
                    return BadRequest(new { error = "Sai email hoặc mật khẩu." });
                }
            }
            else
            {
                return BadRequest(new { error = "Tài khoản không tồn tại." });
            }

        }
    }
}
