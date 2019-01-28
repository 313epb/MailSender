namespace Mail_Sender.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M62 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Receivers", "IsMailing");
        }

        public override void Down()
        {
            AddColumn("dbo.Receivers", "IsMailing", c => c.String());
        }
    }
}
