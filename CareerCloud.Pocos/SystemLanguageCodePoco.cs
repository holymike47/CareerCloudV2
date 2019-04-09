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
    [Table("System_Language_Codes")]
    public class SystemLanguageCodePoco
    {
        [Key]
        [StringLength(10)]
        public string LanguageID { get; set; }
        [DataMember]
        [StringLength(50),Required]
        public string Name { get; set; }
        [DataMember]
        [Column("Native_Name"),StringLength(50),Required]
        public string NativeName { get; set; }
        public virtual ICollection<CompanyDescriptionPoco> CompanyDescriptions { get; set; }
    }
}
