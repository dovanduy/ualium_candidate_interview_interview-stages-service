using System;

namespace Ualium.Candidate.Interview.CandidateInterviewStagesServiceContract.Queries
{
    public interface IGetInterviewStagesQueryRequest
    {
        Guid CandidateId { get; set; }
    }

    public class GetInterviewStagesQueryRequest : IGetInterviewStagesQueryRequest
    {
        public Guid CandidateId { get; set; }
    }
}