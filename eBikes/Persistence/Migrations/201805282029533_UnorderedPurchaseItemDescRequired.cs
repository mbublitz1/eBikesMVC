using System.Data.Entity.Migrations;

namespace eBikes.Persistence.Migrations
{
    public partial class UnorderedPurchaseItemDescRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UnorderedPurchaseItemCart", "Description", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UnorderedPurchaseItemCart", "Description", c => c.String(maxLength: 100));
        }
    }
}
