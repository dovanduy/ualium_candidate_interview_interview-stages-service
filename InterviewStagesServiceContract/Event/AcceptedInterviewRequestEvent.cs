using System;

namespace Ualium.Candidate.Interview.CandidateInterviewStagesServiceContract.Event
{
    public interface IAcceptedInterviewRequestEvent
    {
        Guid InterviewStageId { get; set; }
        Guid EmployerPositionId { get; set; }
        Shared.Interview Interview { get; set; }
    }

    public class AcceptedInterviewRequestEvent : IAcceptedInterviewRequestEvent
    {
        public Guid InterviewStageId { get; set; }
        public  Guid EmployerPositionId { get; set; }
        public  Shared.Interview Interview { get; set; }
    }
}