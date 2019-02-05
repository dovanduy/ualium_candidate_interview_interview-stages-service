using System.Collections.Generic;
using Ualium.Candidate.Interview.CandidateInterviewStagesServiceContract.Shared;

namespace Ualium.Candidate.Interview.CandidateInterviewStagesServiceContract.Commands
{
    public interface ILeaveInterviewFeedbackCommandResponse
    {
        IList<Error> Errors { get; set; }
    }

    public class LeaveInterviewFeedbackCommandResponse : ILeaveInterviewFeedbackCommandResponse
    {
        public IList<Error> Errors { get; set; }
    }
}