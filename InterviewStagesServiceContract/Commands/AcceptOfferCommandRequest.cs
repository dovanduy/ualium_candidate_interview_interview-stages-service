using System;

namespace Ualium.Candidate.Interview.CandidateInterviewStagesServiceContract.Commands
{
    public interface IAcceptOfferCommandRequest
    {
        Guid CandidateId { get; set; }
        Guid EmployerPositionId { get; set; }
        DateTime WhenOfferAcceptedUtc { get; set; }
    }

    public class AcceptOfferCommandRequest : IAcceptOfferCommandRequest
    {
        public Guid CandidateId { get; set; }
        public Guid EmployerPositionId { get; set; }
        public DateTime WhenOfferAcceptedUtc { get; set; }
    }
}