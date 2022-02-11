using AutoMapper;
using GetConnection.Core.Config;
using GetConnection.Core.Entities;
using GetConnection.Core.Repositories.IssueComplains;
using GetConnection.Infrastructure.Context;
using GetConnection.Infrastructure.Repository.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetConnection.Infrastructure.Repository.IssueComplains
{
    public class IssueComplainWriteOnlyRepository : Repository<GetConnection.Core.Entities.IssueComplain>, IIssueComplainWriteOnlyRepository
    {
        private IConfiguration _configuration;
        private GetConnectionContext _getConnection;
        private readonly IOptions<MKConfiguration> _options;
        private readonly IMapper _mapper;
        public IssueComplainWriteOnlyRepository(IMapper mapper, IOptions<MKConfiguration> options, GetConnectionContext getConnection, GetConnectionContext getConnectionContext, IConfiguration configuration) : base(getConnectionContext)
        {
            _getConnection = getConnection;
            _configuration = configuration;
            _options = options;
            _mapper = mapper;
        }

        public Task<bool>AddIssueIamge(IssueImage data)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_options.Value.EmployeeDB))
                {

                    conn.Open();
                    SqlCommand cmd = new SqlCommand("AddIssueImage", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@IssueId", SqlDbType.BigInt).Value = data.IssueId;
                    cmd.Parameters.Add("@ImagePath", SqlDbType.VarChar).Value = data.ImagePath;
                    using (var rdr = cmd.ExecuteReader())
                    {

                        return Task.FromResult(true);
                    }
                }
            }
            catch(Exception ex)
            {

                return Task.FromResult(false);

            }
        }
        public Task<IssueComplain> AddComplainIssue(IssueComplain data)
        {
            
            
                try
                {
                    using (var db = new Context.GetConnectionContext(_configuration))
                    {

                        data.ComplainDateTime = DateTime.Now;
                        var entityInDb = db.IssueComplain.Add(data);

                        db.SaveChanges();

                        return Task.FromResult(data);
                    }
                }

                catch (Exception ex)
                {
                    IssueComplain re = new IssueComplain();
                    return Task.FromResult(re);
                }

            

        }

        public  Task<bool> UpdateComplainIssue(IssueComplain data)
        {


            try
            {

                using (SqlConnection conn = new SqlConnection(_options.Value.EmployeeDB))
                {

                    conn.Open();
                    SqlCommand cmd = new SqlCommand("UpdateIssueComplain", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.BigInt).Value = data.Id;
                    cmd.Parameters.Add("@DeparmentId", SqlDbType.Int).Value = data.DeparmentId;
                    cmd.Parameters.Add("@IssueTypeID", SqlDbType.Int).Value = data.IssueTypeID;
                    cmd.Parameters.Add("@EmployeeId", SqlDbType.Int).Value = data.EmployeeId;
                    cmd.Parameters.Add("@StatusType", SqlDbType.Int).Value = data.StatusType;
                    cmd.Parameters.Add("@UserDescription", SqlDbType.VarChar).Value = data.UserDescription;    
                    cmd.Parameters.Add("@ComplainDateTime", SqlDbType.DateTime).Value =DateTime.Now;

                    using (var rdr = cmd.ExecuteReader())
                    {
                       
                        return Task.FromResult(true);
                    }
                }
            }

            catch (Exception ex)
            {
            
                return  Task.FromResult(false);
            }



        }
    } 
} 