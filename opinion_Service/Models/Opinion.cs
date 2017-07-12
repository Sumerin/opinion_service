using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace opinion_Service.Models
{
    public class Opinion
    {
        [Key]
        public int OpinionId { get; set; }
        [Required (ErrorMessage ="Description")]
        public string Description { get; set; }
        public User User { get; set; }
        public Site Site { get; set; }

        public Opinion()
        {
            Site = new Site();
        }
    }
}