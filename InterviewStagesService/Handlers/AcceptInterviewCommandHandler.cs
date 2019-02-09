using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using MassTransit;
using NLog;
using Ualium.Candidate.Interview.CandidateInterviewStagesServiceContract.Commands;
using Ualium.Candidate.Interview.CandidateInterviewStagesServiceContract.Shared;
using Ualium.Candidate.Interview.InterviewStagesService.Entities;

namespace Ualium.Candidate.Interview.InterviewStagesService.Handlers
{
    public class AcceptInterviewCommandHandler : IConsumer<IAcceptInterviewCommandRequest>
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public async Task Consume(ConsumeContext<IAcceptInterviewCommandRequest> context)
        {
            await Task.Factory.StartNew(() =>
            {
                /*
                 * Initiated  when Candidate Accepts/Declines Employer Interview request.
                 */
                try
                {
                    using (var connection = new SqlConnection(InterviewStagesServiceDbContext.Connectionstring.GetConnection))
                    {
                        /* Create New Candidate Interview which part of CandidateInterviewStage */
                        connection.Open();

                        var insertInterviewCmd = connection.CreateCommand();
                        insertInterviewCmd.Parameters.AddWithValue("WhenStatusChangedUtc", context.Message.WhenStatusChangedUtc);
                        insertInterviewCmd.Parameters.AddWithValue("InterviewStageEnum", context.Message.InterviewStageEnum);
                        insertInterviewCmd.Parameters.AddWithValue("InterviewStatusEnum", InterviewStatusEnum.CandidateAcceptedEmployerPending);
                        insertInterviewCmd.Parameters.AddWithValue("CandidateInterviewStageId", context.Message.CandidateInterviewStageId);

                        var insertInterviewSql = @"
                            DECLARE @InterviewId uniqueidentifier = '00000000-0000-0000-0000-000000000000';

                            SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                            BEGIN TRANSACTION
                              IF NOT EXISTS (SELECT
                                  1
                                FROM [dbo].[Interviews]
                                WITH (UPDLOCK)
                                WHERE CandidateInterviewStage_CandidateInterviewStageId = @CandidateInterviewStageId)

                                SET @InterviewId = NEWID();

                                INSERT INTO [dbo].[Interviews] (InterviewId, InterviewStageEnum, InterviewStatusEnum, WhenInterWhenStatusChangedUtcviewStatusChanged, CandidateInterviewStage_CandidateInterviewStageId)
                                  VALUES (@InterviewId, @InterviewStageEnum, @InterviewStatusEnum, @WhenStatusChangedUtc, @CandidateInterviewStageId)
                            COMMIT;

                            SELECT @InterviewId;";

                        insertInterviewCmd.CommandText = insertInterviewSql;
                        var reader = insertInterviewCmd.ExecuteReader();
                        var interviewId = Guid.Empty;

                        while (reader.Read())
                        {
                            interviewId = reader.GetGuid(0);
                        }

                        connection.Close();

                        context.Respond(new AcceptInterviewCommandResponse
                        {
                            InterviewId = interviewId
                        });
                    }
                }
                catch (Exception ex)
                {
                    const string message = "Could not insert Candidate Interview.";

                    Logger.Error($"{message} {ex.Message}");

                    var response = new AcceptInterviewCommandResponse {Errors = new List<Error>()};
                    response.Errors.Add(new Error(23001, ex.Message, message));

                    throw new Exception(message, ex);
                }
            });
        }
    }
}