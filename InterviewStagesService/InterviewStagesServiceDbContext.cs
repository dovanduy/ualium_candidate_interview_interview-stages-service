using System.Data.Entity;
using System.Data.SqlClient;
using Ualium.Candidate.Interview.InterviewStagesService.Entities;
using Ualium.ServiceConfigurator;

namespace Ualium.Candidate.Interview.InterviewStagesService
{
    public class InterviewStagesServiceDbContext : DbContext
    {
        public DbSet<CandidateInterviewStage> CandidateInterviewStages { get; set; }

        public InterviewStagesServiceDbContext() : base(Connectionstring.GetConnection)
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<InterviewStagesServiceDbContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }

        public static class Connectionstring
        {
            private static readonly SqlConnectionStringBuilder SqlConnectionString;

            static Connectionstring()
            {
                SqlConnectionString = new SqlConnectionStringBuilder()
                {
                    ConnectTimeout = 30,
                    DataSource = Settings.GetSettings("RelationalDb.DataSource"),
                    InitialCatalog = $"{Settings.GetSettings("RelationalDb.CataloguePrifix")}" +
                                     $"{Settings.GetSettings("RelationalDb.Catalog")}",
                    UserID = Settings.GetSettings("RelationalDb.UserId"),
                    Password = Settings.GetSettings("RelationalDb.Password"),
                };
            }

            public static string GetConnection => SqlConnectionString.ConnectionString;
        }
    }
}