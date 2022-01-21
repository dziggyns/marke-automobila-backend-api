namespace ZavrsniMojaPriprema1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModelFix : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Markas", new[] { "Drzava" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Markas", "Drzava", unique: true);
        }
    }
}
