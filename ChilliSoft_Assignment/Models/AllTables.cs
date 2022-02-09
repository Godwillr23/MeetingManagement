using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChilliSoft_Assignment.Models
{
    public class AllTables
    {
        public virtual Meeting Meetings { get; set; }
        public virtual MeetingType MeetingTypes { get; set; }
        public virtual MeetingItem MeetingItems { get; set; }
        public virtual MeetingItemStatus MeetingItemStatuses { get; set; }
    }
}