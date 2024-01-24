using Knowledge_Managment_System2.Model;
using Knowledge_Managment_System2.Model.UserDTOs;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Knowledge_Managment_System2
{
    public class SystemDbContext : IdentityDbContext<Employee, Permission ,string>
    {
        //DbContext:
        public SystemDbContext() : base() { }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<Record> Records { get; set; }
        public DbSet<Link> Links { get; set; }
        public DbSet<FileR> FileRs { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Achievement> Achievements { get; set; }

        public SystemDbContext(DbContextOptions<SystemDbContext> options) : base(options)
        { Database.EnsureCreated(); }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Property for entity:

            //tb.1 Position Property
            modelBuilder.Entity<Position>(r =>
            {
                r.Property(ro => ro.PositionName).HasColumnType("varchar(100)").IsRequired();

            });

            //tb.2 Employee property
            modelBuilder.Entity<Employee>(em =>
            {
                em.Property(emp => emp.FirstName).HasColumnType("varchar(50)").IsRequired();
                em.Property(emp => emp.LastName).HasColumnType("varchar(50)").IsRequired();
                em.Property(emp => emp.Email).HasColumnType("varchar(50)").IsRequired();
                em.Property(emp => emp.PasswordHash).HasColumnType("varchar(800)").IsRequired();
                //em.Property(emp => emp.Experience_Years).HasDefaultValue(0);
                //em.Property(emp => emp.Experience_Type).HasColumnType("varchar(500)").HasDefaultValue("No Experience");
                em.Property(emp => emp.Address).HasColumnType("varchar(100)").IsRequired();
                em.Property(emp => emp.AccessFailedCount).HasDefaultValue(4);
            });

            //tb.3 Experience properity
            modelBuilder.Entity<Experience>(ex =>
            {
                ex.Property(ex => ex.PositionName).HasColumnType("varchar(250)");
                ex.Property(ex => ex.Description).HasColumnType("varchar(500)");
            });

            //tb.3 Permission properity
            //modelBuilder.Entity<Permission>(Per =>
            //{
            //    Per.Property(Permi => Permi.Permission_Name).HasColumnType("varchar(50)").IsRequired();
            //});

            //tb.4 Track
            modelBuilder.Entity<Track>(t =>
            {
                t.Property(tr => tr.TrackName).HasColumnType("varchar(100)").IsRequired();
                t.Property(tr => tr.Created).HasDefaultValueSql("GETDATE()");
                t.Property(tr => tr.RequiredSkills).HasColumnType("varchar(500)").IsRequired();
            });

            //tb.5 Record 
            modelBuilder.Entity<Record>(r =>
            {
                r.Property(rec => rec.RecordName).HasColumnType("varchar(100)").IsRequired();
                r.Property(rec => rec.Status).HasColumnType("bit").HasDefaultValue(false);
                r.Property(rec => rec.Wait).HasColumnType("bit").HasDefaultValue(true);
                r.Property(rec => rec.Description).HasColumnType("varchar(500)").IsRequired();
                r.Property(rec => rec.Mandantory).HasColumnType("bit").HasDefaultValue(false);
                r.Property(rec => rec.Created).HasDefaultValueSql("GETDATE()");
            });

            //tb.6 link
            modelBuilder.Entity<Link>(l =>
            {
                l.Property(li => li.LinkName).HasColumnType("varchar(120)").IsRequired();
                l.Property(li => li.LinkData).HasColumnType("varchar(250)").IsRequired();
            });

            //tb.7 Department 
            modelBuilder.Entity<Department>(d =>
            {
                d.Property(dep => dep.DepartmentName).HasColumnType("varchar(50)").IsRequired();
            });

            //tb.8 File
            modelBuilder.Entity<FileR>(f =>
            {
                f.Property(file => file.FileName).HasColumnType("varchar(550)").IsRequired();
            });

            //tb.9 Course
            // modelBuilder.HasSequence<int>("CourseId", schema:"Course").StartsAt(100).IncrementsBy(1);
            modelBuilder.Entity<Course>(c =>
            {
                c.Property(cou => cou.CourseName).HasColumnType("varchar(100)").IsRequired();
                c.Property(cou => cou.RequiredSkills).HasColumnType("varchar(500)").IsRequired();
                c.Property(cou => cou.Link_course).HasColumnType("varchar(400)").IsRequired();
                c.Property(cou => cou.Mandantory).HasColumnType("bit").HasDefaultValue(false);
                c.Property(cou => cou.Status).HasColumnType("bit").HasDefaultValue(false);
                c.Property(cou => cou.Wait).HasColumnType("bit").HasDefaultValue(true);
                c.Property(cou => cou.Created).HasDefaultValueSql("GETDATE()");
            });

            //Relationship between tabls:

            //RT.1 Department and Position: 1 to many
            modelBuilder.Entity<Position>()
                .HasOne(r => r.Department)
                .WithMany(d => d.Positions);

            //RT.2 Position and Employee: 1 to many
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Position)
                .WithMany(r => r.Employees);

            //RT.3 Position and Track : 1 to many
            modelBuilder.Entity<Track>()
                .HasOne(t => t.Position)
                .WithMany(r => r.Tracks)
                .OnDelete(DeleteBehavior.ClientCascade);

            //RT.4 Employee and Track: 1 to many
            modelBuilder.Entity<Track>()
                .HasOne(t => t.Employee)
                .WithMany(e => e.Tracks);

            //RT.5 Track and Record: 1 to many
            modelBuilder.Entity<Record>()
                .HasOne(r => r.Track)
                .WithMany(t => t.Records)
                .OnDelete(DeleteBehavior.ClientCascade);

            //RT.6 Employee and Record: 1 to many
            modelBuilder.Entity<Record>()
                .HasOne(r => r.Employee)
                .WithMany(e => e.Records);

            //RT.7 Record and link: 1 to many
            modelBuilder.Entity<Link>()
                .HasOne(l => l.Record)
                .WithMany(r => r.Links)
                .OnDelete(DeleteBehavior.ClientCascade);

            //RT.8 Redcord and File: 1 to many
            modelBuilder.Entity<FileR>()
                .HasOne(f => f.Record)
                .WithMany(r => r.FileRs);

            //RT.9 Employee and Experience: 1 to many
            modelBuilder.Entity<Experience>()
                .HasOne(e => e.Employee)
                .WithMany(ex => ex.Experiences);

            //RT.11 Employee and Course: many to many
            modelBuilder.Entity<Employee>()
                .HasMany(Co => Co.Courses)
                .WithMany(em => em.Employees)
                .UsingEntity<Achievement>(
                    a => a
                        .HasOne(ac => ac.Course)
                        .WithMany(c => c.Achievements)
                        .HasForeignKey(ac => ac.CourseId),
                    a => a
                        .HasOne(ac => ac.Employee)
                        .WithMany(e => e.Achievements)
                        .HasForeignKey(ac => ac.Id)
                        .OnDelete(DeleteBehavior.ClientCascade),
                    a =>
                    {
                        a.Property(ach => ach.Description).HasColumnType("varchar(500)");
                        a.Property(ach => ach.StartDate).HasDefaultValueSql("GETDATE()");
                        a.Property(ach => ach.AchievDate).HasColumnType("date");
                        a.HasKey(ac => new { ac.CourseId, ac.Id });
                    }
             );

            //RT .12 Track and Courses: 1 to many
            modelBuilder.Entity<Course>()
                .HasOne(tr => tr.Track)
                .WithMany(co => co.Courses);
            //RT.14 Record and File: 1 to many

            //RT.15 Department and Record: 1 to many
            modelBuilder.Entity<Record>()
                .HasOne(de => de.Department)
                .WithMany(re => re.Records)
                .HasForeignKey(re => re.DepartmentId)
                .OnDelete(DeleteBehavior.ClientCascade);
        }

    }
}