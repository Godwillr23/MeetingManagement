using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChilliSoft_Assignment.Models
{
    public class MeetingItemStatus
    {
        [Key]
        public int StatusItemId { get; set; }
        public int ItemId { get; set; }
        public string Status { get; set; }
    }
}