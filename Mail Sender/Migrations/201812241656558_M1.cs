namespace Mail_Sender.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Mails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        IsHTML = c.Boolean(nullable: false),
                        Topic = c.String(),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Receivers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        ReceiverName = c.String(),
                        IsMailing = c.Boolean(nullable: false),
                        Key = c.String(),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SendedReceivers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReceiverId = c.Int(nullable: false),
                        SendedId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Receivers", t => t.ReceiverId, cascadeDelete: true)
                .ForeignKey("dbo.Sendeds", t => t.SendedId, cascadeDelete: true)
                .Index(t => t.ReceiverId)
                .Index(t => t.SendedId);
            
            CreateTable(
                "dbo.Sendeds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Created = c.DateTime(nullable: false),
                        Mail_Id = c.Int(),
                        Sender_Id = c.Int(),
                        SMTP_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Mails", t => t.Mail_Id)
                .ForeignKey("dbo.Senders", t => t.Sender_Id)
                .ForeignKey("dbo.SMTPs", t => t.SMTP_Id)
                .Index(t => t.Mail_Id)
                .Index(t => t.Sender_Id)
                .Index(t => t.SMTP_Id);
            
            CreateTable(
                "dbo.Senders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Password = c.String(),
                        Key = c.String(),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SMTPs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SMTPName = c.String(),
                        Port = c.String(),
                        Key = c.String(),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sendeds", "SMTP_Id", "dbo.SMTPs");
            DropForeignKey("dbo.Sendeds", "Sender_Id", "dbo.Senders");
            DropForeignKey("dbo.SendedReceivers", "SendedId", "dbo.Sendeds");
            DropForeignKey("dbo.Sendeds", "Mail_Id", "dbo.Mails");
            DropForeignKey("dbo.SendedReceivers", "ReceiverId", "dbo.Receivers");
            DropIndex("dbo.Sendeds", new[] { "SMTP_Id" });
            DropIndex("dbo.Sendeds", new[] { "Sender_Id" });
            DropIndex("dbo.Sendeds", new[] { "Mail_Id" });
            DropIndex("dbo.SendedReceivers", new[] { "SendedId" });
            DropIndex("dbo.SendedReceivers", new[] { "ReceiverId" });
            DropTable("dbo.SMTPs");
            DropTable("dbo.Senders");
            DropTable("dbo.Sendeds");
            DropTable("dbo.SendedReceivers");
            DropTable("dbo.Receivers");
            DropTable("dbo.Mails");
        }
    }
}
