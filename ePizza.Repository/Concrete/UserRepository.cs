
using epizza.Domain.Models;
using ePizza.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ePizza.Repository.Concrete
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        // add
        // delete 
        // get 
        // update
        public UserRepository(epizzaHubDBContext dbContext) : base(dbContext)
        {
        }

        public User findUser(string emailAdress)
        {
            return _dbContext.Users.Include(
            x => x.Roles)
               .Where(x => x.Email == emailAdress).FirstOrDefault()!;
        }

        public bool PersistUserTokens(UserToken userToken)
        {
            var existingToken = _dbContext.UserTokens.FirstOrDefault(x => x.UserId == userToken.UserId);
            if (existingToken != null)
            {
                existingToken.AccessToken = userToken.AccessToken;
                existingToken.RefreshToken = userToken.RefreshToken;
                _dbContext.Entry(existingToken).State = EntityState.Modified;
            }
            else
            {
                _dbContext.UserTokens.Add(userToken);

            }
            int rowsAffected = _dbContext.SaveChanges();
            return rowsAffected > 0;
        }

        public UserToken GetUserToken(int userId)
        {
            return _dbContext.UserTokens.FirstOrDefault(x => x.UserId == userId);
        }
    }
}
