using AutoMapper;
using GetConnection.Core.Config;
using GetConnection.Core.Entities;
using GetConnection.Core.Repositories.FirbaseNotifications;
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

namespace GetConnection.Infrastructure.Repository.FirbaseNotifications
{
    public class FirbaseNotificationWriteOnlyRepository : Repository<GetConnection.Core.Entities.FirbaseNotification>, IFirbaseNotificationWriteOnlyRepository
    {
        private IConfiguration _configuration;
        private GetConnectionContext _getConnection;
        private readonly IOptions<MKConfiguration> _options;
        private readonly IMapper _mapper;
        public FirbaseNotificationWriteOnlyRepository(IMapper mapper, IOptions<MKConfiguration> options, GetConnectionContext getConnection, GetConnectionContext getConnectionContext, IConfiguration configuration) : base(getConnectionContext)
        {
            _getConnection = getConnection;
            _configuration = configuration;
            _options = options;
            _mapper = mapper;
        }

        public Task<FirbaseNotification> AdNotification(FirbaseNotification data)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_options.Value.EmployeeDB))
                {

                    conn.Open();
                    SqlCommand cmd = new SqlCommand("AddNotification", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@devicetoken", SqlDbType.VarChar).Value = data.DeviceToken;
                    cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = data.Title;
                    cmd.Parameters.Add("@body", SqlDbType.VarChar).Value = data.Body;
                    cmd.Parameters.Add("@dateTime", SqlDbType.VarChar).Value = data.DateTime;

                    using (var rdr = cmd.ExecuteReader())
                    {
                        FirbaseNotification result = new FirbaseNotification();
                        while (rdr.Read())
                        {
                            result.Id = rdr.GetInt64(0);
                            result.Title = (string)rdr.GetString(1);
                            result.Body = (string)rdr.GetString(2);
                            result.DateTime = rdr.GetDateTime(3);
                                
                        }
                        return Task.FromResult(result);
                    }
                }
            }
            catch (Exception ex)
            {
                FirbaseNotification datares = new FirbaseNotification();
                return Task.FromResult(datares);

            }
        }
    }

}