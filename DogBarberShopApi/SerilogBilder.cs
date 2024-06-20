using Serilog;

namespace DogBarberShopApi
{
    public static class SerilogBilder
    {
       
            public static WebApplicationBuilder UseSerilog(this WebApplicationBuilder builder)
            {
                builder
                    .Host
                    .UseSerilog((
                        hostingContext,
                        loggerConfiguration) =>
                    {
                        loggerConfiguration
                        .ReadFrom
                        .Configuration(hostingContext.Configuration);
                    });

                return builder;
            }
        }
    }

