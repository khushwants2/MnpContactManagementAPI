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
    /// <summary>
    /// Respective Class hels to setup service provider for XUnit Testing
    /// </summary>
    public class ServiceProviderFactory
    {
        public ServiceProvider SetupServiceProvider(ITestOutputHelper testOutputHelper)
        {
            //Set and Get Configuration from appsettings.json
            IConfiguration configuration = UnitTestConfigHelper.GetConfiguration();


            //Logging for test output
            ILogger XUnitLogger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.TestOutput(testOutputHelper, Serilog.Events.LogEventLevel.Verbose)
                .CreateLogger();

            //create Services
            var services = new ServiceCollection();

            //Add Configuarions into services
            services.AddSingleton(configuration);
            services.AddEntityFrameworkInMemoryDatabase();
            //set up logging
            services.AddLogging(logbuilder => 
            { 
                logbuilder.AddSerilog(XUnitLogger); 
            });

            // generating a unique db name for each.
            string dbName = Guid.NewGuid().ToString();
            //create db context
            services.AddDbContext<MnpContactManagementContext>(options =>
            {
                options.UseInMemoryDatabase(dbName);
            });
            //add Interface references
            services.AddTransient<IContactManagementBusinessLogic, ContactManagementBusinessLogic>();
            //retrun service providers
            return services.BuildServiceProvider();

        }
    }
}
