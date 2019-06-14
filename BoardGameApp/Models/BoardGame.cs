using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BoardGameApp.Models
{
    public class BoardGame : BaseEntity
    {
        [Required]
        [StringLength(255)]
        [Display(Name = "Tytuł")]
        public string Title { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Wydawca")]
        public string Publisher { get; set; }

        [Required]
        [StringLength(70)]
        [Display(Name = "Liczba graczy")]
        public String Players { get; set; }

        [Required]
        [Display(Name = "Czas gry")]
        public int Playtime { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }
    }
}