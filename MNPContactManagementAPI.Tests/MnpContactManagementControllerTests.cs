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
        public void GetCompaniesDD_CompaniesTable_ReturnNotNull_ReturnFourCount()
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

        [Fact]
        public void GetMNPContanctManagementList_MnpContactManagementDTO_ReturnNotNull_ReturnFourCount()
        {
            //Arrange
            var sut = ActivatorUtilities.CreateInstance<MnpContactManagementController>(_serviceProvider);
            //Act
            var actionResult = sut.GetMNPContanctManagementList();

            //Asert
            var okResult = Assert.IsType<OkObjectResult>(actionResult);
            Assert.NotNull(okResult.Value);
            string json = okResult.Value.ToString();
            var values = JsonSerializer.Deserialize<List<MnpContactManagementDTO>>(json);
            Assert.NotNull(values);
            Assert.Equal(4, values.Count);

        }

        [Fact]
        public void GetMNPContanctManagementById_MnpContactManagementDTO_ReturnNotNull_ReturnOneCount()
        {
            //Arrange
            var sut = ActivatorUtilities.CreateInstance<MnpContactManagementController>(_serviceProvider);
            //Act
            var actionResult = sut.GetMNPContanctManagementById(1);

            //Asert
            var okResult = Assert.IsType<OkObjectResult>(actionResult);
            Assert.NotNull(okResult.Value);
            string json = okResult.Value.ToString();
            var values = JsonSerializer.Deserialize<MnpContactManagementDTO>(json);
            Assert.NotNull(values);
            Assert.Equal(values.Id, 1);

        }

        [Fact]
        public void GetMNPContanctManagementById_MnpContactManagementDTO_ReturnNotFound()
        {
            //Arrange
            var sut = ActivatorUtilities.CreateInstance<MnpContactManagementController>(_serviceProvider);
            //Act
            var actionResult = sut.GetMNPContanctManagementById(10);

            //Asert
            var okResult = Assert.IsType<NotFoundObjectResult>(actionResult);
            Assert.Equal(okResult.Value.ToString(), "NotFound");
           

        }
        [Fact]
        public void SaveMNPContanctManagement_MnpContactManagementDTO_ReturnString()
        {
            //Arrange
            var sut = ActivatorUtilities.CreateInstance<MnpContactManagementController>(_serviceProvider);
            MnpContactManagementDTO data = new MnpContactManagementDTO()
            {
                Id = 0,
                ContactName = "Anna Smith",
                Address = "1234 Main Street Test Caledon",
                LastDateContacted = DateTime.Now.AddDays(1),
                JobTitle = "CFO",
                Phone = 1225567890,
                CompanyId = 4,
                Email = "Anna@abc.com",
                Comments = " Comments Section Test 5"

            };
            //Act
            var actionResult = sut.SaveMNPContanctManagement(data);

            //Asert
            var okResult = Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal(okResult.Value.ToString(), "Contact Saved Succeessfully");


        }

        [Fact]
        public void UpdateMNPContanctManagement_MnpContactManagementDTO_ReturnString()
        {
            //Arrange
            var sut = ActivatorUtilities.CreateInstance<MnpContactManagementController>(_serviceProvider);
            MnpContactManagementDTO data = new MnpContactManagementDTO()
            {
                Id = 1,
                ContactName = "Anna Smith",
                Address = "1234 Main Street Test Caledon",
                LastDateContacted = DateTime.Now.AddDays(1),
                JobTitle = "CFO",
                Phone = 1225567890,
                CompanyId = 3,
                Email = "Anna@abc.com",
                Comments = " Comments Section Test 5"

            };
            //Act
            var actionResult = sut.SaveMNPContanctManagement(data);

            //Asert
            var okResult = Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal(okResult.Value.ToString(), "Contact Saved Succeessfully");


        }
    }
}
