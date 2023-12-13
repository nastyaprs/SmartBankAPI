using Microsoft.EntityFrameworkCore;
using SmartBank.DAL.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SmartBank.DAL.Data
{
    public class SmartBankDBContext : DbContext
    {
        public SmartBankDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Account> Account { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Card> Card { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Expense> Expense { get; set; }
        public DbSet<Report> Report { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureAccountTable(modelBuilder);
            ConfigureAddressTable(modelBuilder);
            ConfigureCardTable(modelBuilder);
            ConfigureCategoryTable(modelBuilder);
            ConfigureExpenseTable(modelBuilder);
            ConfigureReportTable(modelBuilder);
            ConfigureUserTable(modelBuilder);
        }

        private void ConfigureAccountTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasKey(m => m.Id);

            modelBuilder.Entity<Account>()
            .Property(a => a.AmountOfMoney)
            .HasColumnType("decimal(18,2)");
        }

        private void ConfigureAddressTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>()
                .HasKey(m => m.Id);
        }

        private void ConfigureCardTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Card>()
            .HasKey(c => c.Id);

            modelBuilder.Entity<Card>()
            .HasOne(c => c.Account)
            .WithOne(c => c.Card)
            .HasForeignKey<Card>(c => c.AccountId)
            .IsRequired();

            modelBuilder.Entity<Card>()
            .HasMany(c => c.Expense)
            .WithOne(e => e.Card)
            .HasForeignKey(e => e.CardId)
            .IsRequired();

            modelBuilder.Entity<Card>()
             .HasOne(c => c.Account)
             .WithOne(a => a.Card)
             .HasForeignKey<Card>(c => c.AccountId)
             .IsRequired()
             .OnDelete(DeleteBehavior.NoAction);
        }

        private void ConfigureCategoryTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasKey(m => m.Id);

            modelBuilder.Entity<Category>()
           .HasMany(c => c.Expense)
           .WithOne(e => e.Category)
           .HasForeignKey(e => e.CategoryId)
           .IsRequired();
        }

        private void ConfigureExpenseTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Expense>()
                .HasKey(m => m.Id);
        }

        private void ConfigureReportTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Report>()
                .HasKey(m => m.Id);


            modelBuilder.Entity<Report>()
                .Property(r => r.SpendedMoneyAmount)
                .HasColumnType("decimal(18,2)");
        }

        private void ConfigureUserTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(m => m.Id);

            modelBuilder.Entity<User>()
            .HasOne(u => u.Address)
            .WithOne(a => a.User)
            .HasForeignKey<User>(u => u.AddressId)
            .IsRequired();

            modelBuilder.Entity<User>()
            .HasMany(u => u.Account)
            .WithOne(a => a.User)
            .HasForeignKey(a => a.UserId)
            .IsRequired();

            modelBuilder.Entity<User>()
           .HasMany(u => u.Report)
           .WithOne(r => r.User)
           .HasForeignKey(r => r.UserId)
           .IsRequired();

            modelBuilder.Entity<User>()
           .HasMany(u => u.Category)
           .WithOne(c => c.User)
           .HasForeignKey(c => c.UserId);
        }
    }
}
