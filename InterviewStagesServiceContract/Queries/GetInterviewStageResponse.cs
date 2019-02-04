using System.Collections.Generic;
using Ualium.Candidate.Interview.CandidateInterviewStagesServiceContract.Shared;

namespace Ualium.Candidate.Interview.CandidateInterviewStagesServiceContract.Queries
{
    public interface IGetInterviewStageResponse
    {
        CandidateInterviewStage CandidateInterviewStage { get; set; }
        IList<Error> Errors { get; set; }
    }

    public class GetInterviewStageResponse : IGetInterviewStageResponse
    {
        public CandidateInterviewStage CandidateInterviewStage { get; set; }
        public IList<Error> Errors { get; set; }
    }
}