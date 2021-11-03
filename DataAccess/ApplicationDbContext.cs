using Microsoft.EntityFrameworkCore;
using BusinessObject;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DataAccess
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }


        public DbSet<MemberObject> MemberObject { get; set; }

        public DbSet<ProductObject> ProductObject { get; set; }

        public DbSet<OrderObject> OrderObject { get; set; }

        public DbSet<OrderDetailObject> OrderDetailObject { get; set; }

        public IConfiguration Configuration { get; set;}


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfiguration config = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile("appsettings.json", true, true)
                                    .Build();

            string connectionString = config["ConnectionStrings:DefaultConnection"];

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}

