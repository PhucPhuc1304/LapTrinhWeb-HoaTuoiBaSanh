using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System;
using System.Security.Cryptography;
namespace CF_HOATUOIBASANH.FormatHelper
{
    public static class FormatHelper
    {
        public static string FormatPriceVND(double price)
        {
            double giaLe = (double)price;
            string giaLeFomat = giaLe.ToString("N0") ;
            return giaLeFomat;
        }  
    }
    public class NameParser
    {
        public static (string firstName, string lastName) SplitFullName(string fullName)
        {
            string[] parts = fullName.Trim().Split(' ');

            if (parts.Length >= 2)
            {
                string lastName = parts[parts.Length - 1];

                string firstName = string.Join(" ", parts.Take(parts.Length - 1));

                return (firstName, lastName);
            }
            else
            {
                return (fullName, string.Empty);
            }
        }
    }
    public static class HashPassword
    {
        private const string DefaultSalt = "JCYZ3d8kJa";
        public static string Hash(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password + DefaultSalt));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
    }

}
