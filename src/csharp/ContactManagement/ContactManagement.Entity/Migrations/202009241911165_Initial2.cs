namespace ContactManagement.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Contacts", "FirstName", c => c.String(nullable: false, maxLength: 100, unicode: false));
            AlterColumn("dbo.Contacts", "LastName", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.Contacts", "Email", c => c.String(nullable: false, maxLength: 255, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Contacts", "Email", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Contacts", "LastName", c => c.String(maxLength: 100));
            AlterColumn("dbo.Contacts", "FirstName", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
