using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogBarberShopEntitis
{
    public class QueueWithUserDetails
    {
     
            public int Id { get; set; }
            public DateTime Date { get; set; }
            public string Hour { get; set; }
            public int UserId { get; set; }
            public string UserName { get; set; }

            public QueueWithUserDetails(int id, DateTime date, string hour, int userId, string userName)
            {
                Id = id;
                Date = date;
                Hour = hour;
                UserId = userId;
                UserName = userName;
            }
        }

    }

