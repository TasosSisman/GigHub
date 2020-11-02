using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GigHub.Models
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
		public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }


        public DbSet<Gig> Gigs { get; set; }
        public DbSet<Genre> Genres { get; set; }
		public DbSet<Attendance> Attendances { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
            modelBuilder.Entity<Attendance>()
                .HasRequired(async => async.Gig)
                .WithMany()
                .WillCascadeOnDelete(false);

			base.OnModelCreating(modelBuilder);
		}
	}
}