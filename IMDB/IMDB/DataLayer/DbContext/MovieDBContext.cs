using IMDB.DataLayer.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace IMDB.Entities
{
    public partial class MovieDBContext : IdentityDbContext
    {
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<Genre> Genre { get; set; }
        public virtual DbSet<Job> Job { get; set; }
        public virtual DbSet<Movie> Movie { get; set; }
        public virtual DbSet<MovieStaff> MovieStaff { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<PersonJobs> PersonJobs { get; set; }
        public virtual DbSet<Rating> Rating { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public MovieDBContext(DbContextOptions<MovieDBContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("Name_Company");

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.Description)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Image).HasColumnName("Image").HasMaxLength(2000).IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.Property(e => e.GenreId).HasColumnName("GenreID");

                entity.Property(e => e.Description)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.MovieId).HasColumnName("MovieID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.Genre)
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Genre_Movie");
            });

            modelBuilder.Entity<Job>(entity =>
            {
                entity.Property(e => e.JobId).HasColumnName("JobID");

                entity.Property(e => e.Description).HasMaxLength(250);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.Property(e => e.MovieId).HasColumnName("MovieID");

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.Duration).HasColumnName("Duration");

                entity.Property(e => e.Image).HasColumnName("Image").HasMaxLength(2000).IsUnicode(false);

                entity.Property(e => e.Rating).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.ReleaseDate).HasColumnType("date");

                entity.Property(e => e.Summary)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TrailerLink)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Movie)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_Movie_Company");
            });

            modelBuilder.Entity<MovieStaff>(entity =>
            {
                entity.HasIndex(e => e.MovieId);

                entity.HasIndex(e => e.PersonJobsId);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.MovieId).HasColumnName("MovieID");

                entity.Property(e => e.PersonJobsId).HasColumnName("PersonJobsID");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.MovieStaff)
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Movie_MovieStaff");

                entity.HasOne(d => d.PersonJobs)
                    .WithMany(p => p.MovieStaff)
                    .HasForeignKey(d => d.PersonJobsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PersonJobs_MovieStaff");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.Property(e => e.Bio)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Image).HasColumnName("Image").HasMaxLength(2000).IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MiddleName).HasColumnType("nchar(10)");
            });

            modelBuilder.Entity<PersonJobs>(entity =>
            {
                entity.HasIndex(e => e.JobId);

                entity.HasIndex(e => e.PersonId);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.JobId).HasColumnName("JobID");

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.PersonJobs)
                    .HasForeignKey(d => d.JobId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Job_PersonJobs");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.PersonJobs)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Person_PersonJobs");
            });

            modelBuilder.Entity<Rating>(entity =>
            {
                entity.HasIndex(e => e.MovieId)
                    .HasName("FK_MovieID");

                entity.Property(e => e.RatingId).HasColumnName("RatingID");

                entity.Property(e => e.MovieId).HasColumnName("MovieID");
                entity.Property(e => e.MovieRating).HasColumnName("Rating");
                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.RatingNavigation)
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Movie_Rating");
            });

            base.OnModelCreating(modelBuilder);
        }



    }
}
