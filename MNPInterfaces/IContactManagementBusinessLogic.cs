using MNPDatabaseRepository.Models;

namespace MNPInterfaces
{
    public interface IContactManagementBusinessLogic
    {
        public List<MnpContactManagement> GetMnpContactManagementTableData();
        public bool CreateNewContact(MnpContactManagement mnpContactManagement);
        public bool SaveContact(MnpContactManagement mnpContactManagement);

    }
}