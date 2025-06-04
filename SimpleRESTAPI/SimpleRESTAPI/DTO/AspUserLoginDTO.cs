using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleRESTAPI.DTO
{
    public class AspUserLoginDTO
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Token { get; set; } 
    }
}