namespace SeminarUpisi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _ModelValidation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Predbiljezba", "Telefon", c => c.String(nullable: false, maxLength: 30));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Predbiljezba", "Telefon", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
