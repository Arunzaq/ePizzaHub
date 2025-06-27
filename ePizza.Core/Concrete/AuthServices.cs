using AutoMapper;
using ePizza.Core.Contracts;
using ePizza.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizza.Core.Concrete
{
    internal class AuthServices :IAuthServices
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AuthServices(IUserRepository userRepository,IMapper mapper) 
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public bool validateUser(string username, string password)
        {
           
        }
    }
}
