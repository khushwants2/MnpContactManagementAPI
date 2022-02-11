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
                entity.HasData(
                   new CompaniesTable() { Id = 1, ComapanyName = "Google" },
                   new CompaniesTable() { Id = 2, ComapanyName = "Microsoft" },
                   new CompaniesTable() { Id = 3, ComapanyName = "Apple" },
                   new CompaniesTable() { Id = 4, ComapanyName = "Amazon" }
                );
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
                entity.HasData(
                    new MnpContactManagement()
                    {
                        Id = 1,
                        ContactName = "Jane",
                        Address = "123 Main Street Test Toronto",
                        LastDateContacted = DateTime.Now.AddDays(0),
                        JobTitle = "Accountant",
                        Phone = 1234567890,
                        CompanyId = 1,
                        Email = "123@abc.com",
                        Comments = " Comments Section Test 1"

                    },

                    new MnpContactManagement()
                    {
                        Id = 2,
                        ContactName = "Anna",
                        Address = "1231 Main Street Test Montreal",
                        LastDateContacted = DateTime.Now.AddDays(1),
                        JobTitle = "Developer",
                        Phone = 1234567990,
                        CompanyId = 2,
                        Email = "321@abc.com",
                        Comments = " Comments Section Test 2"

                    },

                    new MnpContactManagement()
                    {
                        Id = 3,
                        ContactName = "Rob",
                        Address = "12312 Main Street Test London",
                        LastDateContacted = DateTime.Now.AddDays(3),
                        JobTitle = "Accountant",
                        Phone = 1234567899,
                        CompanyId = 3,
                        Email = "12345@abc.com",
                        Comments = " Comments Section Test 3"

                    },

                    new MnpContactManagement()
                    {
                        Id = 4,
                        ContactName = "Bob Smith",
                        Address = "1234 Main Street Test Brampton",
                        LastDateContacted = DateTime.Now.AddDays(10),
                        JobTitle = "CEO",
                        Phone = 1235567890,
                        CompanyId = 4,
                        Email = "xyz@abc.com",
                        Comments = " Comments Section Test 4"

                    }
                );
            });

            OnModelCreatingPartial(modelBuilder);
            //new DbInitializer(modelBuilder).Seed();
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
