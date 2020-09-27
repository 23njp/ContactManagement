namespace ContactManagement.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class databasechanges : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Contacts", "PhoneNumber", c => c.String(maxLength: 12));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Contacts", "PhoneNumber", c => c.String());
        }
    }
}
