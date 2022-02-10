using System;
using System.Collections.Generic;

namespace MNPDatabaseRepository.Models
{
    public partial class CompaniesTable
    {
        public CompaniesTable()
        {
            MnpContactManagements = new HashSet<MnpContactManagement>();
        }

        public int Id { get; set; }
        public string ComapanyName { get; set; } = null!;

        public virtual ICollection<MnpContactManagement> MnpContactManagements { get; set; }
    }
}
