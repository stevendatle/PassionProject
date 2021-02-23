namespace Passionproject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Classes",
                c => new
                    {
                        ClassID = c.Int(nullable: false, identity: true),
                        ClassName = c.String(),
                        ClassSpec = c.String(),
                        ClassPic = c.Boolean(nullable: true),
                        PicExtension = c.String(),
                        CompID = c.Int(nullable: true),
                    })
                .PrimaryKey(t => t.ClassID)
                .ForeignKey("dbo.Comps", t => t.CompID, cascadeDelete: true)
                .Index(t => t.CompID);
            
            CreateTable(
                "dbo.Comps",
                c => new
                    {
                        CompID = c.Int(nullable: false, identity: true),
                        CompName = c.String(),
                        CompClass1 = c.String(),
                        CompClass2 = c.String(),
                        CompClass3 = c.String(),
                    })
                .PrimaryKey(t => t.CompID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Classes", "CompID", "dbo.Comps");
            DropIndex("dbo.Classes", new[] { "CompID" });
            DropTable("dbo.Comps");
            DropTable("dbo.Classes");
        }
    }
}
