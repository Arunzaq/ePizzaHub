using AutoMapper;
using ePizza.Core.Contracts;
using ePizza.Models.Response;
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

        public ValidateUserResponse validateUser(string username, string password)
        {
            var userDetails = _userRepository.findUser(username);
            if (userDetails != null)
            {
                bool isvalidpassword = BCrypt.Net.BCrypt.Verify(password ,userDetails.Password );
                if (isvalidpassword)
                {
                    return new ValidateUserResponse()
                    {
                        Email = username,
                        Name = userDetails.Name,
                        Roles = userDetails.Roles.Select(x => x.Name).ToList()
                    };
                }
            }
            return new ValidateUserResponse();
        }
    }
}
