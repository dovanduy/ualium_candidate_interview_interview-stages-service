using System;

namespace Ualium.Candidate.Interview.CandidateInterviewStagesServiceContract.Commands
{
    public interface IDeclineInterviewCommandRequest
    {
        Guid CandidateId { get; set; }
        Guid EmployerPositionId { get; set; }
        DateTime WhenDeclinedUtc { get; set; }
    }

    public class DeclineInterviewCommandRequest : IDeclineInterviewCommandRequest
    {
        public Guid CandidateId { get; set; }
        public Guid EmployerPositionId { get; set; }
        public DateTime WhenDeclinedUtc { get; set; }
    }
}