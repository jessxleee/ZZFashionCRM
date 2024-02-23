using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_T04_Team6.Models
{
    public class FeedbackViewModel
    {
        public List<Feedback> feedbackList { get; set; }
        public List<Response> responseList { get; set; }
        public FeedbackViewModel()
        {
            feedbackList = new List<Feedback>();
            responseList = new List<Response>();
        }
    }
}
