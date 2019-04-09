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
    [Table("Company_Job_Skills")]
    public class CompanyJobSkillPoco : IPoco
    {
        
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        public Guid Job { get; set; }
        [StringLength(100)]
        public string Skill { get; set; }
        
        [Column("Skill_Level"),Required]
        public int SkillLevel { get; set; }
        
        [Required]
        public int Importance { get; set; }
        
        [Column("Time_Stamp",TypeName = "timestamp")]
        [Timestamp]
        public byte[] TimeStamp { get; set; }
        [ForeignKey(nameof(Job))]
        public virtual CompanyJobPoco CompanyJob { get; set; }
    }
}
