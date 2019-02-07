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
    public class AcceptOfferCommandHandler : IConsumer<IAcceptOfferCommandRequest>
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public async Task Consume(ConsumeContext<IAcceptOfferCommandRequest> context)
        {
            await Task.Factory.StartNew(() =>
            {
                /*
                * Save Accepted Candiddate Offer 
                */
                try
                {
                    using (var connection = new SqlConnection(InterviewStagesServiceDbContext.Connectionstring.GetConnection))
                    {
                        /* Create New Candidate Interview which is part of CandidateInterviewStage */
                        connection.Open();

                        var insertInterviewCmd = connection.CreateCommand();
                        insertInterviewCmd.Parameters.AddWithValue("CandidateId", context.Message.CandidateId);
                        insertInterviewCmd.Parameters.AddWithValue("EmployerPositionId", context.Message.EmployerPositionId);
                        insertInterviewCmd.Parameters.AddWithValue("WhenStatusChangedUtc", context.Message.WhenStatusChangedUtc);
                        insertInterviewCmd.Parameters.AddWithValue("InterviewStageEnum", InterviewStageEnum.Offer);
                        insertInterviewCmd.Parameters.AddWithValue("InterviewStatusEnum", InterviewStatusEnum.CandidateAcceptedEmployerPending);

                        var insertInterviewSql = @"
                            DECLARE @InterviewId uniqueidentifier = NEWID();

                            SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                            BEGIN TRANSACTION
                              IF NOT EXISTS (SELECT
                                  1
                                FROM [dbo].[Interviews]
                                WITH (UPDLOCK)
                                WHERE CandidateInterviewStage_CandidateInterviewStageId = @EmployerPositionId)

                                INSERT INTO [dbo].[Interviews] (InterviewId, InterviewStageEnum, InterviewStatusEnum, WhenStatusChangedUtc, CandidateInterviewStage_CandidateInterviewStageId)
                                  VALUES (@InterviewId, @InterviewStageEnum, @InterviewStatusEnum, @WhenStatusChangedUtc, @CandidateInterviewStage_CandidateInterviewStageId)
                            COMMIT;";

                        insertInterviewCmd.CommandText = insertInterviewSql;
                        insertInterviewCmd.ExecuteNonQuery();

                        connection.Close();

                        context.Respond(new AcceptOfferCommandResponse());
                    }
                }
                catch (Exception ex)
                {
                    const string message = "Could not insert Candidate Accepted Offer.";

                    Logger.Error($"{message} {ex.Message}");

                    var response = new AcceptOfferCommandResponse { Errors = new List<Error>()};
                    response.Errors.Add(new Error(23001, ex.Message, message));

                    throw new Exception(message, ex);
                }
            });
        }
    }
}