﻿using System;
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
    public class DeclineInterviewRequestCommandHandler : IConsumer<IDeclineInterviewRequestCommandRequest>
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public async Task Consume(ConsumeContext<IDeclineInterviewRequestCommandRequest> context)
        {
            await Task.Factory.StartNew(() =>
            {
                /*
                * Save Candidate Interview Stage for the Record - Initiated when Candidate Declined Employer Interview Request
                */
                try
                {
                    using (var connection = new SqlConnection(InterviewStagesServiceDbContext.Connectionstring.GetConnection))
                    {
                        connection.Open();

                        var insertInterviewStageCmd  = connection.CreateCommand();

                        insertInterviewStageCmd.Parameters.AddWithValue("CandidateId", context.Message.CandidateId);
                        insertInterviewStageCmd.Parameters.AddWithValue("EmployerPositionId", context.Message.EmployerPositionId);
                        insertInterviewStageCmd.Parameters.AddWithValue("WhenStatusChangedUtc", context.Message.WhenStatusChangedUtc);

                        var insertInterviewStageSql = @" 
                            DECLARE @CandidateInterviewStageId uniqueidentifier = '00000000-0000-0000-0000-000000000000';

                            SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                            BEGIN TRANSACTION
                              IF NOT EXISTS (SELECT
                                  1
                                FROM [dbo].[CandidateInterviewStages]
                                WITH (UPDLOCK)
                                WHERE EmployerPositionId = @EmployerPositionId)

                                SET @CandidateInterviewStageId = NEWID();

                                INSERT INTO [dbo].[CandidateInterviewStages] (CandidateInterviewStageId, CandidateId, EmployerPositionId, WhenAcceptedUtc)
                                  VALUES (@CandidateInterviewStageId, @CandidateId, @EmployerPositionId, @WhenAcceptedUtc)
                            COMMIT;

                            IF (@CandidateInterviewStageId != '00000000-0000-0000-0000-000000000000')
                            BEGIN
                              SELECT
                                @CandidateInterviewStageId
                            END;";

                        insertInterviewStageCmd.CommandText = insertInterviewStageSql;


                        var candidateInterviewStageId = Guid.Empty;

                        using (var reader = insertInterviewStageCmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                candidateInterviewStageId = reader.GetGuid(0);
                            }
                        }

                        connection.Close();

                        /* Create New Interview for the Record  - First Stage */
                        connection.Open();

                        var insertInterviewCmd = connection.CreateCommand();
                        insertInterviewCmd.Parameters.AddWithValue("CandidateId", context.Message.EmployerPositionId);
                        insertInterviewCmd.Parameters.AddWithValue("InterviewStageEnum", InterviewStageEnum.PhoneInterview);
                        insertInterviewCmd.Parameters.AddWithValue("InterviewStatusEnum", InterviewStatusEnum.EmployerAcceptedCandidateDeclined);
                        insertInterviewCmd.Parameters.AddWithValue("WhenStatusChangedUtc", context.Message.WhenStatusChangedUtc);
                        insertInterviewCmd.Parameters.AddWithValue("CandidateInterviewStage_CandidateInterviewStageId", candidateInterviewStageId);

                        var insertInterviewSql = @"
                            DECLARE @InterviewId uniqueidentifier = NEWID();

                            SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                            BEGIN TRANSACTION
                              IF NOT EXISTS (SELECT
                                  1
                                FROM [dbo].[Interviews]
                                WITH (UPDLOCK)
                                WHERE CandidateInterviewStage_CandidateInterviewStageId = @CandidateInterviewStage_CandidateInterviewStageId)

                                INSERT INTO [dbo].[Interviews] (InterviewId, InterviewStageEnum, InterviewStatusEnum, WhenStatusChangedUtc, CandidateInterviewStage_CandidateInterviewStageId)
                                  VALUES (@InterviewId, @InterviewStageEnum, @InterviewStatusEnum, @WhenStatusChangedUtc, @CandidateInterviewStage_CandidateInterviewStageId)
                            COMMIT;";

                        insertInterviewCmd.CommandText = insertInterviewSql;
                        insertInterviewCmd.ExecuteNonQuery();

                        connection.Close();

                        context.Respond(new DeclineInterviewRequestCommandResponse());
                    }
                }
                catch (Exception ex)
                {
                    const string message = "Could not save Save Candidate Interview Stage.";

                    Logger.Error($"{message} {ex.Message}");

                    var response = new DeclineInterviewRequestCommandResponse { Errors = new List<Error>() };
                    response.Errors.Add(new Error(23001, ex.Message, message));

                    throw new Exception(message, ex);
                }
            });
        }
    }
}