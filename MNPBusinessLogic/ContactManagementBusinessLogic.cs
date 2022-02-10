using Microsoft.Extensions.Logging;
using MNPDatabaseRepository.Models;
using MNPInterfaces;

namespace MNPBusinessLogic
{
    public class ContactManagementBusinessLogic: IContactManagementBusinessLogic
    {
        private readonly ILogger<ContactManagementBusinessLogic> _logger;
        private readonly MnpContactManagementContext _mnpContactManagementContext;
        public ContactManagementBusinessLogic( ILogger<ContactManagementBusinessLogic> logger, MnpContactManagementContext mnpContactManagementContext)
        {
            _logger = logger;
            _mnpContactManagementContext = mnpContactManagementContext;
        }

        public List<MnpContactManagement> GetMnpContactManagementTableData()
        {
            try
            {
                return _mnpContactManagementContext.MnpContactManagements.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} \n {ex.StackTrace}");
                return new List<MnpContactManagement>();
            }

        }

        public List<CompaniesTable> GetCompaniesList()
        {
            try
            {
                return _mnpContactManagementContext.CompaniesTables.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} \n {ex.StackTrace}");
                return new List<CompaniesTable>();
            }

        }

        public bool VerifyDuplicateMnpContactManagement(MnpContactManagement mnpContactManagement)
        {
            
            try
            {
                var mnp =  _mnpContactManagementContext.MnpContactManagements
                    .Where(x => x.Phone == mnpContactManagement.Phone
                    && x.ContactName == mnpContactManagement.ContactName)
                    .ToList();
                return (mnp.Count > 0)? true: false;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} \n {ex.StackTrace}");
                return false;
            }

        }

        public MnpContactManagement GetMnpContactManagementTableDataById(int id)
        {
            MnpContactManagement? mnpContactManagement =  new MnpContactManagement();
            try
            {
                return _mnpContactManagementContext.MnpContactManagements.Where(x => x.Id == id).FirstOrDefault();
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} \n {ex.StackTrace}");
                return null;
            }

        }

        public bool CreateNewContact(MnpContactManagement mnpContactManagement)
        {
            try
            {
                _mnpContactManagementContext.AddRange(mnpContactManagement);
                _mnpContactManagementContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} \n {ex.StackTrace}");
                return false;
            }
        }

        public bool SaveContact(MnpContactManagement mnpContactManagement)
        {
            try
            {
                _mnpContactManagementContext.UpdateRange(mnpContactManagement);
                _mnpContactManagementContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} \n {ex.StackTrace}");
                return false;
            }
        }

    }
}