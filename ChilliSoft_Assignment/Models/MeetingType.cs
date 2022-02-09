using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace ChilliSoft_Assignment.Models
{
    public class MeetingType
    {
        [Key]
        public int MeetingTypeId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Meeting Name is required.")]
        public string MeetingName { get; set; }
    }
}