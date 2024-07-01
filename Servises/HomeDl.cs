using DogBarberShopDl.EF.Contexts;
using DogBarberShopDl.Intarfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogBarberShopDl.Servises
{
    public class HomeDl:IhomeDl
    {
        private readonly DogBarberShopContext _dogBarberShopContext;
        private readonly ILogger<HomeDl> _logger;
        public HomeDl(DogBarberShopContext dogBarberShopContext, ILogger<HomeDl> logger)
        {
            _dogBarberShopContext = dogBarberShopContext;
            _logger = logger;
        }
        public string GetUserName()

        {

            var user = _dogBarberShopContext.Users.FirstOrDefault();
            return user?.UserName ?? "Unknown";
        }

        public int GetUserId()
        {
            // חיפוש היוזר הראשון במסד הנתונים
            var user = _dogBarberShopContext.Users.FirstOrDefault();
            Console.WriteLine(user);

            // אם מצאנו יוזר, נחזיר את ה-Id שלו, אחרת נחזיר ערך ברירת מחדל (למשל -1)
            return user.Id;
                
        }

    }
}
