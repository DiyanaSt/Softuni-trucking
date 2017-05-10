namespace DSTrucking
{
    using DAL.Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Linq;

    public class DSContext : DbContext
    {   public DSContext()
            : base("name=DSContext")
        {
        }

        public DbSet<Candidate> Candidates { get; set; }

        public DbSet<WorkHistory> WorkHistory { get; set; }

        public DbSet<State> States { get; set; }

        public DbSet<JobApplication> JobApplications { get; set; }
        public DbSet<CDLInformation> CDLInformations { get; set; }

        public DbSet<File> Files { get; set; }

        public DbSet<Experience> Experiences { get; set; }

        public DbSet<ExperiencePeriod> AmmountOfExperiences { get; set; }

        public DbSet<NewsLetterEmail> NewsLetterEmails { get; set; }

        public DbSet<JobPossition> JobPossitions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<JobApplication>().HasMany(x => x.Experiences).WithMany(x => x.JobApplications).Map(x =>
            {
                x.ToTable("JobApplicationExperience");
                x.MapLeftKey("JobApplicationId");
                x.MapRightKey("ExperienceId");
            });

            modelBuilder.Entity<Experience>().HasMany(x => x.JobApplications).WithMany(x => x.Experiences).Map(x =>
            {
                x.ToTable("JobApplicationExperience");
                x.MapLeftKey("ExperienceId");
                x.MapRightKey("JobApplicationId");
            });
            
            base.OnModelCreating(modelBuilder);
        }

    }

}