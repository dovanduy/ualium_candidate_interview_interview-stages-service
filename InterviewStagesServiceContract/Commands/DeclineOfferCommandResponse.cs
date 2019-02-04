using System.Collections.Generic;
using Ualium.Candidate.Interview.CandidateInterviewStagesServiceContract.Shared;

namespace Ualium.Candidate.Interview.CandidateInterviewStagesServiceContract.Commands
{
    public interface IDeclineOfferCommandResponse
    {
        IList<Error> Errors { get; set; }
    }

    public class DeclineOfferCommandResponse : IDeclineOfferCommandResponse
    {
        public IList<Error> Errors { get; set; }
    }
}