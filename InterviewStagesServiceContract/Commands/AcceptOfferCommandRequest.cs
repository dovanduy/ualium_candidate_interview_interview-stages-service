using System;

namespace Ualium.Candidate.Interview.CandidateInterviewStagesServiceContract.Commands
{
    public interface IAcceptOfferCommandRequest
    {
        Guid CandidateInterviewStageId { get; set; }
        Guid CandidateId { get; set; }
        Guid EmployerPositionId { get; set; }
        DateTime WhenStatusChangedUtc { get; set; }
    }

    public class AcceptOfferCommandRequest : IAcceptOfferCommandRequest
    {
        public Guid CandidateInterviewStageId { get; set; }
        public Guid CandidateId { get; set; }
        public Guid EmployerPositionId { get; set; }
        public DateTime WhenStatusChangedUtc { get; set; }
    }
}