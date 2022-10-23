using Microsoft.EntityFrameworkCore;
using Data.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Design;

namespace Data.Context
{
    public class SchoolContext: DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options)
                : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<OfficeAssignment> OfficeAssignments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().ToTable("Course")
                .HasMany(c => c.Instructors)
                .WithMany(c => c.Courses);

            modelBuilder.Entity<Student>().ToTable("Student");
            modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
        }

        public class SchoolContextFactory : IDesignTimeDbContextFactory<SchoolContext>
        {
            public SchoolContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<SchoolContext>();
                optionsBuilder.UseSqlServer();

                return new SchoolContext(optionsBuilder.Options);
            }
        }
    }
}