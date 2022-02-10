using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MNPBusinessLogic;
using MNPDatabaseRepository.Models;
using MNPInterfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Microsoft.EntityFrameworkCore;


namespace MNPContactManagementAPI.Tests
{
    public class ServiceProviderFactory
    {
        public static ServiceProvider SetupServiceProvider(ITestOutputHelper testOutputHelper)
        {
            IConfiguration configuration = UnitTestConfigHelper.GetConfiguration();

            ILogger XUnitLogger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.TestOutput(testOutputHelper, Serilog.Events.LogEventLevel.Verbose)
                .CreateLogger();
            var services = new ServiceCollection();

            services.AddSingleton(configuration);

            services.AddLogging(logbuilder => 
            { 
                logbuilder.AddSerilog(XUnitLogger); 
            });            

            services.AddDbContext<MnpContactManagementContext>(options =>
            {
                options.UseInMemoryDatabase("MNPContactManagementTest");
            });
               
            services.AddTransient<IContactManagementBusinessLogic, ContactManagementBusinessLogic>();
            return services.BuildServiceProvider();

        }
    }
}
