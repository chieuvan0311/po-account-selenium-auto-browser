using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace PAYONEER.DataConnection
{
    public partial class PayoneerDbContext : DbContext
    {
        public PayoneerDbContext()
            : base("name=PayoneerDbContext")
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Del_Account_Save> Del_Account_Save { get; set; }
        public virtual DbSet<Question> Questions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.AccPassword)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.EmailPassword)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.RecoveryEmail)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.Forward_Email)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.Email_2FA)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.Proxy)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.ProfileID)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.Old_AccPassword)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.Old_EmailPassword)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.Old_RecoveryEmail)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.EmailType)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.Random_AccPassword)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.Random_EmailPassword)
                .IsUnicode(false);

            modelBuilder.Entity<Admin>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Admin>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Del_Account_Save>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Del_Account_Save>()
                .Property(e => e.AccPassword)
                .IsUnicode(false);

            modelBuilder.Entity<Del_Account_Save>()
                .Property(e => e.EmailPassword)
                .IsUnicode(false);

            modelBuilder.Entity<Del_Account_Save>()
                .Property(e => e.RecoveryEmail)
                .IsUnicode(false);

            modelBuilder.Entity<Del_Account_Save>()
                .Property(e => e.Forward_Email)
                .IsUnicode(false);

            modelBuilder.Entity<Del_Account_Save>()
                .Property(e => e.Email_2FA)
                .IsUnicode(false);

            modelBuilder.Entity<Del_Account_Save>()
                .Property(e => e.Proxy)
                .IsUnicode(false);

            modelBuilder.Entity<Del_Account_Save>()
                .Property(e => e.ProfileID)
                .IsUnicode(false);

            modelBuilder.Entity<Del_Account_Save>()
                .Property(e => e.Old_AccPassword)
                .IsUnicode(false);

            modelBuilder.Entity<Del_Account_Save>()
                .Property(e => e.Old_EmailPassword)
                .IsUnicode(false);

            modelBuilder.Entity<Del_Account_Save>()
                .Property(e => e.Old_RecoveryEmail)
                .IsUnicode(false);

            modelBuilder.Entity<Del_Account_Save>()
                .Property(e => e.EmailType)
                .IsUnicode(false);

            modelBuilder.Entity<Del_Account_Save>()
                .Property(e => e.Random_AccPassword)
                .IsUnicode(false);

            modelBuilder.Entity<Del_Account_Save>()
                .Property(e => e.Random_EmailPassword)
                .IsUnicode(false);
        }
    }
}
