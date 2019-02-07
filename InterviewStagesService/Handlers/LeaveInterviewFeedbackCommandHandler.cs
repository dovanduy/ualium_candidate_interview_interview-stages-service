using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using MassTransit;
using NLog;
using Ualium.Candidate.Interview.CandidateInterviewStagesServiceContract.Commands;
using Ualium.Candidate.Interview.CandidateInterviewStagesServiceContract.Shared;

namespace Ualium.Candidate.Interview.InterviewStagesService.Handlers
{
    public class LeaveInterviewFeedbackCommandHandler : IConsumer<ILeaveInterviewFeedbackCommandRequest>
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public async Task Consume(ConsumeContext<ILeaveInterviewFeedbackCommandRequest> context)
        {
            await Task.Factory.StartNew(() =>
            {
                /*
                * Save Candidate Feedback for Employer
                */
                try
                {
                    using (var connection = new SqlConnection(InterviewStagesServiceDbContext.Connectionstring.GetConnection))
                    {
                        connection.Open();

                        var insertFeedbackCmd = connection.CreateCommand();
                        insertFeedbackCmd.Parameters.AddWithValue("CandidateId", context.Message.CandidateId);
                        insertFeedbackCmd.Parameters.AddWithValue("EmployerPositionId", context.Message.EmployerPositionId);
                        insertFeedbackCmd.Parameters.AddWithValue("FeedbackText", context.Message.FeedbackText);
                        insertFeedbackCmd.Parameters.AddWithValue("WhenFeedbackLeftUtc", context.Message.WhenFeedbackLeftUtc);

                        var insertFeedbackSql = @"
                            DECLARE @CandidateFeedbackId uniqueidentifier = '00000000-0000-0000-0000-000000000000',
                                    @CandidateInterviewStageId uniqueidentifier = '00000000-0000-0000-0000-000000000000';

                            SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                            BEGIN TRANSACTION
                              IF NOT EXISTS (SELECT
                                  1
                                FROM [dbo].[CandidateFeedbacks]
                                WITH (UPDLOCK)
                                WHERE WhenFeedbackLeftUtc = @WhenFeedbackLeftUtc)

                                SET @CandidateFeedbackId = NEWID();

                              INSERT INTO [dbo].[CandidateFeedbacks] (CandidateFeedbackId, WhenFeedbackLeftUtc, FeedbackText)
                                VALUES (@CandidateFeedbackId, @WhenFeedbackLeftUtc, @FeedbackText)
                            COMMIT;

                            SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                            BEGIN TRANSACTION
                              IF NOT EXISTS (SELECT
                                  1
                                FROM [dbo].[Interviews]
                                WITH (UPDLOCK)
                                WHERE WhenFeedbackLeftUtc = @WhenFeedbackLeftUtc)

                                SELECT
                                  @CandidateInterviewStageId = CandidateInterviewStageId
                                FROM [dbo].[CandidateInterviewStages]
                                WHERE EmployerPositionId = @EmployerPositionId;

                              UPDATE [dbo].[Interviews]
                              SET CandidateFeedback_CandidateFeedbackId = @CandidateFeedbackId
                              WHERE CandidateInterviewStage_CandidateInterviewStageId = @CandidateInterviewStageId;
                            COMMIT;";

                        insertFeedbackCmd.CommandText = insertFeedbackSql;
                        insertFeedbackCmd.ExecuteNonQuery();

                        connection.Close();

                        context.Respond(new LeaveInterviewFeedbackCommandResponse());
                    }
                }
                catch (Exception ex)
                {
                    const string message = "Could not insert Candidate Feedback for Employer.";

                    Logger.Error($"{message} {ex.Message}");

                    var response = new LeaveInterviewFeedbackCommandResponse { Errors = new List<Error>()};
                    response.Errors.Add(new Error(23001, ex.Message, message));

                    throw new Exception(message, ex);
                }
            });
        }
    }
}