using AutoMapper;
using MNPDatabaseRepository.Models;
using MNPModels;

namespace MnpContactManagementAPI
{
    public class ViewModelProfiles: Profile
    {
        public ViewModelProfiles()
        {
            CreateMap<MnpContactManagement, MnpContactManagementDTO>();
            CreateMap<CompaniesTable, CompaniesTableDTO>();
        }
    }
}
