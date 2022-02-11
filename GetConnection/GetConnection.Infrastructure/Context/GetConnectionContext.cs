using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetConnection.Infrastructure.Context
{
   
    public class GetConnectionContext  : DbContext
    {

        protected readonly IConfiguration Configuration;
      
        public GetConnectionContext(IConfiguration configuration)
        {
            Configuration = configuration;
       
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server with connection string from app settings
            options.UseSqlServer(Configuration.GetConnectionString("EmployeeDB"));
        }

        // public AreaContext(DbContextOptions<AreaContext> options) : base(options) { }
      

        public DbSet<GetConnection.Core.Entities.Area> Area {get; set;}

        public DbSet<GetConnection.Core.Entities.Organiztion> Organiztion  { get; set; }

        public DbSet<GetConnection.Core.Entities.User> User  { get; set; }

        public DbSet<GetConnection.Core.Entities.DeviceToken> DeviceToken  { get; set; }

        public DbSet<GetConnection.Core.Entities.IssueComplain> IssueComplain { get; set; }

        public DbSet<GetConnection.Core.Entities.IssueType> IssueType { get; set; }

        public DbSet<GetConnection.Core.Entities.SupportToken> SupportToken { get; set; }

        public DbSet<GetConnection.Core.Entities.IssueImage> IssueImage { get; set; }

        public DbSet<GetConnection.Core.Entities.SuportTokenAttachemnt> SuportTokenAttachemnt { get; set; }

        public DbSet<GetConnection.Core.Entities.FirbaseNotification> FirbaseNotification { get; set; }

    }

}
