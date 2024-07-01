using DogBarberShopDl.EF.Contexts;
using DogBarberShopDl.EF.Models;
using DogBarberShopDl.Intarfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogBarberShopDl.Servises
{
    public class UsersDl: IUsersDl
    {
        private readonly DogBarberShopContext _dogBarberShopContext;
        public UsersDl(DogBarberShopContext dogBarberShopContext)
        {
           _dogBarberShopContext= dogBarberShopContext;
        }

        public User Login(User user)
        {
            User existingUser = _dogBarberShopContext.Users.FirstOrDefault(u => u.UserName == user.UserName && u.Password == user.Password);
            if (existingUser == null)
            {
                throw new Exception("המשתמש לא קיים במערכת. יש להירשם תחילה.");
            }

            return existingUser;
        }

    }
}

