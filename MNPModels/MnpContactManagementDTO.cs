using System.ComponentModel.DataAnnotations;

using System.Runtime.Serialization;

namespace MNPModels
{
    [DataContract]
    public class MnpContactManagementDTO
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string ContactName { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public DateTime LastDateContacted { get; set; }
        [DataMember]
        public string JobTitle { get; set; }
        [DataMember]
        public long Phone { get; set; }
        [DataMember]
        public int CompanyId { get; set; }
        [DataMember]
        public string Email { get; set; } 
        [DataMember]
        public string? Comments { get; set; }



    }
}