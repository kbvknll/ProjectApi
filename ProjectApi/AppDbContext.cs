using Microsoft.EntityFrameworkCore;
using ProjectApi.Models;

namespace ProjectApi
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Models.Task> Tasks { get; set; }
        public DbSet<TaskEmployee> TaskEmployees { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.NoAction); 

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.User)
                .WithOne(u => u.Employee)
                .HasForeignKey<Employee>(e => e.UserId)
                .OnDelete(DeleteBehavior.NoAction); 

            modelBuilder.Entity<Project>()
                .HasMany(p => p.Tasks)
                .WithOne(t => t.Project)
                .HasForeignKey(t => t.ProjectId)
                .OnDelete(DeleteBehavior.NoAction); 

            modelBuilder.Entity<Models.Task>()
                .HasMany(t => t.TaskEmployees)
                .WithOne(te => te.Task)
                .HasForeignKey(te => te.TaskId)
                .OnDelete(DeleteBehavior.NoAction); 

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.TaskEmployees)
                .WithOne(te => te.Employee)
                .HasForeignKey(te => te.EmployeeId)
                .OnDelete(DeleteBehavior.NoAction); 

            modelBuilder.Entity<TaskEmployee>()
                .HasKey(te => new { te.TaskId, te.EmployeeId });
        }
    }
}
