
using ePizza.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ePizza.Repository.Contracts;
using ePizza.Models.Response;
using ePizza.Models.Request;
using ePizza.Core.Mapper;
using AutoMapper;
using epizza.Domain.Models;


namespace ePizza.Core.Concrete
{
    public class UserServices : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRolesRepository _rolesRepository;
        private readonly IMapper _mapper;

        public UserServices(IUserRepository userRepository,IRolesRepository rolesRepository,IMapper mapper)
        {
            _userRepository = userRepository;
            _rolesRepository = rolesRepository;
            _mapper = mapper;
        }

        public bool AddUser(CreateUserRequest userRequest)
        {
            var roles = _rolesRepository.GetAll().Where(x=>x.Id==2).FirstOrDefault();
            var userDetails = _mapper.Map <User>(userRequest);
            userDetails.Roles.Add(roles);
            userDetails.Password = BCrypt.Net.BCrypt.HashPassword(userDetails.Password);
            _userRepository.Add(userDetails);
           int rowsaffected= _userRepository.Commitchanges();
            return rowsaffected>0;
        }

        public IEnumerable<UserResponseModel> GetAllUsers()
        {
            var users = _userRepository.GetAll().AsEnumerable();
           var userresponse=_mapper.Map<IEnumerable<UserResponseModel>>(users);
            return userresponse;
            
           // return users.ConvertToUserResponseModel();
        }
    }
}
