using AutoMapper;
using DogBarberShopBl.intarfaces;
using DogBarberShopDl.EF.Models;
using DogBarberShopDl.Intarfaces;
using DogBarberShopDl.Servises;
using DogBarberShopEntitis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogBarberShopBl.servers
{
    public class QueueBl:IqueueBl
    {
        private readonly IoueueDl _ioueueDl;
        private readonly IMapper _mapper;
        private readonly IhomeBl _homeBl;

        public QueueBl(IoueueDl IoueueDl, IMapper mapper, IhomeBl homeBl)
        {
            _ioueueDl = IoueueDl;
            _mapper = mapper;
           
                _homeBl = homeBl; 

        }
        public void AddQueue(AddQueueDTO queueDTO)
        {

          
           Queue quequMapper = _mapper.Map<Queue>(queueDTO);
            quequMapper.UserId= _homeBl.GetUserId();
            _ioueueDl.AddQueue(quequMapper);
        }
        public  List<QueueWithUserDetails> GetAllQueues()
        {
            return _ioueueDl.GetAllQueues();
        }
        public   void UpdateQueue(UpdateQueueDTO queue)
        {

            Queue quequMapper = _mapper.Map<Queue>(queue);
            

            _ioueueDl.UpdateQueue(quequMapper);
        }
        public void DeleteQueue(int Id)
        {
            

            _ioueueDl.DeleteQueue(Id);

        }
       public int GetUserById(int queueId)
        {
         return   _ioueueDl.GetUserById(queueId);
        }

    }
}
