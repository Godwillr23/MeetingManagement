using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChilliSoft_Assignment.Models
{
    public class Meeting
    {
        [Key]
        public int MeetingId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Meeting Type is required.")]
        public string MeetingCode { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Meeting Date is required")]
        public string MeetingDate { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Meeting Time is required")]
        public string MeetingTime { get; set; }

        public DateTime DateCreated { get; set; }
    }
}