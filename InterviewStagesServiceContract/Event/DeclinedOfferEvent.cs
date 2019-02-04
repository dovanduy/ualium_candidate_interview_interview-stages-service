using Ualium.Candidate.Interview.CandidateInterviewStagesServiceContract.Shared;

namespace Ualium.Candidate.Interview.CandidateInterviewStagesServiceContract.Event
{
    public interface IDeclinedOfferEvent
    {
        CandidateInterviewStage CandidateInterviewStage { get; set; }
    }

    public class DeclinedOfferEvent : IDeclinedOfferEvent
    {
        public CandidateInterviewStage CandidateInterviewStage { get; set; }
    }
}