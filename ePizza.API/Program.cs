
using epizza.Domain.Models;
using ePizza.API.Middlewares;
using ePizza.API.Utils;
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

            builder.Services.RegisterDependancies().RegisterServices().Registerjwt(builder.Configuration);
           
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
