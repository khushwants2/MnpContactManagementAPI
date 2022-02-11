using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using MnpContactManagementAPI.Controllers;
using MNPDatabaseRepository.Models;
using MNPInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using System.Text.Json;
using MNPModels;

namespace MNPContactManagementAPI.Tests
{
    public class MnpContactManagementControllerTests
    {
        private readonly ServiceProvider _serviceProvider;
        private readonly MnpContactManagementContext _mnpContactManagementContext;
        private readonly IContactManagementBusinessLogic _iContactManagementBusinessLogic;

        public MnpContactManagementControllerTests(ITestOutputHelper testOutputHelper)
        {
            ServiceProviderFactory serviceProviderFactory = new ServiceProviderFactory();
            _serviceProvider = serviceProviderFactory.SetupServiceProvider(testOutputHelper);
            using (var scope = _serviceProvider.CreateScope())
            {
                _mnpContactManagementContext = scope.ServiceProvider.GetRequiredService<MnpContactManagementContext>();
                var setup = new SetupInMemoryDatabase(_mnpContactManagementContext);

                _iContactManagementBusinessLogic = scope.ServiceProvider.GetRequiredService<IContactManagementBusinessLogic>();

            }
        }


        [Fact]
        public void GetCompaniesDD_CompaniesTable_ReturnString()
        {
            //Arrange
            var sut = ActivatorUtilities.CreateInstance<MnpContactManagementController>(_serviceProvider);
            //Act
            var actionResult = sut.GetCompaniesDD();

            //Asert
            var okResult = Assert.IsType<OkObjectResult>(actionResult);
            Assert.NotNull(okResult.Value);
            string json = okResult.Value.ToString();
            var values = JsonSerializer.Deserialize<List<CompaniesTableDTO>>(json);
            Assert.NotNull(values);
            Assert.Equal(4, values.Count);
           
        }

    }
}
