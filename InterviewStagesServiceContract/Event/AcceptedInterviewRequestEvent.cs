using Ualium.Candidate.Interview.CandidateInterviewStagesServiceContract.Shared;

namespace Ualium.Candidate.Interview.CandidateInterviewStagesServiceContract.Event
{
    public interface IAcceptedInterviewRequestEvent
    {
        CandidateInterviewStage CandidateInterviewStage { get; set; }
    }

    public class AcceptedInterviewRequestEvent : IAcceptedInterviewRequestEvent
    {
        public CandidateInterviewStage CandidateInterviewStage { get; set; }
    }
}