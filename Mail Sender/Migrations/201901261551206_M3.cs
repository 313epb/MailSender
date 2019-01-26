namespace Mail_Sender.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Receivers", "Email");
            DropColumn("dbo.Receivers", "ReceiverName");
            DropColumn("dbo.Senders", "Email");
            DropColumn("dbo.Senders", "Password");
            DropColumn("dbo.SMTPs", "SMTPName");
            DropColumn("dbo.SMTPs", "Port");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SMTPs", "Port", c => c.String());
            AddColumn("dbo.SMTPs", "SMTPName", c => c.String());
            AddColumn("dbo.Senders", "Password", c => c.String());
            AddColumn("dbo.Senders", "Email", c => c.String());
            AddColumn("dbo.Receivers", "ReceiverName", c => c.String());
            AddColumn("dbo.Receivers", "Email", c => c.String());
        }
    }
}
