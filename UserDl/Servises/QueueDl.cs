using DogBarberShopDl.EF.Contexts;
using DogBarberShopDl.EF.Models;
using DogBarberShopDl.Intarfaces;
using DogBarberShopEntitis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogBarberShopDl.Servises
{
    public class QueueDl : IoueueDl
    {
        private readonly DogBarberShopContext _dogBarberShopContext;
        private readonly ILogger<QueueDl> _logger;
        public QueueDl(DogBarberShopContext context, ILogger<QueueDl> logger)

        {
            _logger = logger;
            _dogBarberShopContext = context;
        }
        public List<QueueWithUserDetails> GetAllQueues()
        {
            var queuesWithUserDetails = _dogBarberShopContext.Queues
                .Join(_dogBarberShopContext.Users,
                      queue => queue.UserId,
                      user => user.Id,
                      (queue, user) => new QueueWithUserDetails(
                          queue.Id,
                          queue.Date,
                          queue.Hour,
                          queue.Id,
                          user.UserName
                      )).ToList();

            return queuesWithUserDetails;
        }


        public void AddQueue(Queue queue)
        {
            try
            {
                var exist = _dogBarberShopContext.Queues.FirstOrDefault(x => x.Date == queue.Date && x.Hour == queue.Hour);
                if (exist != null)
                {
                    // קיים תור עם אותם התאריך והשעה, זרוק חריגה
                    throw new InvalidOperationException("A queue with the same date and hour already exists.");
                }

                queue.ProductionDate = DateTime.Now;
                _dogBarberShopContext.Add(queue);
                _dogBarberShopContext.SaveChanges();
                _logger.LogInformation("Queue added successfully: {Queue}", queue);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a queue. Queue details: {Queue}", queue);
                throw;
            }
        }

        public void UpdateQueue(Queue queue)
        {
            var queueFromDb = _dogBarberShopContext.Queues.FirstOrDefault(x => x.Id == queue.Id);
            var exist = _dogBarberShopContext.Queues.FirstOrDefault(x => x.Date == queue.Date && x.Hour == queue.Hour);
            if (exist != null)
            {
                // קיים תור עם אותם התאריך והשעה, זרוק חריגה
                throw new InvalidOperationException("A queue with the same date and hour already exists.");
            }
            if (queueFromDb != null)
            {
                queueFromDb.Date = queue.Date;
                queueFromDb.Hour = queue.Hour;
                _dogBarberShopContext.SaveChanges();
            }
            else
            {
                // טיפול במקרה שבו התור לא נמצא
                throw new Exception("Queue not found");
            }
        }
        public void DeleteQueue(int id)
        {
            var queueFromDb = _dogBarberShopContext.Queues.FirstOrDefault(x => x.Id == id);

            if (queueFromDb != null)
            {
                _dogBarberShopContext.Queues.Remove(queueFromDb);
                _dogBarberShopContext.SaveChanges();
            }
            else
            {
                // טיפול במקרה שבו התור לא נמצא
                throw new Exception("Queue not found");
            }
        }

        public int GetUserById(int queueId)
        {
            var queue = _dogBarberShopContext.Queues.FirstOrDefault(q => q.Id == queueId);
            return queue?.UserId ?? 0;
        }

    }


}