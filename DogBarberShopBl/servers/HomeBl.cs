using Azure.Core;
using DogBarberShopBl.intarfaces;
using DogBarberShopEntitis;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DogBarberShopEntites;

namespace DogBarberShopBl.servers
{
    public class HomeBl : IhomeBl
    {
        private Appsetting _Appsetting;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeBl(Appsetting appSettings, IHttpContextAccessor httpContextAccessor)
        {
            _Appsetting = appSettings;
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserName()
        {
            byte[] byteArray;
            _httpContextAccessor.HttpContext.Session.TryGetValue("username", out byteArray);
            string userName = Encoding.ASCII.GetString(byteArray);
            return userName;
        }

        public int GetUserId() { 
  
            var token = _httpContextAccessor.HttpContext.Request.Cookies[CookiesKeys.AccessToken];
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadJwtToken(token);
            string userId = jsonToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userId, out int id))
            {
                throw new Exception("Invalid user ID in token");
            }

            return id;
        }

        public string GetHeader()
        {
            var token = _httpContextAccessor.HttpContext.Request.Cookies[CookiesKeys.AccessToken];
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadJwtToken(token);

            string userName = jsonToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            return ($"דף הבית - {userName}");
        }

      
    }
    }
