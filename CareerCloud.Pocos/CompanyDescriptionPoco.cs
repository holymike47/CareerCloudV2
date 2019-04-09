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
    [Table("Company_Descriptions")]
    public class CompanyDescriptionPoco : IPoco
    {
        
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        public Guid Company { get; set; }
        
        [StringLength(10),Required]
        public string LanguageId { get; set; }
        
        [Column("Company_Name"),Required]
        public string CompanyName { get; set; }
        [DataType(DataType.MultilineText)]
        [Column("Company_Description"),StringLength(500),Required]
        public string CompanyDescription { get; set; }
        
        [Column("Time_Stamp",TypeName = "timestamp")]
        [Timestamp]
        public byte[] TimeStamp { get; set; }
        [ForeignKey(nameof(Company))]
        public virtual CompanyProfilePoco CompanyProfile { get; set; }
        [ForeignKey(nameof(LanguageId))]
        public virtual SystemLanguageCodePoco SystemLanguageCode { get; set; }
    }
}
