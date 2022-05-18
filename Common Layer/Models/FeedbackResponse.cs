using System;
using System.Collections.Generic;
using System.Text;

namespace Common_Layer.Models
{
    public class FeedbackResponse
    {
        public int FeedbackId { get; set; }
        public int UserId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public int BookId { get; set; }
        public string FullName { get; set; }
    }
}
