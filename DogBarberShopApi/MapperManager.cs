using AutoMapper;
using DogBarberShopDl.EF.Models;
using DogBarberShopEntitis;

namespace DogBarberShopApi
{
    public class MapperManager: Profile
    {
        public MapperManager()
        {

            CreateMap<AddQueueDTO, Queue>();
            CreateMap<UserLoginDTO, User>();
            CreateMap<AddUserDTo,User>();
            CreateMap<UpdateQueueDTO, Queue>();

        }
    }
}
