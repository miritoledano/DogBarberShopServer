using DogBarberShopDl.EF.Models;
using DogBarberShopEntitis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogBarberShopBl.intarfaces
{
    public interface IqueueBl
    {
        void AddQueue(AddQueueDTO queue);
        void UpdateQueue(UpdateQueueDTO queueDTO);
        void DeleteQueue(int Id);
        int GetUserById(int userId);
        List<QueueWithUserDetails> GetAllQueues();
    }
}
