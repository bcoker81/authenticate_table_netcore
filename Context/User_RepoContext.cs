using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using testmysql.Models;

namespace testmysql.Context
{
    public partial class User_RepoContext : DbContext
    {
        public User_RepoContext()
        {
        }

        public User_RepoContext(DbContextOptions<User_RepoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<CoreUser> CoreUser { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account", "User_Repo");

                entity.HasIndex(e => e.FkCoreUserId)
                    .HasName("FK_Core_User_idx");

                entity.HasIndex(e => e.Id)
                    .HasName("Id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.EmailAddress)
                    .HasColumnName("Email_Address")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.FkCoreUserId)
                    .HasColumnName("FK_Core_User_Id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.HashedPassword)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.FkCoreUser)
                    .WithMany(p => p.Account)
                    .HasForeignKey(d => d.FkCoreUserId)
                    .HasConstraintName("FK_Core_User");

                entity.Property(d => d.Logins)
                    .HasMaxLength(11)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CoreUser>(entity =>
            {
                entity.ToTable("Core_User", "User_Repo");

                entity.HasIndex(e => e.Id)
                    .HasName("Id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });
        }
    }
}
