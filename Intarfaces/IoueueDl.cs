using DogBarberShopDl.EF.Models;
using DogBarberShopEntitis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogBarberShopDl.Intarfaces
{
    public interface IoueueDl
    {
        public  void AddQueue(Queue queue);
        public void UpdateQueue(Queue queue);
        void DeleteQueue(int id);
        int GetUserById(int userId);
        List<QueueWithUserDetails> GetAllQueues();
    }
}
