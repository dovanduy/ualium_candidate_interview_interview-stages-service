using System;
using System.Collections.Generic;

namespace Ualium.Candidate.Interview.CandidateInterviewStagesServiceContract.Shared
{
    public class AcceptInterview
    {
        public Guid CandidateId { get; set; }
        public Guid EmployerId { get; set; }
        public Guid EmployerPositionId { get; set; }
    }
}