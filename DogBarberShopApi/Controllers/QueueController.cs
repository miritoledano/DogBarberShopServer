
using DogBarberShopApi.Attributes;
using DogBarberShopBl.intarfaces;
using DogBarberShopBl.servers;
using DogBarberShopDl.EF.Models;
using DogBarberShopDl.Servises;
using DogBarberShopEntitis;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.ConstrainedExecution;

namespace DogBarberShopApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class QueueController : ControllerBase
    {
        private readonly IqueueBl _iqueueBl;
        private readonly IhomeBl _homeBl;

        public QueueController(IqueueBl iqueueBl, IhomeBl homeBl)
        {
            _iqueueBl = iqueueBl;
            _homeBl = homeBl;
        }


        [HttpGet]
        public IActionResult GetAllQueues()
        {
            try
            {
                List<QueueWithUserDetails> queues = _iqueueBl.GetAllQueues();
                return Ok(queues);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize]

        [HttpPost]
        public IActionResult AddQueue(AddQueueDTO queueDTO)
        {
            try
            {
                Console.WriteLine("Received Date: " + queueDTO.Date);
                Console.WriteLine("Received Hour: " + queueDTO.Hour);
                //queueDTO.UserId = _homeBl.GetUserId();
                _iqueueBl.AddQueue(queueDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        //[MyAttribute]

        [MyAttribute]

        [HttpPut("{id}")]

        public IActionResult UpdateQueue(int id, UpdateQueueDTO queue)
        {
            try
            {

                queue.Id = id;

                _iqueueBl.UpdateQueue(queue);
                return Ok(new
                {
                    id = queue.Id,
                    date = queue.Date,
                    hour = queue.Hour,

                    // ניתן להוסיף כאן עוד מאפיינים או נתונים על המשתמש שברצונך להחזיר
                });

            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [MyAttribute]
        [HttpDelete("{id}")]

        public IActionResult DeleteQueue(int id)
        {
            try
            {
                var UserId = _homeBl.GetUserId();

                _iqueueBl.DeleteQueue(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet]
        public IActionResult GetUserById(int UserID)
        {
            try
            {
                var userId = _iqueueBl.GetUserById(UserID);
                return Ok(userId);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
    }
