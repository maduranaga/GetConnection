using GetConnection.Core.Config;
using GetConnection.Core.Entities;
using GetConnection.Core.Repositories.Users;
using GetConnection.Infrastructure.Context;
using GetConnection.Infrastructure.Repository.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace GetConnection.Infrastructure.Repository.Users
{
    public class UsersWriteOnlyRepository : Repository<GetConnection.Core.Entities.User>, IUsersWriteOnlyRepository
    {

        private IConfiguration _configuration;
        private readonly GetConnectionContext _context;
        private readonly IOptions<MKConfiguration> _options;
        public UsersWriteOnlyRepository(IOptions<MKConfiguration> options, GetConnectionContext context, GetConnectionContext getConnectionContext, IConfiguration configuration) : base(getConnectionContext)
        {
            _context = context;
            _configuration = configuration;
            _options = options;
        }

        public Task<User> CreateUser(User user)
        {

            try
            {
                using (var db = new Context.GetConnectionContext(_configuration))
                {
                    var entityInDb = db.User.Add(user);

                    db.SaveChanges();

                    return Task.FromResult(user);
                }
            }
            catch (Exception ex)
            {
                User re = new User();
                return Task.FromResult(re);
            }

        }


        public Task<DeviceToken> InsertToken(DeviceToken data)
        {

            try
            {
                using (SqlConnection conn = new SqlConnection(_options.Value.EmployeeDB))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("InsertDeviceToken", conn);
                    cmd.Parameters.Add("@UserId", SqlDbType.BigInt).Value = data.UserId;
                    cmd.Parameters.Add("@DeviceToken", SqlDbType.VarChar).Value = data.UserDeviceToken;
                    cmd.Parameters.Add("@DateTime", SqlDbType.DateTime).Value =DateTime.Now;
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rdr = cmd.ExecuteReader())
                    {
                        DeviceToken result = new DeviceToken();
                        while (rdr.Read())
                        {
                            result.UserId = rdr.GetInt64(1);
                            result.UserDeviceToken = (string)rdr.GetString(0);
                            result.DateTime = rdr.GetDateTime(3);
                        }
                        return Task.FromResult(result);
                    }
                }
            }
            catch (Exception ex)
            {
                DeviceToken re = new DeviceToken();
                return Task.FromResult(re);
            }
        }

        public Task<bool> RemoveOlderToken()
        {

            try
            {
                using (SqlConnection conn = new SqlConnection(_options.Value.EmployeeDB))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("RemoveOlderDeviceToken", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rdr = cmd.ExecuteReader())
                    {
                        return Task.FromResult(true);
                    }

                }
            }
            catch (Exception ex)
            {

                return Task.FromResult(false);
            }

        }

        public Task<bool> OtpCodeSave(string Email, string code)
        {

            try
            {
                using (var db = new Context.GetConnectionContext(_configuration))
                {
                    var res = db.User.Where(a => a.Email == Email).FirstOrDefault();
                    res.OtpCode = code;
                    var entityInDb = db.User.Update(res);

                    db.SaveChanges();

                    return Task.FromResult(true);
                }
            }
            catch (Exception ex)
            {
                User re = new User();
                return Task.FromResult(false);
            }

        }
        public Task<bool> SaveNewPassword(string paswd, string sltKey, string Email)
        {

            try
            {
                using (SqlConnection conn = new SqlConnection(_options.Value.EmployeeDB))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SaveNewPassword", conn);
                    cmd.Parameters.Add("@Pswd", SqlDbType.VarChar).Value = paswd;
                    cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = Email;
                    cmd.Parameters.Add("@Salt", SqlDbType.VarChar).Value = sltKey;
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rdr = cmd.ExecuteReader())
                    {
                        return Task.FromResult(true);
                    }

                }
            }
            catch (Exception ex)
            {

                return Task.FromResult(false);
            }

        }

        public Task<bool> RemoveDeviceToken(string dviceId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_options.Value.EmployeeDB))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("DeleteDeviceToken", conn);
                    cmd.Parameters.Add("@DviceId", SqlDbType.VarChar).Value = dviceId;
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rdr = cmd.ExecuteReader())
                    {
                        return Task.FromResult(true);
                    }

                }
            }
            catch(Exception Ex)
            {

                return Task.FromResult(false);

            }
        }
        public Task<User> updateUser(User user)
        {

            try
            {
                using (SqlConnection conn = new SqlConnection(_options.Value.EmployeeDB))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("UpdateUser", conn);
                    cmd.Parameters.Add("@Id", SqlDbType.VarChar).Value = user.Id;
                    cmd.Parameters.Add("@Gender", SqlDbType.VarChar).Value = user.Gender;
                    cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = user.EmployeeName;
                    cmd.Parameters.Add("@ProfileImage", SqlDbType.VarChar).Value = user.ProfileImage;

                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rdr = cmd.ExecuteReader())
                    {
                        User result = new User();
                        while (rdr.Read())
                        {


                            
                                result.Id = (long)rdr.GetInt64(0);
                                result.Email = (string)rdr.GetString(1);
                                result.EmployeeName = (string)rdr.GetString(2);
                                result.Gender = (string)rdr.GetString(9);
                                result.ProfileImage = (string)rdr.GetString(10);
                            
                        }

                        return Task.FromResult(result);

                    }

                }
            }
            catch (Exception ex)
            {
                User re = new User();
                return Task.FromResult(re);
            }



        }
    }
}