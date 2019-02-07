namespace Ualium.Candidate.Interview.InterviewStagesService.Entities
{
    public enum InterviewStatusEnum
    {
        None = 0,
        CandidateAcceptedEmployerDeclined = 1,
        EmployerAcceptedCandidateDeclined = 2,
        CandidateAcceptedEmployerPending = 3,
        EmployerAcceptedCandidatePending = 4,
        CandidateDeclinedEmployerPending = 5,
        EmployerDeclinedCandidatePending = 6,
        CandidateEmployerAccepted = 7,
        CandidateEmployerDeclined = 8
    }
}