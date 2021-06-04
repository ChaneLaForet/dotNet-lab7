using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2.Models
{
    public class Comment
    {
        //[Key]
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime DateTime { get; set; }
        //[Range (1,10)]
        public int Rating { get; set; }

        public Movie Movie { get; set; }
    }
}
