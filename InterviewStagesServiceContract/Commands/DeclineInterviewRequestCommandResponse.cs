using System.Collections.Generic;
using Ualium.Candidate.Interview.CandidateInterviewStagesServiceContract.Shared;

namespace Ualium.Candidate.Interview.CandidateInterviewStagesServiceContract.Commands
{
    public interface IDeclineInterviewRequestCommandResponse
    {
        IList<Error> Errors { get; set; }
    }

    public class DeclineInterviewRequestCommandResponse : IDeclineInterviewRequestCommandResponse
    {
        public IList<Error> Errors { get; set; }
    }
}