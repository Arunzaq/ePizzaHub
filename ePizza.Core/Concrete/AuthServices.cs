using AutoMapper;
using epizza.Domain.Models;
using ePizza.Core.Contracts;
using ePizza.Core.CustomException;
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

        public UserTokenModel GetSavedTokenDetail(string userName)
        {
            var user = GetUserDetails(userName);

            var userToken = _userRepository.GetUserToken(user.Id);

            return _mapper.Map<UserTokenModel>(userToken);
        }

        public UserResponseModel GetUserDetails(string userName)
        {
            var userDetails = _userRepository.findUser(userName);

            if (userDetails == null)
                throw new RecordNotFoundException($"No user found in database against User with email as {userName}");

            return _mapper.Map<UserResponseModel>(userDetails);
        }

        public bool PersistUserToken(UserTokenModel userTokenModel)
        {
            var token = _mapper.Map<UserToken>(userTokenModel);

            return _userRepository.PersistUserTokens(token);
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
                        UserId= userDetails.Id,
                        Roles = userDetails.Roles.Select(x => x.Name).ToList()
                    };
                }
            }
            return new ValidateUserResponse();
        }
    }
}
