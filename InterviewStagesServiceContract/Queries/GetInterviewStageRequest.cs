using System;

namespace Ualium.Candidate.Interview.CandidateInterviewStagesServiceContract.Queries
{
    public interface IGetInterviewStageRequest
    {
        Guid CandidateId { get; set; }
        Guid EmployerId { get; set; }
        Guid EmployerPositionId { get; set; }
    }

    public class GetInterviewStageRequest : IGetInterviewStageRequest
    {
        public Guid CandidateId { get; set; }
        public Guid EmployerId { get; set; }
        public Guid EmployerPositionId { get; set; }
    }
}