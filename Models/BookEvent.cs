using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UnPeu.Models
{
    public class BookEvent
    {
        public int Id { get; set; }
        public BranchEvent BranchEvent { get; set; }
        public int BranchEventId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; }
    }
}