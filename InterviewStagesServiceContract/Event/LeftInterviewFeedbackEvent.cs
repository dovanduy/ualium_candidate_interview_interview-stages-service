using Ualium.Candidate.Interview.CandidateInterviewStagesServiceContract.Shared;

namespace Ualium.Candidate.Interview.CandidateInterviewStagesServiceContract.Event
{
    public interface ILeftInterviewFeedbackEvent
    {
        CandidateInterviewStage CandidateInterviewStage { get; set; }
    }

    public class LeftInterviewFeedbackEvent : ILeftInterviewFeedbackEvent
    {
        public CandidateInterviewStage CandidateInterviewStage { get; set; }
    }
}