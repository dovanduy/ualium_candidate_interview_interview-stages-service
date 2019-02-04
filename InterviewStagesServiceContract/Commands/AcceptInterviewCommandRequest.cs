using System;

namespace Ualium.Candidate.Interview.CandidateInterviewStagesServiceContract.Commands
{
    public interface IAcceptInterviewCommandRequest
    {
        Guid CandidateId { get; set; }
        Guid EmployerPositionId { get; set; }
        DateTime WhenAcceptedUtc { get; set; }
    }

    public class AcceptInterviewCommandRequest : IAcceptInterviewCommandRequest
    {
        public Guid CandidateId { get; set; }
        public Guid EmployerPositionId { get; set; }
        public DateTime WhenAcceptedUtc { get; set; }
    }
}