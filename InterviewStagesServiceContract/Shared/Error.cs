namespace Ualium.Candidate.Interview.CandidateInterviewStagesServiceContract.Shared
{
    public class Error
    {
        public int Code { get; set; }
        public string ErrorMessage { get; set; }
        public string Description { get; set; }

        public Error(int code, string errorMessage, string description)
        {
            Code = code;
            ErrorMessage = errorMessage;
            Description = description;
        }
    }
}