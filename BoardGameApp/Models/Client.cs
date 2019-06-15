using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using BoardGameApp.Models;

namespace BoardGameApp.Models
{
    public class Client
    {
        [Key]
        public string Id { get; set; }
        [ForeignKey("ClientRefId")]
        public List<BoardGameCopy> BoardGameCopies { get; set; }

        [Required]
        [StringLength(70, ErrorMessage = Notifications.MAX_LEN)]
        [Display(Name = "Imię")]
        public string Name { get; set; }

        [Required]
        [StringLength(70, ErrorMessage = Notifications.MAX_LEN)]
        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }

    }
}