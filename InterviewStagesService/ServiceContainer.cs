using System.ServiceProcess;

namespace Ualium.Candidate.Interview.InterviewStagesService
{
    public partial class ServiceContainer : ServiceBase
    {
        private InterviewStagesService _service;
        public ServiceContainer()
        {
            var nsSegments = typeof(ServiceContainer).Namespace.Split('.');

            components = new System.ComponentModel.Container();
            ServiceName = $"{nsSegments[0]}.{nsSegments[1]}.{nsSegments[2]}";

            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            _service = new InterviewStagesService();
        }

        protected override void OnStop()
        {
            _service.Shutdown();
        }
    }
}
