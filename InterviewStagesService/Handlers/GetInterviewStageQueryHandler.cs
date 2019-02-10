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
    public class GetInterviewStageQueryHandler : IConsumer<IGetInterviewStageQueryRequest>
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public async Task Consume(ConsumeContext<IGetInterviewStageQueryRequest> context)
        {
            await Task.Factory.StartNew(() =>
            {
                /*
                * Get Interview Stage
                */
                try
                {
                    var interviewStage = new CandidateInterviewStage();

                    using (var connection = new SqlConnection(InterviewStagesServiceDbContext.Connectionstring.GetConnection))
                    {
                        connection.Open();

                        var getInterviewStageCmd = connection.CreateCommand();
                        getInterviewStageCmd.Parameters.AddWithValue("CandidateId", context.Message.CandidateId);
                        getInterviewStageCmd.Parameters.AddWithValue("EmployerPositionId", context.Message.EmployerPositionId);

                        var getInterviewStageSql = @"
                            DECLARE @CandidateInterviewStageId uniqueidentifier = '00000000-0000-0000-0000-000000000000';

                            SELECT
                              @CandidateInterviewStageId = CandidateInterviewStageId
                            FROM [dbo].[CandidateInterviewStages]
                            WHERE EmployerPositionId = @EmployerPositionId;

                            SELECT
                              CandidateInterviewStageId,
                              CandidateId,
                              EmployerPositionId
                            FROM [dbo].[CandidateInterviewStages]
                            WHERE EmployerPositionId = @EmployerPositionId;

                            SELECT
                              InterviewId,
                              InterviewStageEnum,
                              InterviewStatusEnum,
                              WhenStatusChangedUtc
                            FROM [dbo].[Interviews]
                            WHERE CandidateInterviewStage_CandidateInterviewStageId = @CandidateInterviewStageId;

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
                            interviewStage.InterviewStageId = reader.GetGuid(0);
                            interviewStage.CandidateId = reader.GetGuid(1);
                            interviewStage.EmployerPositionId = reader.GetGuid(2);
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            interviewStage.Interviews.Add(new CandidateInterviewStagesServiceContract.Shared.Interview
                            {
                                InterviewId = reader.GetGuid(0),
                                InterviewStageEnum = reader.GetInt32(1),
                                InterviewStatusEnum = reader.GetInt32(2),
                                WhenStatusChangedUtc = reader.GetDateTime(3)
                            });
                        }


                        reader.NextResult();

                        while (reader.Read())
                        {
                            var interviewId = reader.GetGuid(0);
                            var interview = interviewStage.Interviews.SingleOrDefault(x => x.InterviewId == interviewId);

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
                            var interview = interviewStage.Interviews.SingleOrDefault(x => x.InterviewId == interviewId);

                            if (interview == null)
                                continue;

                            interview.EmployerFeedback.FeedbackId = reader.GetGuid(1);
                            interview.EmployerFeedback.WhenFeedbackLeftUtc = reader.GetDateTime(1);
                            interview.EmployerFeedback.FeedbackText = reader.GetString(3);
                        }

                        connection.Close();


                        context.Respond(new GetInterviewStageQueryResponse
                        {
                            CandidateInterviewStage = interviewStage
                        });
                    }
                }
                catch (Exception ex)
                {
                    const string message = "Could not get Interview Stage";

                    Logger.Error($"{message} {ex.Message}");

                    var response = new GetInterviewStageQueryResponse {Errors = new List<Error>()};
                    response.Errors.Add(new Error(23001, ex.Message, message));

                    throw new Exception(message, ex);
                }
            });
        }
    }
}