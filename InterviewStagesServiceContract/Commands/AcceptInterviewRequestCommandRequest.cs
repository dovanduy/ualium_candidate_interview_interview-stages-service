using System;

namespace Ualium.Candidate.Interview.CandidateInterviewStagesServiceContract.Commands
{
    public interface IAcceptInterviewRequestCommandRequest
    {
        Guid CandidateId { get; set; }
        Guid EmployerPositionId { get; set; }
        DateTime WhenAcceptedUtc { get; set; }
    }

    public class AcceptInterviewRequestCommandRequest : IAcceptInterviewRequestCommandRequest
    {
        public Guid CandidateId { get; set; }
        public Guid EmployerPositionId { get; set; }
        public DateTime WhenAcceptedUtc { get; set; }
    }
}