using System;
using System.Collections.Generic;
using Ualium.Candidate.Interview.CandidateInterviewStagesServiceContract.Shared;

namespace Ualium.Candidate.Interview.CandidateInterviewStagesServiceContract.Commands
{
    public interface IAcceptInterviewCommandResponse
    {
        Guid InterviewId { get; set; }
        IList<Error> Errors { get; set; }
    }

    public class AcceptInterviewCommandResponse : IAcceptInterviewCommandResponse
    {
        public Guid InterviewId { get; set; }
        public IList<Error> Errors { get; set; }
    }
}