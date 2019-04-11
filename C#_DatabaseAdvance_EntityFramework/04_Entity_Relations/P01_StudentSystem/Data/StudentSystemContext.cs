namespace P01_StudentSystem.Data
{
    using Microsoft.EntityFrameworkCore;
    using P01_StudentSystem.Data.Models;

    public class StudentSystemContext : DbContext
    {
        public StudentSystemContext()
        {

        }

        public StudentSystemContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Homework> HomeworkSubmissions { get; set; }

        public DbSet<Resource> Resources { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<StudentCourse> StudentCourses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            StudentModelCreating(modelBuilder);

            CourseModelCreating(modelBuilder);

            ResourceModelCreating(modelBuilder);

            HomeworkModelCreating(modelBuilder);

            StudentCourseModelCreating(modelBuilder);
        }

        private void StudentCourseModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentCourse>()
                .HasKey(st => new { st.StudentId, st.CourseId });

            modelBuilder.Entity<StudentCourse>()
                .HasOne(st => st.Student)
                .WithMany(c => c.CourseEnrollments)
                .HasForeignKey(st => st.StudentId);

            modelBuilder.Entity<StudentCourse>()
                .HasOne(st => st.Course)
                .WithMany(s => s.StudentsEnrolled)
                .HasForeignKey(st => st.CourseId);
        }

        private void HomeworkModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Homework>(entity =>
            {
                entity.HasKey(h => h.HomeworkId);

                entity.Property(h => h.Content).IsUnicode(false);
            });
        }

        private void ResourceModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Resource>(entity =>
            {
                entity.HasKey(r => r.ResourceId);

                entity.Property(r => r.Name).HasMaxLength(50).IsUnicode(true).IsRequired(false);

                entity.Property(r => r.Url).IsUnicode(false);
            });
        }

        private void CourseModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasKey(c => c.CourseId);

                entity.Property(c => c.Name).IsUnicode(true).HasMaxLength(80);

                entity.Property(c => c.Description).IsUnicode(true).IsRequired(false);

                entity
                    .HasMany(c => c.Resources)
                    .WithOne(r => r.Course);

                entity
                    .HasMany(c => c.HomeworkSubmissions)
                    .WithOne(h => h.Course);
            });
        }

        private void StudentModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(entity =>
            {
                entity
                    .HasKey(p => p.StudentId);

                entity
                    .Property(s => s.Name)
                    .IsUnicode(true)
                    .HasMaxLength(100);

                entity
                    .Property(s => s.PhoneNumber)
                    .HasColumnType("char(10)")
                    .IsUnicode(false)
                    .IsRequired(false);

                entity
                    .HasMany(s => s.HomeworkSubmissions)
                    .WithOne(h => h.Student);
            });
        }
    }
}