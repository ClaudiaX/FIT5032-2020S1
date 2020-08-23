using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UnPeu.Models
{
    public class Job
    {
        public int Id { get; set; }
        public string Position { get; set; }

        public string Company { get; set; }

        public string Description { get; set; }

        public string Email { get; set; }

        public DateTime EndTime { get; set; }
    }
}