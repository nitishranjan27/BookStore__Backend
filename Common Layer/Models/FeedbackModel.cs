using System;
using System.Collections.Generic;
using System.Text;

namespace Common_Layer.Models
{
    public class FeedbackModel
    {
        public int Rating { get; set; }
        public string Comment { get; set; }
        public int BookId { get; set; }
    }
}
