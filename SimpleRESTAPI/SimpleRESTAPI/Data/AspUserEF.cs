using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SimpleRESTAPI.Models;

namespace SimpleRESTAPI.Data
{
    public class AspUserEF : IAspUser
    {
        private readonly ApplicationDbContext _context;
        public AspUserEF(ApplicationDbContext context)
        {
            _context = context;
        }
        public void DeleteUser(string username)
        {
            throw new NotImplementedException();
        }


        public string GenerateToken(string username)
        {
           var user = GetUserByUsername(username);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with username '{username}' not found.");
            }
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username)
            };
            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var key = Helpers.ApiSettings.GenerateSecretBytes();
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public IEnumerable<AspUser> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public AspUser GetUserByUsername(string username)
        {
           if(string.IsNullOrEmpty(username))
            {
                throw new ArgumentException("Username cannot be null or empty", nameof(username));
            }

            var user = _context.AspUsers.FirstOrDefault(u => u.Username == username);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with username '{username}' not found.");
            }
            return user;
        }

        public bool Login(string username, string password)
        {
            var user = _context.AspUsers.FirstOrDefault(u => u.Username == username);
            if (user == null)
                return false;

            var hashed = Helpers.HashHelper.HashPassword(password);
            return user.Password == hashed;
        } 

        public AspUser RegisterUser(AspUser user)
        {
            try
            {
                if (user == null)
                {
                    throw new ArgumentNullException(nameof(user), "User cannot be null");
                }
                user.Password = Helpers.HashHelper.HashPassword(user.Password);
                _context.AspUsers.Add(user);
                _context.SaveChanges();
                return user;
            }
            catch (DbUpdateException dbex)
            {
                throw new Exception("An error occurred while registering the user", dbex);
            }
            catch (System.Exception ex)
            {
                throw new Exception("An unexpected error occurred", ex);
            }
        }

        public AspUser UpdateUser(AspUser user)
        {
            throw new NotImplementedException();
        }
    }
}