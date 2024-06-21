using DogBarberShopDl.EF.Contexts;
using DogBarberShopDl.EF.Models;
using DogBarberShopDl.Intarfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogBarberShopDl.Servises
{
    public class UserDl:IuserDl
    {
        private readonly DogBarberShopContext _dogBarberShopContext;
        private readonly ILogger<UserDl> _logger;
        public UserDl(DogBarberShopContext context, ILogger<UserDl> logger)
        {
            _logger = logger;

            _dogBarberShopContext = context;
        }
        public void AddUserName(User user)
        {
            var existingUser = _dogBarberShopContext.Users
                .FirstOrDefault(u =>  u.Password == user.Password);

            if (existingUser != null)
            {
                // User with the same username or password already exists
                throw new InvalidOperationException("User with the same username or password already exists.");
            }

            _logger.LogInformation("User added successfully: {user}", user);
            _dogBarberShopContext.Users.Add(user);
            _dogBarberShopContext.SaveChanges();
        }

        public List<User> GetAllUsers()
        {
            return _dogBarberShopContext.Users.ToList();
        }
    }
}
