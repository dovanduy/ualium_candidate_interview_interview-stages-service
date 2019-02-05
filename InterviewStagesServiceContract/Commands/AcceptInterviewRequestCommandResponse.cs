using System;
using System.Collections.Generic;
using Ualium.Candidate.Interview.CandidateInterviewStagesServiceContract.Shared;

namespace Ualium.Candidate.Interview.CandidateInterviewStagesServiceContract.Commands
{
    public interface IAcceptInterviewRequestCommandResponse
    {
        Guid InterviewStageId { get; set; }
        IList<Error> Errors { get; set; }
    }

    public class AcceptInterviewRequestCommandResponse : IAcceptInterviewRequestCommandResponse
    {
        public Guid InterviewStageId { get; set; }
        public IList<Error> Errors { get; set; }
    }
}