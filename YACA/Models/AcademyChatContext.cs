using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace YACA.Models
{
    public partial class AcademyChatContext : DbContext
    {
        public AcademyChatContext()
        {
        }

        public AcademyChatContext(DbContextOptions<AcademyChatContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Message> Message { get; set; }
        public virtual DbSet<Person> Person { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=tcp:academycsharp.cshbdzdmyfsv.eu-west-1.rds.amazonaws.com,1433;Initial Catalog=AcademyChat;Persist Security Info=False;User ID=konsultti;Password=KonsultinPwd123!;TrustServerCertificate=False;Connection Timeout=30");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>(entity =>
            {
                entity.Property(e => e.MessageId).HasColumnName("Message_Id");

                entity.Property(e => e.Category).HasMaxLength(20);

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");

                entity.Property(e => e.FromPersonId).HasColumnName("From_Person_Id");

                entity.Property(e => e.MessageText)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.SendTime).HasColumnType("datetime");

                entity.Property(e => e.Subject)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ToPersonId).HasColumnName("To_Person_Id");

                entity.HasOne(d => d.FromPerson)
                    .WithMany(p => p.Message)
                    .HasForeignKey(d => d.FromPersonId)
                    .HasConstraintName("FK_Message_Person");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.Property(e => e.PersonId).HasColumnName("Person_Id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Hometown).HasMaxLength(50);

                entity.Property(e => e.NickName)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.Password).HasMaxLength(100);

                entity.Property(e => e.RegistrationDate).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
