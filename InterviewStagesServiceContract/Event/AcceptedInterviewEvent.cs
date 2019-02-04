using Ualium.Candidate.Interview.CandidateInterviewStagesServiceContract.Shared;

namespace Ualium.Candidate.Interview.CandidateInterviewStagesServiceContract.Event
{
    public interface IAcceptedInterviewEvent
    {
        CandidateInterviewStage CandidateInterviewStage { get; set; }
    }

    public class AcceptedInterviewEvent : IAcceptedInterviewEvent
    {
        public CandidateInterviewStage CandidateInterviewStage { get; set; }
    }
}