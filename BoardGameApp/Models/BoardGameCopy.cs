using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BoardGameApp.Models
{
    public class BoardGameCopy : BaseEntity
    {
        public string ClientRefId { get; set; }
        public Client Client { get; set; }
        public int BoardGameRefId { get; set; }
        public BoardGame BoardGame { get; set; }
    }
}