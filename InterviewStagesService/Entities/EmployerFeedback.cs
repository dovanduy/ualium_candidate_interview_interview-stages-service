using System;

namespace Ualium.Candidate.Interview.InterviewStagesService.Entities
{
    public class EmployerFeedback
    {
        public Guid EmployerFeedbackId { get; set; }
        public DateTime WhenFeedbackLeftUtc { get; set; }
        public string FeedbackText { get; set; }
    }
}