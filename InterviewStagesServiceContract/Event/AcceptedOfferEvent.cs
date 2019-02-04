using Ualium.Candidate.Interview.CandidateInterviewStagesServiceContract.Shared;

namespace Ualium.Candidate.Interview.CandidateInterviewStagesServiceContract.Event
{
    public interface IAcceptedOfferEvent
    {
        CandidateInterviewStage CandidateInterviewStage { get; set; }
    }

    public class AcceptedOfferEvent : IAcceptedOfferEvent
    {
        public CandidateInterviewStage CandidateInterviewStage { get; set; }
    }
}