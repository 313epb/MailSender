using System.Data.Entity;
using MailSender.Domain.Entities;
using MailSender.Domain.Entities.Base.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using DbContext = System.Data.Entity.DbContext;

namespace Mail_Sender.Model
{
    public class MailSenderContext:DbContext
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

        //protected void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(
        //        @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Documents\MailSenderBase.mdf;Integrated Security=True;Connect Timeout=30");
        //}


        //public ApplicationContext()
        //{
        //    Database.EnsureCreated();
        //}

        public virtual System.Data.Entity.DbSet<SendedReceiver> SendedReceivers { get; set; }
        public virtual System.Data.Entity.DbSet<Mail> Mails { get; set; }
        public virtual System.Data.Entity.DbSet<Receiver> Receivers { get; set; }
        public virtual System.Data.Entity.DbSet<Sended> Sendeds { get; set; }
        public virtual System.Data.Entity.DbSet<Sender> Senders { get; set; }
        public virtual System.Data.Entity.DbSet<SMTP> SMTPs { get; set; }

        #region Singleton MailSenderContext

        private static volatile MailSenderContext instance;
        private static readonly object syncObj = new object();

        public static MailSenderContext Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncObj)
                    {
                        if (instance == null)
                        {
                            instance = new MailSenderContext();
                        }
                    }
                }
                return instance;
            }
        }

        #endregion

    }
}
