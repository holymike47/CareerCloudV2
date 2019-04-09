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
    [Table("Company_Locations")]
    public class CompanyLocationPoco : IPoco
    {
        
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        public Guid Company { get; set; }
        
        [Column("Country_Code"),StringLength(3),Required]
        public string CountryCode { get; set; }
        
        [Column("State_Province_Code"),StringLength(2)]
        public string Province { get; set; }
        
        [Column("Street_Address"),StringLength(100)]
        public string Street { get; set; }
        
        [Column("City_Town"),MaxLength(100)]
        public string City { get; set; }
        
        [Column("Zip_Postal_Code"),StringLength(7)]
        public string PostalCode { get; set; }
        
        [Column("Time_Stamp",TypeName = "timestamp")]
        [Timestamp]
        public byte[] TimeStamp { get; set; }
        [ForeignKey(nameof(Company))]
        public virtual CompanyProfilePoco CompanyProfile { get; set; }
    }
}
