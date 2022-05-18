using Common_Layer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_Layer.Interface
{
    public interface IFeedbackRL
    {
        public FeedbackModel AddFeedback(FeedbackModel feedback, int userId);
        public List<FeedbackResponse> GetRecordsByBookId(int bookId);
    }
}
