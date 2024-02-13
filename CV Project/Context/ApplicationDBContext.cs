using Microsoft.EntityFrameworkCore;
using CV_Project.Models;

namespace CV_Project.Context
{
    public class ApplicationDBContext:DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> contextOptions)
            : base(contextOptions)
        {

        }
        public DbSet<InputForm> input { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<InputForm>(entity =>
            {
                entity.HasKey(x => x.Id);

            });
            //modelBuilder.Entity<InputForm>()
            //    .Ignore(e => e.ProfilePicture);
        }
    }
}
