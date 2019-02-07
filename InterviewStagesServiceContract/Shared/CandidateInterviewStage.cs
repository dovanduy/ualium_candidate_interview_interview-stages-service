using System;
using System.Collections.Generic;

namespace Ualium.Candidate.Interview.CandidateInterviewStagesServiceContract.Shared
{
    public class CandidateInterviewStage
    {
        public Guid InterviewStageId { get; set; }
        public Guid CandidateId { get; set; }
        public Guid EmployerPositionId { get; set; }
        public IList<Interview> Interviews { get; set; }
    }

    public class Interview
    {
        public Guid InterviewId { get; set; }
        public Feedback CandidateFeedback { get; set; }
        public Feedback EmployerFeedback { get; set; }
        public int InterviewStageEnum { get; set; }
        public int InterviewStatusEnum { get; set; }
        public DateTime WhenInterviewCompleted { get; set; }
    }

    public class Feedback
    {
        public Guid FeedbackId { get; set; }
        public DateTime WhenFeedbackLeftUtc { get; set; }
        public string FeedbackText { get; set; }
    }
}