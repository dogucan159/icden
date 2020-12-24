using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DX.Data.Xpo;
using DX.Data.Xpo.Identity;
using icden.NetCore.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace icden.NetCore.API {
    public class Startup {
        public Startup( IConfiguration configuration ) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices( IServiceCollection services ) {

            services.AddControllers();
            services.AddSwaggerGen( c => {
                c.SwaggerDoc( "v1", new OpenApiInfo { Title = "icden.NetCore.API", Version = "v1" } );
            } );

            string connStrName = "Futurecom";
            string connectionString = Configuration.GetConnectionString( connStrName );
            //Initialize XPODataLayer / Database	
            services.AddXpoDatabase( ( o ) => {
                o.Name = connStrName;
                o.ConnectionString = connectionString;
            } );
            //Initialize identity to use XPO
            services
                .AddIdentity<ApplicationUser, ApplicationRole>( options => {
                    // Password settings.
                    options.Password.RequireDigit = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireNonAlphanumeric = true;
                    options.Password.RequireUppercase = true;
                    options.Password.RequiredLength = 6;
                    options.Password.RequiredUniqueChars = 1;

                    // Lockout settings.
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes( 5 );
                    options.Lockout.MaxFailedAccessAttempts = 5;
                    options.Lockout.AllowedForNewUsers = true;

                    // User settings.
                    options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                    options.User.RequireUniqueEmail = false;
                } )
                .AddXpoIdentityStores<ApplicationUser, XpoApplicationUser, ApplicationRole, XpoApplicationRole>( connStrName,
                    new ApplicationUserMapper(),
                    new ApplicationRoleMapper(),
                    new XPUserStoreValidator<string, ApplicationUser, XpoApplicationUser>(),
                    new XPRoleStoreValidator<string, ApplicationRole, XpoApplicationRole>() )
                .AddDefaultTokenProviders();

            IDataStore dataStore = XpoDefault.GetConnectionProvider( connectionString, AutoCreateOption.DatabaseAndSchema );
            WebApiDataStoreService service = new WebApiDataStoreService( dataStore );
            services.AddSingleton( service );

            services.AddMvc().AddXmlSerializerFormatters();

            services.AddCors( options =>
                 options.AddPolicy( "XPO", builder =>
                      builder.AllowAnyOrigin()
                          .WithMethods( "POST" )
                          .WithHeaders( "Content-Type" ) ) );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure( IApplicationBuilder app, IWebHostEnvironment env ) {
            if ( env.IsDevelopment() ) {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI( c => c.SwaggerEndpoint( "/swagger/v1/swagger.json", "icden.NetCore.API v1" ) );
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors( options => options.AllowAnyOrigin().AllowAnyHeader() );

            app.UseEndpoints( endpoints => {
                endpoints.MapControllers();
            } );
        }
    }
}
