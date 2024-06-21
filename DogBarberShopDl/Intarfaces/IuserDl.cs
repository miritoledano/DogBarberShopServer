using DogBarberShopDl.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogBarberShopDl.Intarfaces
{
    public interface IuserDl
    {
        public void AddUserName(User user);
        List<User> GetAllUsers();
    }
}
