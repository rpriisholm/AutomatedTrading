namespace ModelEntities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IndicatorPair : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Candles",
                c => new
                    {
                        SecurityID = c.String(nullable: false, maxLength: 128),
                        TimeFrame = c.Time(nullable: false, precision: 7),
                        CloseTime = c.DateTime(nullable: false),
                        OpenTime = c.DateTime(nullable: false),
                        ClosePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OpenPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LowPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        HighPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalVolume = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AvgPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Storage_ID = c.Int(),
                    })
                .PrimaryKey(t => t.SecurityID)
                .ForeignKey("dbo.Storages", t => t.Storage_ID)
                .Index(t => t.Storage_ID);
            
            CreateTable(
                "dbo.IndicatorPairs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LastResult = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Orders = c.Int(nullable: false),
                        PositiveOrderPct = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SecurityCode = c.String(),
                        SecurityName = c.String(),
                        Leverage = c.Int(nullable: false),
                        StartPieceValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CurrentPieceValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StartValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Direction = c.Int(nullable: false),
                        Portfolio_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Portfolios", t => t.Portfolio_ID)
                .Index(t => t.Portfolio_ID);
            
            CreateTable(
                "dbo.Portfolios",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OrderLimitType = c.Int(nullable: false),
                        OrderLimit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LeverageLimit = c.Int(nullable: false),
                        MaxInvestedPct = c.Int(nullable: false),
                        InitialValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Storages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StoragePath = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.StrategyGenerics",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IsSellEnabled = c.Boolean(nullable: false),
                        IsBuyEnabled = c.Boolean(nullable: false),
                        OrderCount = c.Int(nullable: false),
                        PositiveOrderCount = c.Int(nullable: false),
                        IsDisabled = c.Boolean(nullable: false),
                        LoseLimitConstant = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Candles", "Storage_ID", "dbo.Storages");
            DropForeignKey("dbo.Orders", "Portfolio_ID", "dbo.Portfolios");
            DropIndex("dbo.Orders", new[] { "Portfolio_ID" });
            DropIndex("dbo.Candles", new[] { "Storage_ID" });
            DropTable("dbo.StrategyGenerics");
            DropTable("dbo.Storages");
            DropTable("dbo.Portfolios");
            DropTable("dbo.Orders");
            DropTable("dbo.IndicatorPairs");
            DropTable("dbo.Candles");
        }
    }
}
