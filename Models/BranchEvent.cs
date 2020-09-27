using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UnPeu.Models
{
    public class BranchEvent
    {
        public int Id { get; set; }
        public EventType EventType { get; set; }
        public int EventTypeId { get; set; }
        public Branch Branch { get; set; }
        public int BranchId { get; set; }
        public DateTime StartTime { get; set; }
    }
}