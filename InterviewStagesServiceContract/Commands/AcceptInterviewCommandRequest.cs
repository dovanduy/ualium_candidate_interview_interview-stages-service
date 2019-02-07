using System;

namespace Ualium.Candidate.Interview.CandidateInterviewStagesServiceContract.Commands
{
    public interface IAcceptInterviewCommandRequest
    {
        Guid CandidateId { get; set; }
        Guid CandidateInterviewStageId { get; set; }
        Guid EmployerPositionId { get; set; }
        int InterviewStageEnum { get; set; }
        DateTime WhenStatusChangedUtc { get; set; }
    }

    public class AcceptInterviewCommandRequest : IAcceptInterviewCommandRequest
    {
        public Guid CandidateId { get; set; }
        public Guid CandidateInterviewStageId { get; set; }
        public Guid EmployerPositionId { get; set; }
        public int InterviewStageEnum { get; set; }
        public DateTime WhenStatusChangedUtc { get; set; }
    }
}