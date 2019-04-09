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
    [Table("Applicant_Skills")]
    public class ApplicantSkillPoco : IPoco
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        public Guid Applicant { get; set; }
        
        [Required,StringLength(30)]
        public string Skill { get; set; }
        
        [Column("Skill_Level"),StringLength(10),Required]
        public string SkillLevel { get; set; }
        
        [Column("Start_Month"),Required]
        public byte StartMonth { get; set; }
        
        [Column("Start_Year"),Required]
        public int StartYear { get; set; }
        
        [Column("End_Month"),Required]
        public byte EndMonth { get; set; }
        
        [Column("End_Year"),Required]
        public int EndYear { get; set; }
        
        [Column("Time_Stamp",TypeName = "timestamp")]
        [Timestamp]
        public byte[] TimeStamp { get; set; }
        [ForeignKey(nameof(Applicant))]
        public virtual ApplicantProfilePoco ApplicantProfile { get; set; }
    }
}
