using System.Collections.Generic;
using Ualium.Candidate.Interview.CandidateInterviewStagesServiceContract.Shared;

namespace Ualium.Candidate.Interview.CandidateInterviewStagesServiceContract.Queries
{
    public interface IGetInterviewStagesQueryResponse
    {
        IList<Error> Errors { get; set; }
        IList<CandidateInterviewStage> CandidateInterviewStages { get; set; }
    }

    public class GetInterviewStagesQueryResponse : IGetInterviewStagesQueryResponse
    {
        public IList<CandidateInterviewStage> CandidateInterviewStages { get; set; }
        public IList<Error> Errors { get; set; }
    }
}