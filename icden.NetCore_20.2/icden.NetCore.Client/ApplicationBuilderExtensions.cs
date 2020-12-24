using DevExpress.Xpo;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace icden.NetCore.Client {
    public static class ApplicationBuilderExtensions {
        public static IApplicationBuilder UseXpoInitialData( this IApplicationBuilder app ) {
            using ( var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope() ) {
                UnitOfWork uow = scope.ServiceProvider.GetService<UnitOfWork>();
                InitialDataHelper.Seed( uow );
            }
            return app;
        }
    }
}
