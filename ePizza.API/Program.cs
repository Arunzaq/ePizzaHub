
using epizza.Domain.Models;
using ePizza.API.Middlewares;
using ePizza.Core.Concrete;
using ePizza.Core.Contracts;
using ePizza.Core.Utils;
using ePizza.Repository.Concrete;
using ePizza.Repository.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ePizza.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDbContext<epizzaHubDBContext>(Options =>
            {
                Options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection"));
            });
            builder.Services.AddSingleton<TokenGenerator>();

            builder.Services.AddTransient<IAuthServices, AuthServices>();
            builder.Services.AddTransient<IUserService, UserServices>(); //Registering dependancies
            builder.Services.AddTransient<IItemServices, ItemServices>();
            builder.Services.AddTransient<ICartRepository, CartRepository>();
            builder.Services.AddTransient<ICartServices, CartServices>();

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IRolesRepository,RoleRepository>();
            builder.Services.AddScoped<IItemRepository, ItemRepository>();
           


            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(Options =>
                {
                    Options.RequireHttpsMetadata = false;
                    Options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]!)),
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        ClockSkew = TimeSpan.FromMinutes(10)
                    };
                });

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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

            app.UseMiddleware<CommonResponseMiddlewares>();  // Registering this middleware
            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseAuthentication();


            app.MapControllers();

            app.Run();
        }
    }
}
