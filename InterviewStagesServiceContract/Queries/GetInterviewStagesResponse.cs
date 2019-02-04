using System.Collections.Generic;
using Ualium.Candidate.Interview.CandidateInterviewStagesServiceContract.Shared;

namespace Ualium.Candidate.Interview.CandidateInterviewStagesServiceContract.Queries
{
    public interface IGetInterviewStagesResponse
    {
        IList<Error> Errors { get; set; }
        IList<CandidateInterviewStage> CandidateInterviewStages { get; set; }
    }

    public class GetInterviewStagesResponse : IGetInterviewStagesResponse
    {
        public IList<CandidateInterviewStage> CandidateInterviewStages { get; set; }
        public IList<Error> Errors { get; set; }
    }
}