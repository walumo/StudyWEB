using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace StudyWEB.Models
{
    public partial class StudyDiaryContext : DbContext
    {
        public StudyDiaryContext()
        {
        }

        public StudyDiaryContext(DbContextOptions<StudyDiaryContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Note> Notes { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<Topic> Topics { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-V67EM0K;Database=StudyDiary;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<Note>(entity =>
            {
                entity.ToTable("Note");

                entity.Property(e => e.Note1)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("Note");

                entity.Property(e => e.TaskId).HasColumnName("Task_Id");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.Notes)
                    .HasForeignKey(d => d.TaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Note_Task");
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.ToTable("Task");

                entity.Property(e => e.TaskId).HasColumnName("Task_Id");

                entity.Property(e => e.TaskDeadline).HasColumnType("datetime");

                entity.Property(e => e.TaskDescription)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.TaskTitle)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.TopicId).HasColumnName("Topic_Id");

                entity.HasOne(d => d.Topic)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.TopicId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Task_Topic");
            });

            modelBuilder.Entity<Topic>(entity =>
            {
                entity.ToTable("Topic");

                entity.Property(e => e.TopicId).HasColumnName("Topic_ID");

                entity.Property(e => e.TopicDescription)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.TopicSource)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.TopicTitle)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
