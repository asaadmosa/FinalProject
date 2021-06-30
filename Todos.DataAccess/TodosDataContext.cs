using Microsoft.EntityFrameworkCore;
using Todos.DataModel;

namespace Todos.DataAccess
{
    public class TodosDataContext : DbContext
    {
        public DbSet<TodoList> TodoGroups { get; set; }
        public DbSet<TodoItem> TodoItems { get; set; }


        public TodosDataContext()
        {

        }

        public TodosDataContext(DbContextOptions<TodosDataContext> options)
        : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=WINCTRL-B92QSHT; Initial Catalog=todo-2021;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(Microsoft.EntityFrameworkCore.ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoList>()
                .HasMany<TodoItem>(g => g.Items)
                .WithOne(s => s.CurrentList)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}


