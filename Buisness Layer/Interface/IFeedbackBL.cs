using Common_Layer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Buisness_Layer.Interface
{
    public interface IFeedbackBL
    {
        public FeedbackModel AddFeedback(FeedbackModel feedback, int userId);
        public List<FeedbackResponse> GetRecordsByBookId(int bookId);
    }
}
