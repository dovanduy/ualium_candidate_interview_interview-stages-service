﻿using System;

namespace Ualium.Candidate.Interview.CandidateInterviewStagesServiceContract.Commands
{
    public interface IDeclineInterviewRequestCommandRequest
    {
        Guid CandidateId { get; set; }
        Guid EmployerPositionId { get; set; }
        DateTime WhenStatusChangedUtc { get; set; }
    }

    public class DeclineInterviewRequestCommandRequest : IDeclineInterviewRequestCommandRequest
    {
        public Guid CandidateId { get; set; }
        public Guid EmployerPositionId { get; set; }
        public DateTime WhenStatusChangedUtc { get; set; }
    }
}