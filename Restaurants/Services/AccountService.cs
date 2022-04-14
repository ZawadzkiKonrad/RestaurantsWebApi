using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Restaurants.Entities;
using Restaurants.Exceptions;
using Restaurants.ViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Services
{
    public interface IAccountService
    {
        void RegisterUser(RegisterUserVm vm);
        string GenerateJwt(LoginUserVm vm);
    }

    public class AccountService : IAccountService
    {
        private readonly Context _context;
        private readonly IPasswordHasher<AppUser> _passwordHasher;
        private readonly AutheticationSettings _autheticationSettings;

        public AccountService(Context context, IPasswordHasher<AppUser> passwordHasher, AutheticationSettings autheticationSettings)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _autheticationSettings = autheticationSettings;
        }

        public string GenerateJwt(LoginUserVm vm)
        {
            var user = _context.Users.FirstOrDefault(x => x.Email == vm.Email);
            if (user is null)
            {
                throw new BadRequestException("Invalid email or password/Zły email lub hasło");
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, vm.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid email or password/Zły email lub hasło");

            }
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name,$"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Role,$"{user.Role}"),
                new Claim("DateOfBirth",user.DateOfBirth.Value.ToString("yyyy-MM-dd")),
                new Claim("Nationality",user.Nationality),
            };

            if (!string.IsNullOrEmpty(user.Nationality))
            {
                claims.Add(
                    new Claim("Nationality", user.Nationality)
                          );
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_autheticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_autheticationSettings.JwtExpireDays);
            var token = new JwtSecurityToken(_autheticationSettings.JwtIssuer,
                _autheticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

        public void RegisterUser(RegisterUserVm vm)
        {

            var newUser = new AppUser()
            {
                Email = vm.Email,
                DateOfBirth = vm.DateOfBirth,
                Nationality = vm.Nationality,
                RoleId = (int)vm.RoleId
            };
            var passwordHash = _passwordHasher.HashPassword(newUser, vm.Password);
            newUser.PasswordHash = passwordHash;
            ; _context.Add(newUser);
            _context.SaveChanges();

        }
    }
}
