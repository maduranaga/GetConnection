using GetConnection.Core.Config;
using GetConnection.Core.Entities;
using GetConnection.Core.Repositories.Organiztions;
using GetConnection.Core.Services;
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

namespace GetConnection.Infrastructure.Repository.Organiztions
{
    public class OrganiztionReadOnlyRepository : Repository<GetConnection.Core.Entities.Organiztion>,IOrganiztionReadOnlyRepository
    {

        private IConfiguration _configuration;
        private GetConnectionContext _getConnection;
        private readonly ISqlHelper _sqlHelper;
        private readonly IOptions<MKConfiguration> _options;
        public OrganiztionReadOnlyRepository(IOptions<MKConfiguration> options,ISqlHelper sqlHelper,GetConnectionContext getConnection, GetConnectionContext getConnectionContext, IConfiguration configuration) : base(getConnectionContext)
        {
            _getConnection = getConnection;
            _configuration = configuration;
            _options = options;
        }

        public Task<List<Organiztion>> GetAllOrganiztion()
        {
            /*string connetionString;
            SqlConnection connection;
            connetionString = _options.Value.EmployeeDB;
            connetionString = _options.Value.EmployeeDB;
            connection = new SqlConnection(connetionString);*/

            //var sqlConnection = _sqlHelper.GetSQLConnection();
            //List<Organiztion> dat = new List<Organiztion>();
            try
            {
                using (SqlConnection conn = new SqlConnection(_options.Value.EmployeeDB))
                {

                    conn.Open();

                    // 1.  create a command object identifying the stored procedure
                    SqlCommand cmd = new SqlCommand("getData", conn);
                  

                    // 2. set the command object so it knows to execute a stored procedure
                    cmd.CommandType = CommandType.StoredProcedure;

                   // cmd.Parameters.Add("@Id", SqlDbType.Int).Value=5;
                    // execute the command
                    using (var rdr = cmd.ExecuteReader())
                    {
                        List<Organiztion> result = new List<Organiztion>();
                        //3. Loop through rows
                        while (rdr.Read())
                        {
                            //Get each column
                            result.Add(new Organiztion() { 
                                OrganizationName = (string)rdr.GetString(1),
                                Id=rdr.GetInt32(0)
                            });
                        }
                        return Task.FromResult(result);
                    }
                }
            }

            catch (Exception ex)
            {
                List<Organiztion> re  = new List<Organiztion>();
                return Task.FromResult(re);
            }

        }
    }
}
