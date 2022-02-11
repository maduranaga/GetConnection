using AutoMapper;
using GetConnection.Core.Entities;
using GetConnection.Core.Repositories.Users;
using GetConnection.Infrastructure.Context;
using GetConnection.Infrastructure.Repository.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetConnection.Infrastructure.Repository.Users
{
    public class UsersReadOnlyRepository : Repository<GetConnection.Core.Entities.User>, IUsersReadOnlyRepository
    {

        private IConfiguration _configuration;
        private readonly IMapper _mapper;
        public UsersReadOnlyRepository(IMapper mapper,GetConnectionContext getConnectionContext, IConfiguration configuration) : base(getConnectionContext)
        {

            _configuration = configuration;
            _mapper = mapper;
        }

        public Task<User> userLogin(string mail, string hash )
        {

            try
            {
                using (var db = new Context.GetConnectionContext(_configuration))
                {
                    var entityInDb = db.User.Where(r=>r.Email==mail && r.HashToken == hash).FirstOrDefault();

                    db.SaveChanges();

                    return Task.FromResult(entityInDb);
                }
            }
            catch (Exception ex)
            {
                User re = new User();
                return Task.FromResult(re);
            }

        }

        public Task<bool> otpCheck(string mail, string otp )
        {
            try
            {

                using (var db = new Context.GetConnectionContext(_configuration))
                {
                    var entityInDb = db.User.Where(r => r.Email == mail && r.OtpCode == otp).FirstOrDefault();

                    db.SaveChanges();

                    if (entityInDb !=null) { return Task.FromResult(true); }
                    else { return Task.FromResult(false); }
                   
                }

            }
            catch(Exception ex)
            {
               
                return Task.FromResult(false);
            }
        }
        public Task<User> getUserByEmail(string mail)
        {

            try
            {
                using (var db = new Context.GetConnectionContext(_configuration))
                {
                    var entityInDb = db.User.Where(r => r.Email == mail).FirstOrDefault();

                    db.SaveChanges();
                    if (entityInDb !=null)
                    {
                        return Task.FromResult(entityInDb);
                    }
                    else
                    {
                        User re = new User();
                        re.Email = null;
                        return Task.FromResult(re);
                    }
                }
            }
            catch (Exception ex)
            {
                User re = new User();
                return Task.FromResult(re);
            }

        }

        public async Task<User> getById(long id)
        {

            try
            {
                using (var db = new Context.GetConnectionContext(_configuration))
                {
                    var entityInDb = await db.User.FirstAsync(r => r.Id == id);             
                    return await Task.FromResult(_mapper.Map<User>(entityInDb));
                }
            }
            catch (Exception ex)
            {
                User re = new User();
                return await Task .FromResult(re);
            }

        }
        public async Task<User> GetUserByID(long Id)
        {

            try
            {
                using (var db = new Context.GetConnectionContext(_configuration))
                {
                    var entityInDb = await db.User.FirstAsync(r => r.Id == Id);
                    return await Task.FromResult(_mapper.Map<User>(entityInDb));
                }
            }
            catch (Exception ex)
            {
                User re = new User();
                return await Task.FromResult(re);
            }
        }
    }
}
