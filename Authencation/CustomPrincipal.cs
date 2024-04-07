using CF_HOATUOIBASANH.Models;
using System.Security.Principal;

namespace CF_HOATUOIBASANH.Authencation
{
    public class CustomPrincipal : IPrincipal
    {
        private Account Account;
        private string NameRole;


        public CustomPrincipal(Account account,string RoleAccount)
        {

            this.Account = account;
            this.Identity = new GenericIdentity(account.Username);
            this.NameRole = RoleAccount;
        }
        public IIdentity Identity
        {
            get;
            set;
        }

        public bool IsInRole(string role)
        {
            var permissions = role.Split(',');
            return permissions.Contains(NameRole);
        }
    }
}
