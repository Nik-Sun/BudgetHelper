using BudgetHelper.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace BudgetHelper.Core
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
            //this.Database.Migrate();
            this.Database.EnsureDeleted();
            this.Database.EnsureCreated();
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        {
            
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ExpenseType> ExpenseTypes { get; set; }
        public DbSet<Expense> Expenses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlite($"Data Source={Constants.DatabasePath};");
            }
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Category>()
                .ToTable("Categories");

            modelBuilder.Entity<Expense>()
                 .HasOne(e => e.Type)
                 .WithMany(t => t.Expenses)
                 .HasForeignKey(e => e.TypeId);

            modelBuilder.Entity<ExpenseType>()
                .HasOne(en => en.Category)
                .WithMany(c => c.Types)
                .HasForeignKey(en => en.CategoryId);

            

            modelBuilder.Entity<ExpenseType>()
                .HasData(InitialSeed.SeedExpenseTypes());
            modelBuilder.Entity<Category>()
               .HasData(InitialSeed.SeedCategories());
        }
    }
}
