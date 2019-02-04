using System;

namespace Ualium.Candidate.Interview.CandidateInterviewStagesServiceContract.Queries
{
    public interface IGetInterviewStagesRequest
    {
        Guid CandidateId { get; set; }
    }

    public class GetInterviewStagesRequest : IGetInterviewStagesRequest
    {
        public Guid CandidateId { get; set; }
    }
}