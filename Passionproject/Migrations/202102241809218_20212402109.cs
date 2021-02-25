namespace Passionproject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20212402109 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Classes", "Comp_CompID", "dbo.Comps");
            DropIndex("dbo.Classes", new[] { "Comp_CompID" });
            RenameColumn(table: "dbo.Classes", name: "Comp_CompID", newName: "CompID");
            AlterColumn("dbo.Classes", "CompID", c => c.Int(nullable: false));
            CreateIndex("dbo.Classes", "CompID");
            AddForeignKey("dbo.Classes", "CompID", "dbo.Comps", "CompID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Classes", "CompID", "dbo.Comps");
            DropIndex("dbo.Classes", new[] { "CompID" });
            AlterColumn("dbo.Classes", "CompID", c => c.Int());
            RenameColumn(table: "dbo.Classes", name: "CompID", newName: "Comp_CompID");
            CreateIndex("dbo.Classes", "Comp_CompID");
            AddForeignKey("dbo.Classes", "Comp_CompID", "dbo.Comps", "CompID");
        }
    }
}
