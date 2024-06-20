using DogBarberShopDl.EF.Models;
using DogBarberShopEntitis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogBarberShopBl.intarfaces
{
    public interface IuserBl
    {
        void AddUserName(AddUserDTo user);
            
        List<User> GetAllUsers(); 
    }
}
