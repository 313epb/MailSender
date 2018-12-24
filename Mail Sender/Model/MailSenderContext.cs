using System.Data.Entity;
using MailSender.Domain.Entities;

namespace Mail_Sender.Model
{
    class MailSenderContext:DbContext
    {


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<SendedReceiver>()
        //        .HasKey(t => new {t.ReceiverId, t.SendedId});

        //    modelBuilder.Entity<SendedReceiver>()
        //        .HasOne(sc => sc.Receiver)
        //        .WithMany(s => s.SendedReceivers)
        //        .HasForeignKey(sc => sc.ReceiverId);

        //    modelBuilder.Entity<SendedReceiver>()
        //        .HasOne(sc => sc.Sended)
        //        .WithMany(c => c.SendedReceivers)
        //        .HasForeignKey(sc => sc.SendedId);
        //}

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(
        //        @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Documents\MailSenderBase.mdf;Integrated Security=True;Connect Timeout=30");
        //}


        //public ApplicationContext()
        //{
        //    Database.EnsureCreated();
        //}


        public DbSet<Mail> Mails { get; set; }
        public DbSet<Receiver> Receivers { get; set; }
        public DbSet<Sended> Sendeds { get; set; }
        public DbSet<Sender> Senders { get; set; }
        public DbSet<SMTP> SMTPs { get; set; }

    }
}
