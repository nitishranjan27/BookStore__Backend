using Buisness_Layer.Interface;
using Common_Layer.Models;
using Repository_Layer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Buisness_Layer.Service
{
    public class FeedbackBL : IFeedbackBL
    {
        private readonly IFeedbackRL feedbackRL;
        public FeedbackBL(IFeedbackRL feedbackRL)
        {
            this.feedbackRL = feedbackRL;
        }

        public FeedbackModel AddFeedback(FeedbackModel feedback, int userId)
        {
            try
            {
                return this.feedbackRL.AddFeedback(feedback, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<FeedbackResponse> GetRecordsByBookId(int bookId)
        {
            try
            {
                return this.feedbackRL.GetRecordsByBookId(bookId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
