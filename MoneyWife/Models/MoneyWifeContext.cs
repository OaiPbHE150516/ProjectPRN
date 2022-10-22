using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace MoneyWife.Models
{
    public partial class MoneyWifeContext : DbContext
    {
        public MoneyWifeContext()
        {
        }

        public MoneyWifeContext(DbContextOptions<MoneyWifeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ExpenseType> ExpenseTypes { get; set; } = null!;
        public virtual DbSet<IncomeType> IncomeTypes { get; set; } = null!;
        public virtual DbSet<Money> Money { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExpenseType>(entity =>
            {
                entity.ToTable("ExpenseType");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<IncomeType>(entity =>
            {
                entity.ToTable("IncomeType");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Money>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.UserId });

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Bank)
                    .HasColumnType("money")
                    .HasColumnName("bank");

                entity.Property(e => e.Cash)
                    .HasColumnType("money")
                    .HasColumnName("cash");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Money)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Money_Users");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Fullname)
                    .HasMaxLength(100)
                    .HasColumnName("fullname");

                entity.Property(e => e.Gender)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("gender");

                entity.Property(e => e.Location)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("location");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("phone");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("username");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
