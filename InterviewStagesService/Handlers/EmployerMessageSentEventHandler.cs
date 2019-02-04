using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using MassTransit;
using NLog;

namespace Ualium.Candidate.Interview.InterviewStagesService.Handlers
{
    public class EmployerMessageSentEventHandler : IConsumer<string>
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public async Task Consume(ConsumeContext<string> context)
        {
            await Task.Factory.StartNew(() =>
            {
                /*
                * Save Employer's Sent Message Event
                */
                try
                {
                    //using (var connection = new SqlConnection(InterviewStagesServiceDbContext.Connectionstring.GetConnection))
                    //{
                    //    connection.Open();

                    //    var insertMessagesCmd = connection.CreateCommand();

                    //    insertMessagesCmd.Parameters.AddWithValue("InterviewRequestId", context.Message.InterviewRequestId);
                    //    insertMessagesCmd.Parameters.AddWithValue("MessageText", context.Message.InterviewRequesMessage.MessageText);
                    //    insertMessagesCmd.Parameters.AddWithValue("WhenSentUtc", context.Message.InterviewRequesMessage.WhenSentUtc);
                    //    insertMessagesCmd.Parameters.AddWithValue("IsFromEmployer", true);

                    //    var insertMessagesSql = @" 
                    //        DECLARE @MessageId uniqueidentifier = NEWID();

                    //        INSERT INTO [dbo].[Messages] (MessageId, WhenSentUtc, MessageText, IsFromEmployer, InterviewRequest_InterviewRequestId)
                    //          VALUES (@MessageId, @WhenSentUtc, @MessageText, @IsFromEmployer, @InterviewRequestId);

                    //        SELECT
                    //          @MessageId;";

                    //    insertMessagesCmd.CommandText = insertMessagesSql;
                    //    insertMessagesCmd.ExecuteNonQuery();

                    //    connection.Close();
                    //}
                }
                catch (Exception ex)
                {
                    const string message = "Could not save Employer's Sent Message Event.";

                    Logger.Error($"{message} {ex.Message}");

                    throw new Exception(message, ex);
                }
            });
        }
    }
}