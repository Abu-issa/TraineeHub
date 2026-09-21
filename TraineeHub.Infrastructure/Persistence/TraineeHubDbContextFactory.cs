using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TraineeHub.Infrastructure.Persistence
{

    public class TraineeHubDbContextFactory : IDesignTimeDbContextFactory<TraineeHubDbContext>
    {
        public TraineeHubDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TraineeHubDbContext>();

         
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=TraineeHubDb02;Trusted_Connection=True;");

            return new TraineeHubDbContext(optionsBuilder.Options);
        }
    }
}