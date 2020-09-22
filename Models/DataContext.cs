using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Project_ClicLikes.Models;

namespace Project_ClicLikes.Models
{
    public class DataContext : DbContext
    {
        public DataContext()
        {

        }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", optional: false);
            var configuration = builder.Build();

            var connectionString = configuration.GetConnectionString("Project_DB").ToString();
            optionsBuilder.UseSqlServer(connectionString);

            //if (!optionsBuilder.IsConfigured)
            //{
            //    var builder = new ConfigurationBuilder();
            //    builder.AddJsonFile("appsettings.json", optional: false);
            //    var configuration = builder.Build();

            //    var connectionString = configuration.GetConnectionString("Project_DB").ToString();
            //    optionsBuilder.UseSqlServer(connectionString);
            //}
        }

        public virtual DbSet<Like> Likes { get; set; }
        public virtual DbSet<Article> Articles { get; set; }
    }
}
