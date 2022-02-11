using AutoMapper;
using GetConnection.Core.Config;
using GetConnection.Core.Entities;
using GetConnection.Core.Repositories.SuportTokens;
using GetConnection.Core.ResponseEntity;
using GetConnection.Core.Services;
using GetConnection.Infrastructure.Context;
using GetConnection.Infrastructure.Repository.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetConnection.Infrastructure.Repository.SuportTokens
{
    public class SuportTokenReadOnlyRepository : Repository<GetConnection.Core.Entities.SupportToken>, ISuportTokenReadOnlyRepository
    {

        private IConfiguration _configuration;
        private GetConnectionContext _getConnection;
        private readonly ISqlHelper _sqlHelper;
        private readonly IOptions<MKConfiguration> _options;
        private readonly IMapper _mapper;
        public SuportTokenReadOnlyRepository(IMapper mapper,IOptions<MKConfiguration> options, ISqlHelper sqlHelper, GetConnectionContext getConnection, GetConnectionContext getConnectionContext, IConfiguration configuration) : base(getConnectionContext)
        {
            _getConnection = getConnection;
            _configuration = configuration;
            _options = options;
            _mapper = mapper;
        }
        public async Task<List<SuportTokenGetById>> GetSuportTokenByID(long Id)
        {

            try
            {
                using (SqlConnection conn = new SqlConnection(_options.Value.EmployeeDB))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("GetSuportTokenById", conn);
                    cmd.Parameters.Add("@IssueId", SqlDbType.Int).Value =Id;
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rdr = cmd.ExecuteReader())
                    {
                        List<SuportTokenGetById> result = new List<SuportTokenGetById>();
                        while (rdr.Read())
                        {
                            result.Add(new SuportTokenGetById()
                            {
                              //  IssueId = (SqlInt32)row["ID"],
                                IssueId = (int)rdr.GetInt64(0),
                                Text = rdr.GetString(1),
                                Type=rdr.GetInt32(2),
                                Time=rdr.GetDateTime(3),
                                USerId=rdr.GetInt64(5),
                                UserName=rdr.GetString(4),
                                USerType=rdr.GetInt32(6),
                                Attachment1Path=rdr.GetString(7),
                                Attachment2Path=rdr.GetString(8),
                                Attachment3Path=rdr.GetString(9)
                            });
                        }
                        return await Task.FromResult(result);
                    }
                }
            }
            catch (Exception ex)
            {
                List<SuportTokenGetById> re = new List<SuportTokenGetById>();
                return await Task.FromResult(re);
            }

           

        }
    }
}