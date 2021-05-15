using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2.ViewModels
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime DateTime { get; set; }

        public int Rating { get; set; }
    }
}
