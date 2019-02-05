using System;
using System.Collections.Generic;

namespace Ualium.Candidate.Interview.InterviewStagesService.Entities
{
    public class CandidateInterviewStage
    {
        public Guid CandidateInterviewStageId { get; set; }
        public Guid CandidateId { get; set; }
        public Guid EmployerPositionId { get; set; }
        public IList<Interview> Interviews { get; set; }
    }

    public class Interview
    {
        public Guid InterviewId { get; set; }
        public CandidateFeedback CandidateFeedback { get; set; }
        public EmployerFeedback EmployerFeedback { get; set; }
        public InterviewStageEnum InterviewStageEnum { get; private set; }
        public InterviewStatusEnum InterviewStatusEnum { get; set; }
        public DateTime? WhenInterviewCompleted { get; set; }

        public void ChangeInterviewStage(InterviewStageEnum interviewStage)
        {
            if (InterviewStageEnum == InterviewStageEnum.None && interviewStage == InterviewStageEnum.PhoneInterview)
            {
                InterviewStageEnum = interviewStage;
            }
            else if (InterviewStageEnum == InterviewStageEnum.PhoneInterview && interviewStage == InterviewStageEnum.FaceToFaceInterviewing)
            {
                InterviewStageEnum = interviewStage;
            }
            else if (InterviewStageEnum == InterviewStageEnum.FaceToFaceInterviewing && interviewStage == InterviewStageEnum.Hired)
            {
                InterviewStageEnum = interviewStage;
            }
            else if (InterviewStageEnum == InterviewStageEnum.Hired && interviewStage == InterviewStageEnum.OfferPending)
            {
                InterviewStageEnum = interviewStage;
            }
            else
            {
                throw new ApplicationException($"Wrong InterviewStage: {interviewStage}");
            }
        }
    }
}