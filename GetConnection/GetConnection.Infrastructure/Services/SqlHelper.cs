using GetConnection.Core.Config;
using GetConnection.Core.Services;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetConnection.Infrastructure.Services
{
    public class SqlHelper : ISqlHelper
    {
        private readonly IOptions<MKConfiguration> _options;

        public SqlHelper(IOptions<MKConfiguration> options)
        {
            _options = options;
        }

        public SqlConnection GetSQLConnection()
        {
            string connetionString;
            SqlConnection connection;
            connetionString = _options.Value.EmployeeDB;
            connection = new SqlConnection(connetionString);
            return connection;
        }
    }
}
