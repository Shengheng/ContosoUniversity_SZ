using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ContosoUniversity_ShZ.Models;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ContosoUniversity_ShZ.DAL
{
    public class SchoolContext:DbContext
    {
        public SchoolContext() : base("SchoolContext") { }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<OfficeAssignment> OfficeAssignments { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //many to many relationship 
            modelBuilder.Entity<Course>()      
                .HasMany(c => c.Instructors).WithMany(i => i.Courses)
                .Map(t => t.MapLeftKey("CourseID")
                    .MapRightKey("InstructorID")
                    .ToTable("CourseInstructor"));
            //enable SP operation for EF
            modelBuilder.Entity<Department>().MapToStoredProcedures();
        }
    }

}