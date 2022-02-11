using AutoMapper;
using MNPDatabaseRepository.Models;
using MNPModels;

namespace MNPContactManagementAPI.Tests
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
