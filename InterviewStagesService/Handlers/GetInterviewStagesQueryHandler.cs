using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using NLog;
using Ualium.Candidate.Interview.CandidateInterviewStagesServiceContract.Queries;
using Ualium.Candidate.Interview.CandidateInterviewStagesServiceContract.Shared;

namespace Ualium.Candidate.Interview.InterviewStagesService.Handlers
{
    public class GetInterviewStagesQueryHandler : IConsumer<IGetInterviewStagesQueryRequest>
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public async Task Consume(ConsumeContext<IGetInterviewStagesQueryRequest> context)
        {
            await Task.Factory.StartNew(() =>
            {
                /*
                * Get Interview Stages
                */
                try
                {
                    var interviewStages = new List<CandidateInterviewStage>();

                    using (var connection = new SqlConnection(InterviewStagesServiceDbContext.Connectionstring.GetConnection))
                    {
                        connection.Open();

                        var getInterviewStageCmd = connection.CreateCommand();
                        getInterviewStageCmd.Parameters.AddWithValue("CandidateId", context.Message.CandidateId);

                        var getInterviewStageSql = @"
                            SELECT
                              CandidateInterviewStageId,
                              CandidateId,
                              EmployerPositionId
                            FROM [dbo].[CandidateInterviewStages]
                            WHERE CandidateId = @CandidateId;

                            SELECT
                              interviewStage.CandidateInterviewStageId,
                              interviewStage.CandidateId,
                              interviewStage.EmployerPositionId,
                              interview.InterviewId,
                              interview.InterviewStageEnum,
                              interview.InterviewStatusEnum,
                              interview.WhenStatusChangedUtc
                            FROM [dbo].[Interviews] interview
                            LEFT JOIN [dbo].[CandidateInterviewStages] interviewStage ON interview.CandidateInterviewStage_CandidateInterviewStageId = interviewStage.CandidateInterviewStageId;
                            WHERE interviewStage.CandidateId = @CandidateId;

                            SELECT
                              interviews.InterviewId,
                              CandidateFeedbackId,
                              WhenFeedbackLeftUtc,
                              FeedbackText
                            FROM [dbo].[CandidateFeedbacks] cFeedbacks
                            LEFT JOIN [dbo].[Interviews] interviews
                              ON cFeedbacks.CandidateFeedbackId = interviews.CandidateFeedback_CandidateFeedbackId
                            WHERE interviews.CandidateInterviewStage_CandidateInterviewStageId = @CandidateInterviewStageId;

                            SELECT
                              interviews.InterviewId,
                              EmployerFeedbackId,
                              WhenFeedbackLeftUtc,
                              FeedbackText
                            FROM [dbo].[EmployerFeedbacks] eFeedbacks
                            LEFT JOIN [dbo].[Interviews] interviews
                              ON eFeedbacks.EmployerFeedbackId = interviews.EmployerFeedback_EmployerFeedbackId
                            WHERE interviews.CandidateInterviewStage_CandidateInterviewStageId = @CandidateInterviewStageId;";

                        getInterviewStageCmd.CommandText = getInterviewStageSql;
                        var reader = getInterviewStageCmd.ExecuteReader();

                        while (reader.Read())
                        {
                            interviewStages.Add(new CandidateInterviewStage
                            {
                                InterviewStageId = reader.GetGuid(0),
                                CandidateId = reader.GetGuid(1),
                                EmployerPositionId = reader.GetGuid(2)
                            });
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            var interviewStageId = reader.GetGuid(0);
                            var interviewStage = interviewStages.SingleOrDefault(x => x.InterviewStageId == interviewStageId);

                            interviewStage?.Interviews.Add(new CandidateInterviewStagesServiceContract.Shared.Interview
                            {
                                InterviewId = reader.GetGuid(0),
                                InterviewStageEnum = reader.GetInt32(1),
                                InterviewStatusEnum = reader.GetInt32(2),
                                WhenInterviewCompleted = reader.GetDateTime(3)
                            });
                        }


                        reader.NextResult();

                        while (reader.Read())
                        {
                            var interviewId = reader.GetGuid(0);

                            var interview = interviewStages
                                .SelectMany(p=>p.Interviews)
                                .SingleOrDefault(x => x.InterviewId == interviewId);

                            if (interview == null)
                                continue;

                            interview.CandidateFeedback.FeedbackId = reader.GetGuid(1);
                            interview.CandidateFeedback.WhenFeedbackLeftUtc = reader.GetDateTime(1);
                            interview.CandidateFeedback.FeedbackText = reader.GetString(3);
                        }


                        reader.NextResult();

                        while (reader.Read())
                        {
                            var interviewId = reader.GetGuid(0);
                            var interview = interviewStages
                                .SelectMany(p => p.Interviews)
                                .SingleOrDefault(x => x.InterviewId == interviewId);

                            if (interview == null)
                                continue;

                            interview.EmployerFeedback.FeedbackId = reader.GetGuid(1);
                            interview.EmployerFeedback.WhenFeedbackLeftUtc = reader.GetDateTime(1);
                            interview.EmployerFeedback.FeedbackText = reader.GetString(3);
                        }

                        connection.Close();

                        context.Respond(new GetInterviewStagesQueryResponse
                        {
                            CandidateInterviewStages = interviewStages
                        });
                    }
                }
                catch (Exception ex)
                {
                    const string message = "Could not get Interview Stages.";

                    Logger.Error($"{message} {ex.Message}");

                    var response = new GetInterviewStagesQueryResponse {Errors = new List<Error>()};
                    response.Errors.Add(new Error(23001, ex.Message, message));

                    throw new Exception(message, ex);
                }
            });
        }
    }
}