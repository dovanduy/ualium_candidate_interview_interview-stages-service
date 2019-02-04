using Ualium.Candidate.Interview.CandidateInterviewStagesServiceContract.Shared;

namespace Ualium.Candidate.Interview.CandidateInterviewStagesServiceContract.Event
{
    public interface IDeclinedInterviewEvent
    {
        CandidateInterviewStage CandidateInterviewStage { get; set; }
    }

    public class DeclinedInterviewEvent : IDeclinedInterviewEvent
    {
        public CandidateInterviewStage CandidateInterviewStage { get; set; }
    }
}