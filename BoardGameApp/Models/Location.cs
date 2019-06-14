using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BoardGameApp.Models
{
    public class Location : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Street { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string PostalCode { get; set; }

        [Required]
        public string HouseNumber { get; set; }

        public string ApartmentNumber { get; set; }
    }
}