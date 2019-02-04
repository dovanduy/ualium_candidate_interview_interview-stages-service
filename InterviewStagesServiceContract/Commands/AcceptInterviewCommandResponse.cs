using System.Collections.Generic;
using Ualium.Candidate.Interview.CandidateInterviewStagesServiceContract.Shared;

namespace Ualium.Candidate.Interview.CandidateInterviewStagesServiceContract.Commands
{
    public interface IAcceptInterviewCommandResponse
    {
        CandidateInterviewStage CandidateInterviewStage { get; set; }
        IList<Error> Errors { get; set; }
    }

    public class AcceptInterviewCommandResponse : IAcceptInterviewCommandResponse
    {
        public CandidateInterviewStage CandidateInterviewStage { get; set; }
        public IList<Error> Errors { get; set; }
    }
}