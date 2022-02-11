using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetConnection.Application.UseCases.Organiztions.InsertOraganization
{
    public class InsertOrganizationRequest
    {
        public int ID { get; set; }

        public string OrganizationName { get; set; }
    }
}
