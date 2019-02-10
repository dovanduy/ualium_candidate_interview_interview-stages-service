using System;

namespace Ualium.Candidate.Interview.CandidateInterviewStagesServiceContract.Event
{
    public interface IAcceptedInterviewEvent
    {
        Guid EmployerPositionId { get; set; }
        Shared.Interview Interview { get; set; }
    }

    public class AcceptedInterviewEvent : IAcceptedInterviewEvent
    {
        public Guid EmployerPositionId { get; set; }
        public Shared.Interview Interview { get; set; }
    }
}