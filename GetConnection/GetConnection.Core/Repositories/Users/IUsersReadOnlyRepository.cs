using GetConnection.Core.Entities;
using GetConnection.Core.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetConnection.Core.Repositories.Users
{
    public interface IUsersReadOnlyRepository:IRepository<GetConnection.Core.Entities.User>
    {
        public Task<User> userLogin(string mail, string hash  );
       // public Task<User> userDetailByUserID(long userId);

        public Task<bool> otpCheck(string mail,string otp);

        public Task<User> getUserByEmail(string mail);
        public Task<User> getById(long id);

       
        //public Task<User> GetUserByID();




    }
}
