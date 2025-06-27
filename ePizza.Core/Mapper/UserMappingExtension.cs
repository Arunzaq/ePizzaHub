using epizza.Domain.Models;
using ePizza.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizza.Core.Mapper
{
    public static class UserMappingExtension
    {
        public static IEnumerable<UserResponseModel> ConvertToUserResponseModel(
            this IEnumerable<User> UserList)
        {
            List<UserResponseModel> userResponseModelsList = new List<UserResponseModel>();
            if (UserList.Any())
            {
            
                foreach (User user in UserList)
                {
                    UserResponseModel userResponseModel = new UserResponseModel()
                    {
                        Id = user.Id,
                        Name = user.Name,
                        PhoneNumber = user.PhoneNumber,
                        Email = user.Email,
                        EmailConfirmed = user.EmailConfirmed,
                        Password = user.Password,
                        CreatedDate = user.CreatedDate,
                    };
                    userResponseModelsList.Add(userResponseModel);
                }
            }
            return userResponseModelsList;
        }
    }
}
