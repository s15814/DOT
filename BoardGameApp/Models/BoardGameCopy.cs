using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BoardGameApp.Models
{
    public class BoardGameCopy : BaseEntity
    {
        [Required]
        public byte BoardGameId { get; set; }

        [Required]
        public byte LocationId { get; set; }

        [Required]
        public int Amount { get; set; }
    }
}