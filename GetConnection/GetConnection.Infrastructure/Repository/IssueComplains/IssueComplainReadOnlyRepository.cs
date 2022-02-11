using GetConnection.Core.Config;
using GetConnection.Core.Entities;
using GetConnection.Core.Repositories.IssueComplains;
using GetConnection.Core.ResponseEntity;
using GetConnection.Infrastructure.Context;
using GetConnection.Infrastructure.Repository.Base;
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
    public class IssueComplainReadOnlyRepository : Repository<GetConnection.Core.Entities.IssueComplain>, IIssueComplainReadOnlyRepository
    {
        private IConfiguration _configuration;
        private GetConnectionContext _getConnection;
        private readonly IOptions<MKConfiguration> _options;
        public IssueComplainReadOnlyRepository(IOptions<MKConfiguration> options,GetConnectionContext getConnection, GetConnectionContext getConnectionContext, IConfiguration configuration) : base(getConnectionContext)
        {
            _getConnection = getConnection;
            _configuration = configuration;
            _options = options;
        }

        public Task<List<IssueComplainFilter>> GetHrIssue(int limit, int page, string sort, int order, string[] status, string query, long UserId)
        {
            try { 

               using (SqlConnection conn = new SqlConnection(_options.Value.EmployeeDB))
                    {
                    string sta = "";
                    if(status!=null)
                    {
                        for(int x=0; x<status.Length;x++)
                        {
                            if (x+1==status.Length) { sta = sta + status[x]; }
                            else { sta = sta + status[x] + ","; }
                          
                        }
                    }
                    else
                    {
                        sta = "1,2,3,4,5,-1";
                    }
                    if (query==null) { query = ""; }
                      conn.Open();
                      SqlCommand cmd = new SqlCommand("GetHRissuesUsingOption", conn);
                      cmd.CommandType = CommandType.StoredProcedure;
                      cmd.Parameters.Add("@limit", SqlDbType.Int).Value=limit;
                      cmd.Parameters.Add("@page", SqlDbType.Int).Value = page;
                      cmd.Parameters.Add("@sort", SqlDbType.VarChar).Value = sort;
                      cmd.Parameters.Add("@order", SqlDbType.Int).Value = order;
                      cmd.Parameters.Add("@status", SqlDbType.VarChar).Value = sta;
                      cmd.Parameters.Add("@query", SqlDbType.VarChar).Value = query;
                      cmd.Parameters.Add("@userId", SqlDbType.BigInt).Value = UserId;

                    using (var rdr = cmd.ExecuteReader())
                    {
                        List<IssueComplainFilter> result = new List<IssueComplainFilter>();

                        while (rdr.Read())
                        {
                            result.Add(new IssueComplainFilter()
                         {
                          
                                Id = rdr.GetInt64(0),
                                DeparmentId=rdr.GetInt32(1),
                                IssueTypeID=rdr.GetInt32(2),
                                EmployeeId=rdr.GetInt64(3),
                                StatusType=rdr.GetInt32(4),
                                UserDescription=rdr.GetString(5),
                                ComplainDateTime=rdr.GetDateTime(6),
                                RowNumber = rdr.GetInt64(7)



                            });
                        }
                        return Task.FromResult(result);
                    }
                }
            }

            catch (Exception ex)
                  {
                           List<IssueComplainFilter> re = new List<IssueComplainFilter>();
                           return Task.FromResult(re);
                  }


        }

        public Task<List<IssueComplainFilter>> GetMaintainIssue(int limit, int page, string sort, int order, string[] status, string query,long userID)
        {
            try
            {

                using (SqlConnection conn = new SqlConnection(_options.Value.EmployeeDB))
                {
                

                    string sta = "";
                    if (status != null)
                    {
                        for (int x = 0; x < status.Length; x++)
                        {
                            if (x + 1 == status.Length) { sta = sta + status[x]; }
                            else { sta = sta + status[x] + ","; }

                        }
                    }
                    else
                    {
                        sta = "1,2,3,4,5,-1";
                    }
                    if (query==null) { query = ""; }

                    conn.Open();
                    SqlCommand cmd = new SqlCommand("GetMaintainissuesUsingOption", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@limit", SqlDbType.Int).Value = limit;
                    cmd.Parameters.Add("@page", SqlDbType.Int).Value = page;
                    cmd.Parameters.Add("@sort", SqlDbType.VarChar).Value = sort;
                    cmd.Parameters.Add("@order", SqlDbType.Int).Value = order;
                    cmd.Parameters.Add("@status", SqlDbType.VarChar).Value = sta;
                    cmd.Parameters.Add("@query", SqlDbType.VarChar).Value = query;
                    cmd.Parameters.Add("@userId", SqlDbType.BigInt).Value = userID;

                    using (var rdr = cmd.ExecuteReader())
                    {
                        List<IssueComplainFilter> result = new List<IssueComplainFilter>();

                        while (rdr.Read())
                        {
                            result.Add(new IssueComplainFilter()
                            {

                                Id = rdr.GetInt64(0),
                                DeparmentId = rdr.GetInt32(1),
                                IssueTypeID = rdr.GetInt32(2),
                                EmployeeId = rdr.GetInt64(3),
                                StatusType = rdr.GetInt32(4),
                                UserDescription = rdr.GetString(5),
                                ComplainDateTime = rdr.GetDateTime(6),
                                RowNumber=rdr.GetInt64(7)



                            });
                        }
                        return Task.FromResult(result);
                    }
                }
            }

            catch (Exception ex)
            {
                List<IssueComplainFilter> re = new List<IssueComplainFilter>();
                return Task.FromResult(re);
            }


        }

        public  Task<IssuCplainById> GetHRissueById(long id)
        {

            try
            {
                using (SqlConnection conn = new SqlConnection(_options.Value.EmployeeDB))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("GetIssueComaplinById", conn);
                    cmd.Parameters.Add("@IssueCompalinId", SqlDbType.BigInt).Value = id;
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rdr = cmd.ExecuteReader())
                    {
                        List<IssuCplainById> res  = new List<IssuCplainById>();
                        while (rdr.Read())
                        {
                            res.Add(new IssuCplainById()
                            {
                            
                              
                                Name =rdr.GetString(0),
                                UserDescription = rdr.GetString(1),
                                Id = rdr.GetInt64(2),
                                StatusType = rdr.GetInt32(3)


                            });
                        }
                        return  Task.FromResult(res[0]);
                    }
                }
            }
            catch (Exception ex)
            {
               IssuCplainById re = new IssuCplainById();
                return  Task.FromResult(re);
            }


        }


        public  Task<List<IssueImage>> getImagesByIssueId(long issueId)
        {

            try
            {
                using (var db = new Context.GetConnectionContext(_configuration))
                {
                    var entityInDb = db.IssueImage.Where(r => r.IssueId == issueId).ToList();

                    return  Task.FromResult(entityInDb);
                }
            }
            catch (Exception ex)
            {
              List<IssueImage>re = new List<IssueImage>();
                return Task.FromResult(re);
            }

        }

        public Task<List<IssueComplainFilter>> GetAllIssueComaplinIssue(int limit, int page, string sort, int order, string[] status, string query, long userID)
        {
            try
            {

                using (SqlConnection conn = new SqlConnection(_options.Value.EmployeeDB))
                {


                    string sta = "";
                    if (status != null)
                    {
                        for (int x = 0; x < status.Length; x++)
                        {
                            if (x + 1 == status.Length) { sta = sta + status[x]; }
                            else { sta = sta + status[x] + ","; }

                        }
                    }
                    else
                    {
                        sta = "1,2,3,4,5,-1";
                    }
                    if (query == null) { query = ""; }

                    conn.Open();
                    SqlCommand cmd = new SqlCommand("GetAllIssueComplinOption", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@limit", SqlDbType.Int).Value = limit;
                    cmd.Parameters.Add("@page", SqlDbType.Int).Value = page;
                    cmd.Parameters.Add("@sort", SqlDbType.VarChar).Value = sort;
                    cmd.Parameters.Add("@order", SqlDbType.Int).Value = order;
                    cmd.Parameters.Add("@status", SqlDbType.VarChar).Value = sta;
                    cmd.Parameters.Add("@query", SqlDbType.VarChar).Value = query;
                    cmd.Parameters.Add("@userId", SqlDbType.BigInt).Value = userID;

                    using (var rdr = cmd.ExecuteReader())
                    {
                        List<IssueComplainFilter> result = new List<IssueComplainFilter>();

                        while (rdr.Read())
                        {
                            result.Add(new IssueComplainFilter()
                            {

                                Id = rdr.GetInt64(0),
                                DeparmentId = rdr.GetInt32(1),
                                IssueTypeID = rdr.GetInt32(2),
                                EmployeeId = rdr.GetInt64(3),
                                StatusType = rdr.GetInt32(4),
                                UserDescription = rdr.GetString(5),
                                ComplainDateTime = rdr.GetDateTime(6),
                                RowNumber = rdr.GetInt64(7)



                            });
                        }
                        return Task.FromResult(result);
                    }
                }
            }

            catch (Exception ex)
            {
                List<IssueComplainFilter> re = new List<IssueComplainFilter>();
                return Task.FromResult(re);
            }


        }
    }
}
 