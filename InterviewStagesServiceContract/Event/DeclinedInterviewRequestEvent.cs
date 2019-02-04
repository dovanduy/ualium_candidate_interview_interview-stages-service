using Ualium.Candidate.Interview.CandidateInterviewStagesServiceContract.Shared;

namespace Ualium.Candidate.Interview.CandidateInterviewStagesServiceContract.Event
{
    public interface IDeclinedInterviewRequestEvent
    {
        CandidateInterviewStage CandidateInterviewStage { get; set; }
    }

    public class DeclinedInterviewRequestEvent : IDeclinedInterviewRequestEvent
    {
        public CandidateInterviewStage CandidateInterviewStage { get; set; }
    }
}