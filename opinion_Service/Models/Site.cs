using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace opinion_Service.Models
{
    public class Site
    {
        [Key]
        public int SiteId { get; set; }
        [Display(Name ="Site Name")]
        [Required(ErrorMessage ="Required Site Name")]
        public string DomainName { get; set; }
        public virtual ICollection<Opinion> Opinions { get; set; }
    }
}