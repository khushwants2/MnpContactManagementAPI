using MNPDatabaseRepository.Models;

namespace MNPInterfaces
{
    public interface IContactManagementBusinessLogic
    {
        public void SeedDatabase();
        public List<MnpContactManagement> GetMnpContactManagementTableData();
        public bool CreateNewContact(MnpContactManagement mnpContactManagement);
        public bool SaveContact(MnpContactManagement mnpContactManagement);

        public List<CompaniesTable> GetCompaniesList();

        public bool VerifyDuplicateMnpContactManagement(MnpContactManagement mnpContactManagement);
        public MnpContactManagement GetMnpContactManagementTableDataById(int id);

    }
}