using System;

namespace Ualium.Candidate.Interview.InterviewStagesService.Entities
{
    public class CandidateFeedback
    {
        public Guid CandidateFeedbackId { get; set; }
        public DateTime WhenFeedbackLeftUtc { get; set; }
        public string FeedbackText { get; set; }
    }
}