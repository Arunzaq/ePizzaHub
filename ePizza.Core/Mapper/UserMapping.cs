using AutoMapper;
using epizza.Domain.Models;
using ePizza.Models.Request;
using ePizza.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizza.Core.Mapper
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<User, UserResponseModel>().ReverseMap();
            CreateMap<CreateUserRequest, User>();
        }
    }
}
