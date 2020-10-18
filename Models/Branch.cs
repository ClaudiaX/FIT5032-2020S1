using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UnPeu.Models
{
    public class Branch
    {
        public int Id { get; set; }
        //[Required]
        //[MaxLength(50)]
        public string Name { get; set; }

        public string Address { get; set; }
    }
}