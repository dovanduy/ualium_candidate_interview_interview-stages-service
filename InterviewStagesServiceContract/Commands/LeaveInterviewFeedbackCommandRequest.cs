using System;

namespace Ualium.Candidate.Interview.CandidateInterviewStagesServiceContract.Commands
{
    public interface ILeaveInterviewFeedbackCommandRequest
    {
        Guid CandidateId { get; set; }
        Guid EmployerPositionId { get; set; }
        DateTime WhenFeedbackLeftUtc { get; set; }
        string FeedbackText { get; set; }
    }

    public class LeaveInterviewFeedbackCommandRequest : ILeaveInterviewFeedbackCommandRequest
    {
        public Guid CandidateId { get; set; }
        public Guid EmployerPositionId { get; set; }
        public DateTime WhenFeedbackLeftUtc { get; set; }
        public string FeedbackText { get; set; }
    }
}