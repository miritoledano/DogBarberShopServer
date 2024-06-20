using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using DogBarberShopBl.intarfaces;
using System.Collections;
using DogBarberShopBl.servers;
// Assuming your business logic services are in this namespace

namespace DogBarberShopApi.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class MyAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var serviceProvider = context.HttpContext.RequestServices;
            var queueBl = serviceProvider.GetService<IqueueBl>();
            var homeBl = serviceProvider.GetService<IhomeBl>();

            if (context.ActionArguments.TryGetValue("id", out object idObj) && idObj is int id)
            {
                var queueUserId = queueBl.GetUserById(id);
                var userId = homeBl.GetUserId();
                if (queueUserId == userId)
                {
                    await next(); // Continue with the action
                }
                else
                {
                    context.Result = new ForbidResult(); // Return 403 Forbidden if user is not authorized
                }
            }
            else
            {
                context.Result = new BadRequestObjectResult("Invalid request"); // Return 400 Bad Request if id is missing or not an integer
            }
        }

    }
}