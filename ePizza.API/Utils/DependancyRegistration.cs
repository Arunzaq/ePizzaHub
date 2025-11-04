using System.Text;
using ePizza.Core.Concrete;
using ePizza.Core.Contracts;
using ePizza.Core.Utils;
using ePizza.Repository.Concrete;
using ePizza.Repository.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace ePizza.API.Utils
{
    public static class DependancyRegistration
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<TokenGenerator>();

            services.AddTransient<IAuthServices, AuthServices>();
            services.AddTransient<IUserService, UserServices>(); //Registering dependancies
            services.AddTransient<IItemServices, ItemServices>();
            services.AddTransient<ICartRepository, CartRepository>();
            services.AddTransient<ICartServices, CartServices>();

            return services;
        }

        public static IServiceCollection RegisterDependancies(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRolesRepository, RoleRepository>();
            services.AddScoped<IItemRepository, ItemRepository>();

            return services;
        }

        public static IServiceCollection Registerjwt(this IServiceCollection services,IConfiguration configuration)
        {
               services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(Options =>
               {
                   Options.RequireHttpsMetadata = false;
                   Options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                   {
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]!)),
                       ValidIssuer = configuration["Jwt:Issuer"],
                       ValidAudience = configuration["Jwt:Audience"],
                       ClockSkew = TimeSpan.FromMinutes(10)
                   };
               });

            return services;
        }
    }
}
