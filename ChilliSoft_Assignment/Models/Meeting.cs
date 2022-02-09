using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChilliSoft_Assignment.Models
{
    public class Meeting
    {
        public int MeetingId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Meeting Type is required.")]
        public string MeetingCode { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Meeting Type is Date")]
        public string MeetingDate { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Select Type is Time")]
        public string MeetingTime { get; set; }

        public DateTime DateCreated { get; set; }
    }
}