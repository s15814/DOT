using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using BoardGameApp.Models;

namespace BoardGameApp.Models
{
    public class BoardGame : BaseEntity
    {
        [Required(ErrorMessage = (Notifications.REQ + "gry"))]
        [StringLength(255, ErrorMessage = Notifications.MAX_LEN)]
        [Display(Name = "Tytuł")]
        public string Title { get; set; }

        [Required(ErrorMessage = (Notifications.REQ + "gry"))]
        [StringLength(255, ErrorMessage = Notifications.MAX_LEN)]
        [Display(Name = "Wydawca")]
        public string Publisher { get; set; }

        [Required(ErrorMessage = (Notifications.REQ + "gry"))]
        [StringLength(255, ErrorMessage = Notifications.MAX_LEN)]
        [RegularExpression("^([1-9]|10|11)-([1-9]|10|11|12|13|14|15|16)$|^([0-9]|10|11|12|13|14|15|16)$", ErrorMessage = Notifications.RANGE)]
        [Display(Name = "Liczba graczy")]
        public string Players { get; set; }

        [Required(ErrorMessage = (Notifications.REQ + "gry"))]
        [StringLength(3, ErrorMessage = Notifications.MAX_LEN)]
        [RegularExpression("([0-9]+)", ErrorMessage = Notifications.NUMERIC)]
        [Display(Name = "Czas gry")]
        public string Playtime { get; set; }

        [Required(ErrorMessage = (Notifications.REQ + "gry"))]
        [StringLength(500, ErrorMessage = Notifications.MAX_LEN)]
        [Display(Name = "Opis")]
        public string Description { get; set; }

        [Required(ErrorMessage = (Notifications.REQ + "gry"))]
        [Range(1, 30, ErrorMessage = "Ilość musi się zawierać między {1} - {2}")]
        [Display(Name = "Stan")]
        public int Amount { get; set; }

        [ForeignKey("BoardGameRefId")]
        public List<BoardGameCopy> BoardGameCopies { get; set; }
    }
}