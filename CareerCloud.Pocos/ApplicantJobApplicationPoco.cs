using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.Pocos
{
    [Table("Applicant_Job_Applications")]
    public class ApplicantJobApplicationPoco:IPoco
    {
        [Key][Required]
        public Guid Id { get; set; }
        [Required]
        public Guid Applicant { get; set; }
        [Required]
        public Guid Job { get; set; }
        [DataType(DataType.DateTime)]
        [Column("Application_Date"),Required]
        public DateTime ApplicationDate { get; set; }
        [Column("Time_Stamp", TypeName = "timestamp")]
        [Timestamp]
        public byte[] TimeStamp { get; set; }
        [ForeignKey(nameof(Applicant))]
        public virtual ApplicantProfilePoco ApplicantProfile { get; set; }
        [ForeignKey(nameof(Job))]
        public virtual CompanyJobPoco CompanyJob { get; set; }


    }
}
