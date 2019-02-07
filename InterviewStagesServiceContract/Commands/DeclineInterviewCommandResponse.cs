using System;
using System.Collections.Generic;
using Ualium.Candidate.Interview.CandidateInterviewStagesServiceContract.Shared;

namespace Ualium.Candidate.Interview.CandidateInterviewStagesServiceContract.Commands
{
    public interface IDeclineInterviewCommandResponse
    {
        Guid InterviewId { get; set; }
        IList<Error> Errors { get; set; }
    }

    public class DeclineInterviewCommandResponse : IDeclineInterviewCommandResponse
    {
        public Guid InterviewId { get; set; }
        public IList<Error> Errors { get; set; }
    }
}