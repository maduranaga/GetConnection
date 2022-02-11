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
   public  class FirbaseNotificationReadOnlyRepository: Repository<GetConnection.Core.Entities.FirbaseNotification>, IFirbaseNotificationReadOnlyRepository
    {
        private IConfiguration _configuration;
        private GetConnectionContext _getConnection;
        private readonly IOptions<MKConfiguration> _options;
        public FirbaseNotificationReadOnlyRepository(IOptions<MKConfiguration> options, GetConnectionContext getConnection, GetConnectionContext getConnectionContext, IConfiguration configuration) : base(getConnectionContext)
        {
            _getConnection = getConnection;
            _configuration = configuration;
            _options = options;
        }

        public async Task<List<FirbaseNotification>> GetNotifications(long Id)
        {

            try
            {
                using (SqlConnection conn = new SqlConnection(_options.Value.EmployeeDB))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("getNotifications", conn);
                    cmd.Parameters.Add("@userID", SqlDbType.BigInt).Value = Id;
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rdr = cmd.ExecuteReader())
                    {
                        List<FirbaseNotification> result = new List<FirbaseNotification>();
                        while (rdr.Read())
                        {
                            result.Add(new FirbaseNotification()
                            {
                                
                                Id = (int)rdr.GetInt64(0),
                                Title = rdr.GetString(1),
                                DeviceToken = rdr.GetString(2)

                                
                            });
                        }
                        return await Task.FromResult(result);
                    }
                }
            }
            catch (Exception ex)
            {
                List<FirbaseNotification> re = new List<FirbaseNotification>();
                return await Task.FromResult(re);
            }
        }
    }
}
