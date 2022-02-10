using Microsoft.Extensions.DependencyInjection;
using MNPBusinessLogic;
using MNPDatabaseRepository.Models;
using MNPInterfaces;
using System;
using Xunit;
using Xunit.Abstractions;

namespace MNPContactManagementAPI.Tests
{
    public class ContactManagementBusinessLogicTests
    {
        private readonly ServiceProvider _serviceProvider;
        private readonly MnpContactManagementContext _mnpContactManagementContext;

        public ContactManagementBusinessLogicTests(ITestOutputHelper testOutputHelper)
        {
            _serviceProvider = ServiceProviderFactory.SetupServiceProvider(testOutputHelper);
            using (var scope = _serviceProvider.CreateScope())
            {
                _mnpContactManagementContext = scope.ServiceProvider.GetRequiredService<MnpContactManagementContext>();
                var setup = new SetupInMemoryDatabase(_mnpContactManagementContext);
           
            }
        }


        [Fact]
        public void GetMnpContactManagementTableData_MnpContactManagement_ReturnfourRows()
        {
            var sut = ActivatorUtilities.CreateInstance<ContactManagementBusinessLogic>(_serviceProvider);
            var count = sut.GetMnpContactManagementTableData().Count;
            Assert.Equal(4, count);
        }

        [Fact]
        public void CreateNewContact_MnpContactManagement_ReturnTrue()
        {
            MnpContactManagement mnpContactManagement = new MnpContactManagement()
            {
                ContactName = "Anna Smith",
                Address = "1234 Main Street Test Caledon",
                LastDateContacted = DateTime.Now.AddDays(1),
                JobTitle = "CFO",
                Phone = 1225567890,
                CompanyId = 4,
                Email = "Anna@abc.com",
                Comments = " Comments Section Test 5"

            };
            var sut = ActivatorUtilities.CreateInstance<ContactManagementBusinessLogic>(_serviceProvider);
            Assert.True(sut.CreateNewContact(mnpContactManagement));
            var count = sut.GetMnpContactManagementTableData().Count;
            Assert.Equal(5, count);
        }

        [Fact]
        public void SaveContact_MnpContactManagement_ReturnTrue()
        {
            MnpContactManagement mnpContactManagement = new MnpContactManagement()
            {
                Id = 1,
                ContactName = "Anna Smith",
                Address = "1234 Main Street Test Caledon",
                LastDateContacted = DateTime.Now.AddDays(1),
                JobTitle = "CFO",
                Phone = 1225567890,
                CompanyId = 1,
                Email = "Anna@abc.com",
                Comments = " Comments Section Test 5"

            };
            var sut = ActivatorUtilities.CreateInstance<ContactManagementBusinessLogic>(_serviceProvider);
            Assert.True(sut.SaveContact(mnpContactManagement));
            var count = sut.GetMnpContactManagementTableData().Count;
            Assert.Equal(4, count);
        }

        [Fact]
        public void GetMnpContactManagementTableDataById_MnpContactManagement_ReturnMnpContactManagement()
        {
            
            var sut = ActivatorUtilities.CreateInstance<ContactManagementBusinessLogic>(_serviceProvider);            
            var mnpContactManagement = sut.GetMnpContactManagementTableDataById(1);
            Assert.Equal(1, mnpContactManagement.Id);
        }


        [Fact]
        public void VerifyDuplicateMnpContactManagement_MnpContactManagement_ReturnTrue()
        {
            MnpContactManagement mnpContactManagement = new MnpContactManagement()
            {
               
                ContactName = "Jane",
                Address = "123 Main Street Test Toronto",
                LastDateContacted = DateTime.Now.AddDays(0),
                JobTitle = "Accountant",
                Phone = 1234567890,
                CompanyId = 1,
                Email = "123@abc.com",
                Comments = " Comments Section Test 1"

            };


            var sut = ActivatorUtilities.CreateInstance<ContactManagementBusinessLogic>(_serviceProvider);
            Assert.True(sut.VerifyDuplicateMnpContactManagement(mnpContactManagement));
        }

        [Fact]
        public void VerifyDuplicateMnpContactManagement_MnpContactManagement_ReturnFalse()
        {
            MnpContactManagement mnpContactManagement = new MnpContactManagement()
            {
               
                ContactName = "Jane west",
                Address = "123 Main Street Test Toronto",
                LastDateContacted = DateTime.Now.AddDays(0),
                JobTitle = "Accountant",
                Phone = 2234567890,
                CompanyId = 1,
                Email = "123@abc.com",
                Comments = " Comments Section Test 1"

            };


            var sut = ActivatorUtilities.CreateInstance<ContactManagementBusinessLogic>(_serviceProvider);
            Assert.False(sut.VerifyDuplicateMnpContactManagement(mnpContactManagement));
        }
    }
}