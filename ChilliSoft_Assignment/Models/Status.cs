using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChilliSoft_Assignment.Models
{
    public class Status
    {
        [Key]
        public int StatusItemId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Status is required.")]
        public string StatusItem { get; set; }
    }
}