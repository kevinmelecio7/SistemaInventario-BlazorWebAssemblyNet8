using AppInvWebServer.Data;
using AppInvWebSharedLibrary.State;
using AppInvWebSharedLibrary.Contracts;
using AppInvWebSharedLibrary.DTOs;
using AppInvWebSharedLibrary.Models;
using AppInvWebSharedLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AppInvWebServer.Repositories
{
    public class Account : IAccount
    {
        private readonly AppDbContext appDbContext;
        private readonly IConfiguration config;

        public Account(AppDbContext appDbContext, IConfiguration config)
        {
            this.appDbContext = appDbContext;
            this.config = config;
        }

        public async Task<LoginResponse> LoginAsync(LoginDTO model)
        {
            var findUser = await GetUser(model.Email);
            if (findUser == null)
                return new LoginResponse(false, "User not found");

            if (!BCrypt.Net.BCrypt.Verify(model.Password, findUser.Password))
                return new LoginResponse(false, "Email/Password not invalid");

            string jwtToken = GenerateToken(findUser);
            return new LoginResponse(true, "Login Successfully", jwtToken);
        }

        public async Task<LoginResponse> LogoutAsync()
        {
            ConstantsJWT.JWTToken = "";
            string jwtToken = ConstantsJWT.JWTToken;
            return new LoginResponse(true, "Logout Successfully", jwtToken);
        }

        public async Task<RegistrationResponse> RegisterAsync(RegisterDTO model)
        {
            var findUser = await GetUser(model.Email);
            if (findUser != null)
                return new RegistrationResponse(false, "User already exist");

            appDbContext.Users.Add(new ApplicationUser
            {
                Name = model.Name,
                Email = model.Email,
                Role = model.Role,
                Password = BCrypt.Net.BCrypt.HashPassword(model.Password)
            });

            await appDbContext.SaveChangesAsync();
            return new RegistrationResponse(true, "Success");
        }

        private string GenerateToken(ApplicationUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name!),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.Role, user.Role!)
            };
            var token = new JwtSecurityToken(
                issuer: config["Jwt:Issuer"]!,
                audience: config["Jwt:Audience"]!,
                claims: userClaims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        private async Task<ApplicationUser> GetUser(string email)
            => await appDbContext.Users.FirstOrDefaultAsync(e => e.Email == email);



    }
}
