using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetConnection.Core.Services
{
    public interface ISqlHelper
    {
        SqlConnection GetSQLConnection();
    }
}
