using AutoMapper;
using DogBarberShopBl.intarfaces;
using DogBarberShopDl.EF.Models;
using DogBarberShopDl.Intarfaces;
using DogBarberShopDl.Servises;
using DogBarberShopEntitis;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DogBarberShopEntites;

namespace DogBarberShopBl.servers
{
    public class UserBl:IuserBl
    {

        private readonly IuserDl _userDl;
        private readonly IMapper _mapper;
        public UserBl(IuserDl userDl, IMapper mapper)
        {
            _userDl = userDl;
            _mapper = mapper;
            
        }
        public void AddUserName(AddUserDTo UserDTO)
        {
            User UserMapper = _mapper.Map<User>(UserDTO);
            _userDl.AddUserName(UserMapper);
        }
        public List<User> GetAllUsers()
        {
            return _userDl.GetAllUsers();
        }

    }
}
