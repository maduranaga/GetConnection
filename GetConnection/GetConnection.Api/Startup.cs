
using AutoMapper;
using Azure.Storage.Blobs;
using FirebaseAdmin;
using GetConnection.Application.Mapping;
using GetConnection.Application.UseCases.Organiztions.GellAllOrganizations;
using GetConnection.Application.UseCases.Users.RegisterUser;
using GetConnection.Core.Config;
using GetConnection.Core.Helpers;
using GetConnection.Core.Repositories;
using GetConnection.Core.Repositories.Base;
using GetConnection.Core.Repositories.FirbaseNotifications;
using GetConnection.Core.Repositories.IssueComplains;
using GetConnection.Core.Repositories.IssueTypes;
using GetConnection.Core.Repositories.Organiztions;
using GetConnection.Core.Repositories.SuportTokens;
using GetConnection.Core.Repositories.Users;
using GetConnection.Core.Services;
using GetConnection.Infrastructure.Context;
using GetConnection.Infrastructure.Repository;
using GetConnection.Infrastructure.Repository.Base;
using GetConnection.Infrastructure.Repository.FirbaseNotifications;
using GetConnection.Infrastructure.Repository.IssueComplains;
using GetConnection.Infrastructure.Repository.IssueTypes;
using GetConnection.Infrastructure.Repository.Organiztions;
using GetConnection.Infrastructure.Repository.SuportTokens;
using GetConnection.Infrastructure.Repository.Users;
using GetConnection.Infrastructure.Services;
using Google.Apis.Auth.OAuth2;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GetConnection.Api
{
    public class Startup
    {
    
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //  var key = Encoding.ASCII.GetBytes(Configuration.GetValue<string>("SecretKey"));
            var key = Encoding.ASCII.GetBytes("chamarafvbghythgefrvgtfdsaerrtgb");
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            /*  var googleCredential = _hostingEnvironment.ContentRootPath;
              var filePath = Configuration.GetSection("GoogleFirebase")["fileName"];
              googleCredential = Path.Combine(googleCredential, filePath);
              var credential = GoogleCredential.FromFile(googleCredential);
              FirebaseApp.Create(new AppOptions()
              {

                  Credential = credential
              });
            */
            //services.AddAutoMapper(typeof(GetConnectionMappingProfile));

            services.AddScoped(x => new BlobServiceClient(Configuration.GetValue<string>("AzureBlobStorage")));
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new GetConnectionMappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddHostedService<CronService>();


            services.AddControllersWithViews();
            services.AddControllers();
           
            services.AddDbContext<GetConnectionContext>(m => m.UseSqlServer(Configuration.GetConnectionString("EmployeeDB")), ServiceLifetime.Singleton);
   
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Employee.API",
                    Version = "v1"
                });
            });
             services.AddAutoMapper(typeof(Startup));
            //services.Mapper(typeof(Startup));

           // services.AddMediatR(typeof(GetAreaHandler).GetTypeInfo().Assembly);
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
          //  services.AddTransient<IAreaRepository, AreaRepository>();
          //  services.AddScoped(typeof(IAreaRepository), typeof(AreaRepository));
            services.AddMediatR(Assembly.GetExecutingAssembly());


            /*-------UseCases-------*/         
                services.AddMediatR(typeof(GetAllOrganiztionsUseCase).GetTypeInfo().Assembly);
                services.AddMediatR(typeof(InsertUserUseCase).GetTypeInfo().Assembly);


            /* ----------services----*/
                services.AddTransient<IOrganiztionReadOnlyRepository,OrganiztionReadOnlyRepository>();
                services.AddTransient<IOrganiztionWriteOnlyRepository,OrganiztationWriteOnlyRepository>();
                services.AddTransient<IUsersWriteOnlyRepository, UsersWriteOnlyRepository>();
                services.AddTransient<IUsersReadOnlyRepository,UsersReadOnlyRepository>();
                services.AddTransient<HashPassword>();
                services.AddTransient<IEmailService,EmailService>();
                services.AddTransient<BearerTokenGenrate>();
                services.AddTransient<ValidateJwt>();

                services.AddTransient<ISqlHelper, SqlHelper>();
                services.AddTransient<GenrateOtp>();
                services.AddTransient<IIssueTypeReadOnlyRepository,IssueTypeReadOnlyRepository>();
                services.AddTransient<IIssueComplainReadOnlyRepository, IssueComplainReadOnlyRepository>();
                services.AddTransient<IIssueComplainWriteOnlyRepository, IssueComplainWriteOnlyRepository>();
                services.AddTransient<IBlobService, BlobService>();
                services.AddTransient<ISuportTokenWriteOnlyRepository,SuportTokenWriteOnlyRepository>();
                services.AddTransient<ISuportTokenReadOnlyRepository, SuportTokenReadOnlyRepository>();
            services.AddTransient<IFirbaseNotificationWriteOnlyRepository, FirbaseNotificationWriteOnlyRepository>();
                services.AddTransient<IPushNotificationLogic, PushNotificationLogic>();
               services.AddTransient<IFirbaseNotificationReadOnlyRepository, FirbaseNotificationReadOnlyRepository>();

            var section = Configuration.GetSection("ConnectionStrings");
            services.Configure<MKConfiguration>(section);
           

            services.AddControllers();
          //  services.AddSwaggerGen(c =>
         //   {
         //       c.SwaggerDoc("v1", new OpenApiInfo { Title = "GetConnection.Api", Version = "v1" });
         //   });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseAuthentication();
           // app.UseMvc();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GetConnection.Api v1"));
            }
            app.UseMiddleware<JwtMiddleware>();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
