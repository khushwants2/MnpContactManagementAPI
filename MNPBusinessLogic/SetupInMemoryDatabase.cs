using MNPDatabaseRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MNPBusinessLogic
{
    /// <summary>
    /// Respective class to seed database in Memory
    /// </summary>
    public class SetupInMemoryDatabase
    {        
        /// <summary>
        /// Constructor will call seed database function to load database into memories
        /// </summary>
        /// <param name="mnpContactManagementContext"></param>
        public SetupInMemoryDatabase(MnpContactManagementContext mnpContactManagementContext)
        {
            SeedDatabase(mnpContactManagementContext);
        }
        /// <summary>
        /// Insert value into temp database
        /// </summary>
        /// <param name="dbContext"></param>
        private void SeedDatabase(MnpContactManagementContext dbContext)
        {
            dbContext.CompaniesTables.Add( new CompaniesTable() { Id = 1, ComapanyName = "Google" } );
            dbContext.CompaniesTables.Add(new CompaniesTable() { Id = 2, ComapanyName = "Microsoft" });
            dbContext.CompaniesTables.Add(new CompaniesTable() { Id = 3, ComapanyName = "Apple" });
            dbContext.CompaniesTables.Add(new CompaniesTable() { Id = 4, ComapanyName = "Amazon" });

            dbContext.MnpContactManagements.Add(new MnpContactManagement()
            {
                Id = 1,
                ContactName = "Jane",
                Address = "123 Main Street Test Toronto",
                LastDateContacted = DateTime.Now.AddDays(0),
                JobTitle ="Accountant",
                Phone = 1234567890,
                CompanyId = 1,
                Email ="123@abc.com",
                Comments = " Comments Section Test 1"
                
            });

            dbContext.MnpContactManagements.Add(new MnpContactManagement()
            {
                Id = 2,
                ContactName = "Anna",
                Address = "1231 Main Street Test Montreal",
                LastDateContacted = DateTime.Now.AddDays(1),
                JobTitle = "Developer",
                Phone = 1234567990,
                CompanyId = 2,
                Email = "321@abc.com",
                Comments = " Comments Section Test 2"

            });

            dbContext.MnpContactManagements.Add(new MnpContactManagement()
            {
                Id = 3,
                ContactName = "Rob",
                Address = "12312 Main Street Test London",
                LastDateContacted = DateTime.Now.AddDays(3),
                JobTitle = "Accountant",
                Phone = 1234567899,
                CompanyId = 3,
                Email = "12345@abc.com",
                Comments = " Comments Section Test 3"

            });

            dbContext.MnpContactManagements.Add(new MnpContactManagement()
            {
                Id = 4,
                ContactName = "Bob Smith",
                Address = "1234 Main Street Test Brampton",
                LastDateContacted = DateTime.Now.AddDays(10),
                JobTitle = "CEO",
                Phone = 1235567890,
                CompanyId = 4,
                Email = "xyz@abc.com",
                Comments = " Comments Section Test 4"

            });

            dbContext.SaveChanges();

        }
    }
}
