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
    [Table("Company_Jobs_Descriptions")]
    public class CompanyJobDescriptionPoco : IPoco
    {
        
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        public Guid Job { get; set; }
        
        [Column("Job_Name"),StringLength(100)]
        public string JobName { get; set; }
        
        [Column("Job_Descriptions"),StringLength(500)]
        public string JobDescriptions { get; set; }
        
        [Column("Time_Stamp",TypeName = "timestamp")]
        [Timestamp]
        public byte[] TimeStamp { get; set; }
        [ForeignKey(nameof(Job))]
        public virtual CompanyJobPoco CompanyJob { get; set; }
    }
}
