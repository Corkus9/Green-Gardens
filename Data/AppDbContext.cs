using Microsoft.EntityFrameworkCore;
using ToDoExampleAndy.Model;

namespace ToDoExampleAndy.Data
{
    public class AppDbContext : DbContext
    {
        // Define a DbSet for TaskModel. This represents the Tasks table in the database.
        public DbSet<TaskModel> Tasks { get; set; }

        // Define a DbSet for UserModel. This represents the Users table in the database.
        public DbSet<UserModel> Users { get; set; }

        public DbSet<ProductModel> Products { get; set; }
        public object Items { get; internal set; }

        public DbSet<SalesModel> Sales { get; set; }

        // Constructor for the AppDbContext, receiving DbContextOptions of AppDbContext type.
        // It passes these options to the base DbContext class.
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            // The base constructor handles the options.
        }
    }
}
