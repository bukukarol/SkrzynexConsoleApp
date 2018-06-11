using Microsoft.EntityFrameworkCore;

namespace SkrzynexConsoleApp.DbModel
{
    public partial class skrzynexContext : DbContext
    {
        public skrzynexContext()
        {
        }

        public skrzynexContext(DbContextOptions<skrzynexContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Sprinkeltasks> Sprinkeltasks { get; set; }
        public virtual DbSet<Sprinkletaskaction> Sprinkletaskaction { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                //optionsBuilder.UseMySql("datasource=127.0.0.1;port=3306;username=root;password=;database=skrzynex;TreatTinyAsBoolean=false");
                optionsBuilder.UseMySql("Server=localhost;port=3306;username=root;password=karol123;database=skrzynex;TreatTinyAsBoolean=false");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sprinkeltasks>(entity =>
            {
                entity.ToTable("sprinkeltasks");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.CreationTime).HasColumnType("datetime");

                entity.Property(e => e.Deleted)
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.WateringMode).HasColumnType("int(11)");
            });

            modelBuilder.Entity<Sprinkletaskaction>(entity =>
            {
                entity.ToTable("sprinkletaskaction");

                entity.HasIndex(e => e.TaskId)
                    .HasName("FK_SprinkleTask");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Line).HasColumnType("int(11)");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.TaskId).HasColumnType("int(11)");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.Sprinkletaskaction)
                    .HasForeignKey(d => d.TaskId)
                    .HasConstraintName("FK_SprinkleTask");
            });
        }
    }
}
