using GetConnection.Core.Entities;
using GetConnection.Core.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetConnection.Core.Repositories.Users
{
    public interface  IUsersWriteOnlyRepository: IRepository<GetConnection.Core.Entities.User>
    {
        public Task<User> CreateUser(User user);
        public Task<DeviceToken> InsertToken(DeviceToken data);
        public Task<bool> RemoveOlderToken();

        public Task<bool> OtpCodeSave(string Email, string code);

        public Task<bool> SaveNewPassword(string paswd, string sltKey,string Email);

        public Task<User> updateUser(User user);

        public Task<bool> RemoveDeviceToken(string dviceId);

        //  public Task<User> UpadteUser(User user);
        //public Task<bool> forgetPassword(string mail);


    }
}
