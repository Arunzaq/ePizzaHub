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
    public class AuthServices :IAuthServices
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
            var userDetails = _userRepository.findUser(username);
            if (userDetails != null)
            {
                bool isvalidpassword = BCrypt.Net.BCrypt.Verify(password ,userDetails.Password );
                if (isvalidpassword)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
