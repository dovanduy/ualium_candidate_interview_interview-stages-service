﻿using MassTransit;
using NLog;
using System;
using Ualium.ServiceConfigurator;

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
                    //e.Consumer<>();
                });

                x.ReceiveEndpoint(host, CandidateInterviewStagesServiceContract.RabbitMqQueues.Ualium_External_Candidate_Interview_DeclineInterviewRequest_Command, e =>
                {
                    e.PrefetchCount = 50;
                    //e.Consumer<>();
                });


                x.ReceiveEndpoint(host, CandidateInterviewStagesServiceContract.RabbitMqQueues.Ualium_External_Candidate_Interview_AcceptInterview_Command, e =>
                {
                    e.PrefetchCount = 50;
                    //e.Consumer<>();
                });

                x.ReceiveEndpoint(host, CandidateInterviewStagesServiceContract.RabbitMqQueues.Ualium_External_Candidate_Interview_DeclineInterview_Command, e =>
                {
                    e.PrefetchCount = 50;
                    //e.Consumer<>();
                });


                x.ReceiveEndpoint(host, CandidateInterviewStagesServiceContract.RabbitMqQueues.Ualium_External_Candidate_Interview_AcceptOffer_Command, e =>
                {
                    e.PrefetchCount = 50;
                    //e.Consumer<>();
                });

                x.ReceiveEndpoint(host, CandidateInterviewStagesServiceContract.RabbitMqQueues.Ualium_External_Candidate_Interview_DeclineOffer_Command, e =>
                {
                    e.PrefetchCount = 50;
                    //e.Consumer<>();
                });


                /* Queries */
                x.ReceiveEndpoint(host, CandidateInterviewStagesServiceContract.RabbitMqQueues.Ualium_External_Candidate_Interview_GetInterviewStage_Query, e =>
                {
                    e.PrefetchCount = 50;
                    //e.Consumer<>();
                });

                x.ReceiveEndpoint(host, CandidateInterviewStagesServiceContract.RabbitMqQueues.Ualium_External_Candidate_Interview_GetInterviewStages_Query, e =>
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