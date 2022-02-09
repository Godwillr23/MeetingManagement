using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChilliSoft_Assignment.Models
{
    public class Staff
    {
        [Key]
        public int StaffId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Firstname is required.")]
        public string Firstname { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Lastname is required.")]
        public string Lastname { get; set; }
    }
}