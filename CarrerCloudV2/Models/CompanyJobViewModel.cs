using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarrerCloudV2.Models
{
    public class CompanyJobViewModel
    {
        public Guid Id { get; set; }
        public Guid Company { get; set; }
        public Guid DescId { get; set; }
        [DataType(DataType.DateTime)]
        [Display(Name = "Posted")]
        public DateTime PostedDate { get; set; }
        [Display(Name = "Available")]
        public bool IsAvailable { get; set; } = true;
        //job desc
        public string Title { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        //job skill
        [Display(Name = "Skill")]
        public string Skill1 { get; set; }
        [Display(Name = "Skill")]
        public string Skill2 { get; set; }
        [Display(Name = "Skill")]
        public string Skill3 { get; set; }
        [Display(Name = "Skill")]
        public string Skill4 { get; set; }
        [Display(Name = "Skill")]
        public string Skill5 { get; set; }
        [Display(Name = "Skill Level")]
        public int SkillLevel1 { get; set; }
        [Display(Name = "Skill Level")]
        public int SkillLevel2 { get; set; }
        [Display(Name = "Skill Level")]
        public int SkillLevel3 { get; set; }
        [Display(Name = "Skill Level")]
        public int SkillLevel4 { get; set; }
        [Display(Name = "Skill Level")]
        public int SkillLevel5 { get; set; }
        [Display(Name = "Importance")]
        public int Importance1 { get; set; }
        [Display(Name = "Importance")]
        public int Importance2 { get; set; }
        [Display(Name = "Importance")]
        public int Importance3 { get; set; }
        [Display(Name = "Importance")]
        public int Importance4 { get; set; }
        [Display(Name = "Importance")]
        public int Importance5 { get; set; }

        //major
        [Display(Name = "Major")]
        public string Major1 { get; set; }
        [Display(Name = "Major")]
        public string Major2 { get; set; }
        [Display(Name = "Major")]
        public string Major3 { get; set; }
        [Display(Name = "Major")]
        public string Major4 { get; set; }
        [Display(Name = "Major")]
        public string Major5 { get; set; }
        [Display(Name = "Importance")]
        public int MajorImportance1 { get; set; }
        [Display(Name = "Importance")]
        public int MajorImportance2 { get; set; }
        [Display(Name = "Importance")]
        public int MajorImportance3 { get; set; }
        [Display(Name = "Importance")]
        public int MajorImportance4 { get; set; }
        [Display(Name = "Importance")]
        public int MajorImportance5 { get; set; }

    }
}