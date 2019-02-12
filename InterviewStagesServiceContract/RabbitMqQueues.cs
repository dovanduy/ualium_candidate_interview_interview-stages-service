using System.Diagnostics.CodeAnalysis;

namespace Ualium.Candidate.Interview.CandidateInterviewStagesServiceContract
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public static class RabbitMqQueues
    {
        public static string Ualium_External_Candidate_Interview_AcceptInterviewRequest_Command => "Ualium_External_Candidate_Interview_AcceptInterviewRequest_Command";
        public static string Ualium_External_Candidate_Interview_DeclineInterviewRequest_Command => "Ualium_External_Candidate_Interview_DeclineInterviewRequest_Command";

        public static string Ualium_External_Candidate_Interview_AcceptInterview_Command => "Ualium_External_Candidate_Interview_AcceptInterview_Command";
        public static string Ualium_External_Candidate_Interview_DeclineInterview_Command => "Ualium_External_Candidate_Interview_DeclineInterview_Command";

        public static string Ualium_External_Candidate_Interview_LeaveInterviewFeedback_Command => "Ualium_External_Candidate_Interview_LeaveInterviewFeedback_Command";

        public static string Ualium_External_Candidate_Interview_AcceptOffer_Command => "Ualium_External_Candidate_Interview_AcceptOffer_Command";
        public static string Ualium_External_Candidate_Interview_DeclineOffer_Command => "Ualium_External_Candidate_Interview_DeclineOffer_Command";

        public static string Ualium_External_Candidate_Interview_GetInterviewStages_Query => "Ualium_External_Candidate_Interview_GetInterviewStages_Query";

        public static string Ualium_External_Candidate_Interview_AcceptedInterviewRequest_Event => "Ualium_External_Candidate_Interview_AcceptedInterviewRequest_Event";
        public static string Ualium_External_Candidate_Interview_DeclinedInterviewRequest_Event => "Ualium_External_Candidate_Interview_DeclinedInterviewRequest_Event";

        public static string Ualium_Internal_Candidate_Interview_AcceptedInterview_Event => "Ualium_Internal_Candidate_Interview_AcceptedInterview_Event";
        public static string Ualium_Internal_Candidate_Interview_DeclinedInterview_Event => "Ualium_Internal_Candidate_Interview_DeclinedInterview_Event";

        public static string Ualium_Internal_Candidate_Interview_LeftInterviewFeedback_Event => "Ualium_Internal_Candidate_Interview_LeftInterviewFeedback_Event";

        public static string Ualium_External_Candidate_Interview_AcceptedOffer_Event => "Ualium_External_Candidate_Interview_AcceptedOffer_Event";
        public static string Ualium_External_Candidate_Interview_DeclinedOffer_Event => "Ualium_External_Candidate_Interview_DeclinedOffer_Event";
    }
}