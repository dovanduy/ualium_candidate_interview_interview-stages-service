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
    public class AcceptInterviewCommandHandler : IConsumer<IAcceptInterviewCommandRequest>
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public async Task Consume(ConsumeContext<IAcceptInterviewCommandRequest> context)
        {
            await Task.Factory.StartNew(() =>
            {
                /*
                * Create New Candidate Interview which is part of CandidateInterviewStage
                */
                try
                {
                    using (var connection = new SqlConnection(InterviewStagesServiceDbContext.Connectionstring.GetConnection))
                    {
                        /* Create New Candidate Interview which part of CandidateInterviewStage */
                        connection.Open();

                        var insertInterviewCmd = connection.CreateCommand();
                        insertInterviewCmd.Parameters.AddWithValue("CandidateId", context.Message.CandidateId);
                        insertInterviewCmd.Parameters.AddWithValue("InterviewStageEnum", context.Message.InterviewStageEnum);
                        insertInterviewCmd.Parameters.AddWithValue("InterviewStatusEnum", 3);
                        insertInterviewCmd.Parameters.AddWithValue("CandidateInterviewStage_CandidateInterviewStageId", context.Message.CandidateInterviewStageId);

                        var insertInterviewSql = @"
                            DECLARE @InterviewId uniqueidentifier = NEWID();

                            SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                            BEGIN TRANSACTION
                              IF NOT EXISTS (SELECT
                                  1
                                FROM [dbo].[Interviews]
                                WITH (UPDLOCK)
                                WHERE CandidateInterviewStage_CandidateInterviewStageId = @CandidateInterviewStage_CandidateInterviewStageId)

                                INSERT INTO [dbo].[Interviews] (InterviewId, InterviewStageEnum, InterviewStatusEnum, WhenInterviewCompleted, CandidateInterviewStage_CandidateInterviewStageId)
                                  VALUES (@InterviewId, @InterviewStageEnum, @InterviewStatusEnum, @WhenInterviewCompleted, @CandidateInterviewStage_CandidateInterviewStageId)
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
                    const string message = "Could not Create Candidate Interview.";

                    Logger.Error($"{message} {ex.Message}");

                    var response = new AcceptInterviewCommandResponse {Errors = new List<Error>()};
                    response.Errors.Add(new Error(23001, ex.Message, message));

                    throw new Exception(message, ex);
                }
            });
        }
    }
}