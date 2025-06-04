using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRESTAPI.Helpers
{
    public class ApiSettings
    {
        internal static string SecretKey = "78n90er5ljnbvgsp6zTRfmVnTGGD8GM5";

        internal static byte[] GenerateSecretBytes() =>
         Encoding.ASCII.GetBytes(SecretKey);

    }
}