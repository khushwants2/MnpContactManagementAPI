using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MNPDatabaseRepository.Models
{
    public partial class MnpContactManagementContext : DbContext
    {
        public MnpContactManagementContext()
        {
        }

        public MnpContactManagementContext(DbContextOptions<MnpContactManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CompaniesTable> CompaniesTables { get; set; } = null!;
        public virtual DbSet<MnpContactManagement> MnpContactManagements { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSqlLocalDb;Database=MnpContactManagement;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CompaniesTable>(entity =>
            {
                entity.ToTable("CompaniesTable");

                entity.Property(e => e.ComapanyName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MnpContactManagement>(entity =>
            {
                entity.ToTable("MnpContactManagement");

                entity.Property(e => e.Address)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Comments)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyId).HasColumnName("Company_Id");

                entity.Property(e => e.ContactName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.JobTitle)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LastDateContacted).HasColumnType("date");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.MnpContactManagements)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MnpContac__Compa__2C3393D0");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
