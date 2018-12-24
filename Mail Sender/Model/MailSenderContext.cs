using MailSender.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using DbContext = System.Data.Entity.DbContext;

namespace Mail_Sender.Model
{
    class MailSenderContext:DbContext
    {
        protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SendedReceiver>()
                .HasKey(t => new { t.ReceiverId, t.SendedId });

            modelBuilder.Entity<SendedReceiver>()
                .HasOne(sc => sc.Receiver)
                .WithMany(s => s.SendedReceivers)
                .HasForeignKey(sc => sc.ReceiverId);

            modelBuilder.Entity<SendedReceiver>()
                .HasOne(sc => sc.Sended)
                .WithMany(c => c.SendedReceivers)
                .HasForeignKey(sc => sc.SendedId);
        }

        //protected  void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(
        //        @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Documents\MailSenderBase.mdf;Integrated Security=True;Connect Timeout=30");
        //}


        //public ApplicationContext()
        //{
        //    Database.EnsureCreated();
        //}

        public System.Data.Entity.DbSet<SendedReceiver> SendedReceivers { get; set; }
        public System.Data.Entity.DbSet<Mail> Mails { get; set; }
        public System.Data.Entity.DbSet<Receiver> Receivers { get; set; }
        public System.Data.Entity.DbSet<Sended> Sendeds { get; set; }
        public System.Data.Entity.DbSet<Sender> Senders { get; set; }
        public System.Data.Entity.DbSet<SMTP> SMTPs { get; set; }

    }
}
