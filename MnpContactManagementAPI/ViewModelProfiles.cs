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
            CreateMap<MnpContactManagementDTO,MnpContactManagement >()
                .ForMember(x => x.Company, opt => opt.Ignore());
            CreateMap<CompaniesTable, CompaniesTableDTO>();
        }
    }
}
