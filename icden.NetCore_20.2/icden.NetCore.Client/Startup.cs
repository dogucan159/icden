using DevExpress.Xpo.DB;
using icden.NetCore.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace icden.NetCore.Client {
    public class Startup {
        public Startup( IConfiguration configuration ) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices( IServiceCollection services ) {
            // Add framework services.
            services
                .AddControllersWithViews()
                .AddJsonOptions( options => options.JsonSerializerOptions.PropertyNamingPolicy = null )
                .AddDxSampleModelJsonOptions();

            services
                .AddXpoDefaultUnitOfWork( true, options => options
                     .UseConnectionString( WebApiDataStoreClient.GetConnectionString( "https://localhost:44371/xpo/" ) )
                     //.UseConnectionPool( false )
                     .UseThreadSafeDataLayer( true )
                     .UseAutoCreationOption( DevExpress.Xpo.DB.AutoCreateOption.DatabaseAndSchema ) // Remove this line if the database already exists
                     .UseEntityTypes( typeof( AuditorTitle ) ) // Pass all of your persistent object types to this method.
                );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure( IApplicationBuilder app, IWebHostEnvironment env ) {
            if ( env.IsDevelopment() ) {
                app.UseDeveloperExceptionPage();
            }
            else {
                app.UseExceptionHandler( "/Home/Error" );
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints( endpoints => {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}" );
            } );
            app.UseXpoInitialData();
        }
    }
}
