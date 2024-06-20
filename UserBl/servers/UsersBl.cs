using AutoMapper;
using DogBarberShopBl.intarfaces;
using DogBarberShopDl.EF.Models;
using DogBarberShopDl.Intarfaces;
using DogBarberShopDl.Servises;
using DogBarberShopEntitis;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UserEntites;

namespace DogBarberShopBl.servers
{
    public class UsersBl:IusersBl
    {
        private readonly ILogger<UsersBl> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Appsetting _appSettings;
        private readonly IUsersDl _usersDl;
        private readonly IMapper _mapper;

        public UsersBl(ILogger<UsersBl> logger, IUsersDl usersDl, IMapper mapper, IHttpContextAccessor httpContextAccessor, IOptions<Appsetting> options)
        {
            _logger = logger;
            _usersDl = usersDl;
            _mapper = mapper;
            _httpContextAccessor= httpContextAccessor;
            _appSettings = options.Value;


        }

        public bool Login(UserLoginDTO userLoginDTO)
        {
            _logger.LogInformation("Login method started");
            User userMapped = _mapper.Map<User>(userLoginDTO);
            User userFromDb = _usersDl.Login(userMapped);

            if (userFromDb is not null)
            {
                _logger.LogInformation("User found in database");

                CreateUserToken(userFromDb.UserName, userFromDb.Id); // שינוי כאן

                byte[] usernameByteArray = Encoding.ASCII.GetBytes(userFromDb.UserName);
                _httpContextAccessor.HttpContext.Session.Set("username", usernameByteArray);

                byte[] userIdByteArray = BitConverter.GetBytes(userFromDb.Id); // שינוי כאן
                _httpContextAccessor.HttpContext.Session.Set("userId", userIdByteArray); // שינוי כאן

                return true;
            }

            _logger.LogWarning("User not found in database");
            return false;
        }

        private void CreateUserToken(string userName, int userId) // שינוי כאן
        {
            string newAccessToken = this.GenerateAccessToken(userName, userId); // שינוי כאן

            CookieOptions cookieTokenOptions = new CookieOptions()
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.Now.AddMinutes(_appSettings.Jwt.ExpireMinutes)
            };

            _httpContextAccessor.HttpContext.Response.Cookies.Append(CookiesKeys.AccessToken, newAccessToken, cookieTokenOptions);
        }

        private string GenerateAccessToken(string userName, int userId) // שינוי כאן
        {
            var jwtSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Jwt.SecretKey));
            var credentials = new SigningCredentials(jwtSecurityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
        new Claim(ClaimTypes.Name, userName),
       new Claim(ClaimTypes.NameIdentifier, userId.ToString()) };
            var token = new JwtSecurityToken(
                _appSettings.Jwt.Issuer,
                _appSettings.Jwt.Audience,
                claims,
                expires: DateTime.Now.AddMinutes(_appSettings.Jwt.ExpireMinutes),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}

