using System.Collections.Generic;
using Ualium.Candidate.Interview.CandidateInterviewStagesServiceContract.Shared;

namespace Ualium.Candidate.Interview.CandidateInterviewStagesServiceContract.Commands
{
    public interface IAcceptOfferCommandResponse
    {
        IList<Error> Errors { get; set; }
    }

    public class AcceptOfferCommandResponse : IAcceptOfferCommandResponse
    {
        public IList<Error> Errors { get; set; }
    }
}