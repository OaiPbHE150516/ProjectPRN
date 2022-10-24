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

        public virtual DbSet<Money> Money { get; set; } = null!;
        public virtual DbSet<Transaction> Transactions { get; set; } = null!;
        public virtual DbSet<Type> Types { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            IConfigurationRoot configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("MoneyWifeContext"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Money>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.UserId });

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Bank)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("bank");

                entity.Property(e => e.Cash)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("cash");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Money)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Money_Users");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.UserId });

                entity.ToTable("Transaction");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.CashOrBank)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("cashOrBank");

                entity.Property(e => e.DateUse)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("dateUse");

                entity.Property(e => e.MoneyContent).HasColumnName("moneyContent");

                entity.Property(e => e.MoneyNum)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("moneyNum");

                entity.Property(e => e.MoneyType).HasColumnName("moneyType");

                entity.HasOne(d => d.MoneyTypeNavigation)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.MoneyType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transaction_Type");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transaction_Users");
            });

            modelBuilder.Entity<Type>(entity =>
            {
                entity.ToTable("Type");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Category)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("category");

                entity.Property(e => e.Description)
                    .HasMaxLength(250)
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
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
