using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace opinion_Service.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage ="Username is required")]
        public string Username { get; set; }

        public string Salt { get; set; }

        [Required(ErrorMessage ="Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        public virtual List<Opinion> Opinions { get; set; }
    }
}