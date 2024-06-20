using DogBarberShopApi;
using DogBarberShopBl.intarfaces;
using DogBarberShopBl.servers;
using DogBarberShopDl.EF.Contexts;
using DogBarberShopDl.Intarfaces;
using DogBarberShopDl.Servises;
using DogBarberShopEntitis;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UserEntites;


var builder = WebApplication.CreateBuilder(args).UseSerilog();
builder.Services.Configure<Appsetting>(builder.Configuration);
Appsetting appSettings = builder.Configuration.Get<Appsetting>();
builder.Services.AddScoped<IuserBl, UserBl>();
builder.Services.AddScoped<IuserDl, UserDl>();
builder.Services.AddScoped<IqueueBl,QueueBl>();
builder.Services.AddScoped<IoueueDl,QueueDl>();
builder.Services.AddScoped<IusersBl, UsersBl>();
builder.Services.AddScoped<IUsersDl, UsersDl>();
builder.Services.AddScoped<IhomeBl, HomeBl>();
builder.Services.AddScoped<IhomeDl, HomeDl>();


builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddMemoryCache();
builder.Services.AddSingleton(appSettings);

builder.Services.AddSession();
builder.Services.AddAutoMapper(typeof(MapperManager));

// או כל מימוש אחר של IDistributedCache שברצונך להשתמש בו


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = appSettings.Jwt.Issuer,
        ValidAudience = appSettings.Jwt.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.Jwt.SecretKey)),
        ClockSkew = TimeSpan.Zero
    };
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            context.Token = context.Request.Cookies[CookiesKeys.AccessToken];
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder.WithOrigins("http://localhost:3000")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
        );
});



//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("CorsPolicy",
//        builder => builder.WithOrigins("http://localhost:3000")
//        .AllowAnyMethod()
//        .AllowAnyHeader()
//        .AllowCredentials()
//        );
//});



builder.Services.AddDbContext<DogBarberShopContext>(options =>
{
    options.UseSqlServer(appSettings.ConnectionStrings.DogBarberShop);

});
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.UseSession();

//app.UseCors("CorsPolicy");
app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();


app.UseSession();
app.MapControllers();

app.Run();
