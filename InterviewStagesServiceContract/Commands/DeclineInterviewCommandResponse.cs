using System.Collections.Generic;
using Ualium.Candidate.Interview.CandidateInterviewStagesServiceContract.Shared;

namespace Ualium.Candidate.Interview.CandidateInterviewStagesServiceContract.Commands
{
    public interface IDeclineInterviewCommandResponse
    {
        IList<Error> Errors { get; set; }
    }

    public class DeclineInterviewCommandResponse : IDeclineInterviewCommandResponse
    {
        public IList<Error> Errors { get; set; }
    }
}