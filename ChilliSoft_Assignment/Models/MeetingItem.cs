using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChilliSoft_Assignment.Models
{
    public class MeetingItem
    {
        [Key]
        public int ItemId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Meeting Dept is required.")]
        public int MeetingId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Meeting Name is required.")]
        public string MeetingItemName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Staff Responsible is required.")]
        public string ActionBy { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Item Description is required.")]
        public string ItemDescription { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Item Status is required.")]
        public string ItemStatus { get; set; }
        public DateTime DateCreated { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Due Date is required.")]
        public string DueDate { get; set; }

        public bool isSelected { get; set; }
    }
}