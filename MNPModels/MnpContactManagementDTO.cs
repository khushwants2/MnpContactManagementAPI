using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MNPModels
{
    public class MnpContactManagementDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required(ErrorMessage = "Name Must be provided")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "First Name must be in between 5 and 200 characters.")]
        public string name { get; set; }

        public string address { get; set; }

        public DateOnly lastdatecontacted { get; set; }
        public string jobtitle { get; set; }

        public Int64 phone { get; set; }

        public string company_id { get; set; }

        public string email { get; set; }

        public string comments { get; set; }



    }
}