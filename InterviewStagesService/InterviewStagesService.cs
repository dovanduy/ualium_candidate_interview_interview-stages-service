using MassTransit;
using NLog;
using System;
using Ualium.Candidate.Interview.InterviewStagesService.Handlers;
using Ualium.ServiceConfigurator;
using EmployerInterviewStagesServiceContract = Ualium.Employer.Interview.EmployerInterviewStagesServiceContract;

namespace Ualium.Candidate.Interview.InterviewStagesService
{
    public class InterviewStagesService
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static IBusControl Bus => _bus;
        private static IBusControl _bus;

        public InterviewStagesService()
        {
            try
            {
                var cx = new InterviewStagesServiceDbContext();
                cx.Database.Initialize(true);

                _bus = CreateBus();
                _bus.StartAsync();

                Logger.Info("Service Started.");
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }

        static IBusControl CreateBus()
        {
            var rabbitHost = Settings.GetSettings("RabbitMQ.Host");
            var user = Settings.GetSettings("RabbitMQ.User");
            var password = Settings.GetSettings("RabbitMQ.Password");
            var hostAddress = new Uri(string.Format("rabbitmq://{0}/", rabbitHost));

            return MassTransit.Bus.Factory.CreateUsingRabbitMq(x =>
            {
                var host = x.Host(hostAddress, h =>
                {
                    h.Heartbeat(30);
                    h.Username(user);
                    h.Password(password);
                });
              
                /* Commands */
                x.ReceiveEndpoint(host, CandidateInterviewStagesServiceContract.RabbitMqQueues.Ualium_External_Candidate_Interview_AcceptInterviewRequest_Command, e =>
                {
                    e.PrefetchCount = 50;
                    e.Consumer<AcceptInterviewRequestCommandHandler>();
                });

                x.ReceiveEndpoint(host, CandidateInterviewStagesServiceContract.RabbitMqQueues.Ualium_External_Candidate_Interview_DeclineInterviewRequest_Command, e =>
                {
                    e.PrefetchCount = 50;
                    e.Consumer<DeclineInterviewRequestCommandHandler>();
                });


                x.ReceiveEndpoint(host, CandidateInterviewStagesServiceContract.RabbitMqQueues.Ualium_External_Candidate_Interview_AcceptInterview_Command, e =>
                {
                    e.PrefetchCount = 50;
                    e.Consumer<AcceptInterviewCommandHandler>();
                });

                x.ReceiveEndpoint(host, CandidateInterviewStagesServiceContract.RabbitMqQueues.Ualium_External_Candidate_Interview_DeclineInterview_Command, e =>
                {
                    e.PrefetchCount = 50;
                    e.Consumer<DeclineInterviewCommandHandler>();
                });


                x.ReceiveEndpoint(host, CandidateInterviewStagesServiceContract.RabbitMqQueues.Ualium_External_Candidate_Interview_AcceptOffer_Command, e =>
                {
                    e.PrefetchCount = 50;
                    e.Consumer<AcceptOfferCommandHandler>();
                });

                x.ReceiveEndpoint(host, CandidateInterviewStagesServiceContract.RabbitMqQueues.Ualium_External_Candidate_Interview_DeclineOffer_Command, e =>
                {
                    e.PrefetchCount = 50;
                    e.Consumer<DeclineOfferCommandHandler>();
                });


                x.ReceiveEndpoint(host, CandidateInterviewStagesServiceContract.RabbitMqQueues.Ualium_External_Candidate_Interview_LeaveInterviewFeedback_Command, e =>
                {
                    e.PrefetchCount = 50;
                    e.Consumer<LeaveInterviewFeedbackCommandHandler>();
                });


                /* Queries */
                x.ReceiveEndpoint(host, CandidateInterviewStagesServiceContract.RabbitMqQueues.Ualium_External_Candidate_Interview_GetInterviewStages_Query, e =>
                {
                    e.PrefetchCount = 50;
                    e.Consumer<GetInterviewStagesQueryHandler>();
                });

                /* Events */
                x.ReceiveEndpoint(host, EmployerInterviewStagesServiceContract.RabbitMqQueues.Ualium_External_Employer_Interview_AcceptedInterviewRequest_Event, e =>
                {
                    e.PrefetchCount = 50;
                    //e.Consumer<>();
                });

                x.ReceiveEndpoint(host, EmployerInterviewStagesServiceContract.RabbitMqQueues.Ualium_External_Employer_Interview_DeclinedInterviewRequest_Event, e =>
                {
                    e.PrefetchCount = 50;
                    //e.Consumer<>();
                });


                x.ReceiveEndpoint(host, EmployerInterviewStagesServiceContract.RabbitMqQueues.Ualium_Internal_Employer_Interview_AcceptedInterview_Event, e =>
                {
                    e.PrefetchCount = 50;
                    //e.Consumer<>();
                });

                x.ReceiveEndpoint(host, EmployerInterviewStagesServiceContract.RabbitMqQueues.Ualium_Internal_Employer_Interview_DeclinedInterview_Event, e =>
                {
                    e.PrefetchCount = 50;
                    //e.Consumer<>();
                });


                x.ReceiveEndpoint(host, EmployerInterviewStagesServiceContract.RabbitMqQueues.Ualium_External_Employer_Interview_AcceptedOffer_Event, e =>
                {
                    e.PrefetchCount = 50;
                    //e.Consumer<>();
                });

                x.ReceiveEndpoint(host, EmployerInterviewStagesServiceContract.RabbitMqQueues.Ualium_External_Employer_Interview_DeclinedOffer_Event, e =>
                {
                    e.PrefetchCount = 50;
                    //e.Consumer<>();
                });


                x.ReceiveEndpoint(host, EmployerInterviewStagesServiceContract.RabbitMqQueues.Ualium_Internal_Employer_Interview_LeftInterviewFeedback_Event, e =>
                {
                    e.PrefetchCount = 50;
                    //e.Consumer<>();
                });

            });
        }

        public void Shutdown()
        {
            _bus?.Stop();
        }
    }
}