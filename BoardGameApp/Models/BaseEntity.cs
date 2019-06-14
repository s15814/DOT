using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BoardGameApp.Models
{
    public abstract class BaseEntity
    {
        [Key]
        private int id;

        public int Id { get => id; set => id = value; }
    }
}