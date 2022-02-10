using System;
using System.Collections.Generic;

namespace MNPDatabaseRepository.Models
{
    public partial class MnpContactManagement
    {
        public int Id { get; set; }
        public string ContactName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public DateTime LastDateContacted { get; set; }
        public string JobTitle { get; set; } = null!;
        public long Phone { get; set; }
        public int CompanyId { get; set; }
        public string Email { get; set; } = null!;
        public string? Comments { get; set; }

        public virtual CompaniesTable Company { get; set; } = null!;
    }
}
