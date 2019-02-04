using System;

namespace Ualium.Candidate.Interview.CandidateInterviewStagesServiceContract.Commands
{
    public interface IDeclineOfferCommandRequest
    {
        Guid CandidateId { get; set; }
        Guid EmployerPositionId { get; set; }
        DateTime WhenOfferDeclinedUtc { get; set; }
    }

    public class DeclineOfferCommandRequest : IDeclineOfferCommandRequest
    {
        public Guid CandidateId { get; set; }
        public Guid EmployerPositionId { get; set; }
        public DateTime WhenOfferDeclinedUtc { get; set; }
    }
}