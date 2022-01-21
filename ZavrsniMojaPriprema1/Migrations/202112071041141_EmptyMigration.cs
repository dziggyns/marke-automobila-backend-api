namespace ZavrsniMojaPriprema1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmptyMigration : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Markas", "Drzava", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Markas", new[] { "Drzava" });
        }
    }
}
