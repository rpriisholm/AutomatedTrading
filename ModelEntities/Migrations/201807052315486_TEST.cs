namespace ModelEntities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TEST : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Candles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SecurityID = c.String(nullable: false),
                        TimeFrame = c.Time(nullable: false, precision: 7),
                        CloseTime = c.DateTime(nullable: false),
                        OpenTime = c.DateTime(nullable: false),
                        ClosePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OpenPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LowPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        HighPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalVolume = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AvgPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LengthIndicator_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LengthIndicators", t => t.LengthIndicator_Id)
                .Index(t => t.LengthIndicator_Id);
            
            CreateTable(
                "dbo.AConnections",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RemainingValue = c.Decimal(precision: 18, scale: 2),
                        Portfolio_ID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
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
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Leverage = c.Int(nullable: false),
                        StartPieceValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CurrentPieceValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StartValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Direction = c.Int(nullable: false),
                        SecurityId_SecurityID = c.String(nullable: false, maxLength: 128),
                        Portfolio_ID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SecurityInfoes", t => t.SecurityId_SecurityID, cascadeDelete: true)
                .ForeignKey("dbo.Portfolios", t => t.Portfolio_ID)
                .Index(t => t.SecurityId_SecurityID)
                .Index(t => t.Portfolio_ID);
            
            CreateTable(
                "dbo.SecurityInfoes",
                c => new
                    {
                        SecurityID = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.SecurityID);
            
            CreateTable(
                "dbo.IndicatorPairs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LastResult = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Orders = c.Int(nullable: false),
                        PositiveOrderPct = c.Int(nullable: false),
                        LongIndicator_Id = c.Int(),
                        ShortIndicator_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LengthIndicators", t => t.LongIndicator_Id)
                .ForeignKey("dbo.LengthIndicators", t => t.ShortIndicator_Id)
                .Index(t => t.LongIndicator_Id)
                .Index(t => t.ShortIndicator_Id);
            
            CreateTable(
                "dbo.LengthIndicators",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IndicatorAdapted = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OptimizerOptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RecursiveTests = c.Int(nullable: false),
                        NrOfTestValues = c.Int(nullable: false),
                        IsSellEnabled = c.Boolean(nullable: false),
                        IsBuyEnabled = c.Boolean(nullable: false),
                        MinOrders = c.Int(nullable: false),
                        PositiveOrderPct = c.Int(nullable: false),
                        MinProfitPct = c.Int(nullable: false),
                        LoseLimitConstant = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BestIndicatorPair_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IndicatorPairs", t => t.BestIndicatorPair_Id)
                .Index(t => t.BestIndicatorPair_Id);
            
            CreateTable(
                "dbo.StrategyGenerics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsSellEnabled = c.Boolean(nullable: false),
                        IsBuyEnabled = c.Boolean(nullable: false),
                        OrderCount = c.Int(nullable: false),
                        PositiveOrderCount = c.Int(nullable: false),
                        IsDisabled = c.Boolean(nullable: false),
                        LoseLimitConstant = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Connection_Id = c.Int(),
                        LongIndicator_Id = c.Int(),
                        SecurityID_SecurityID = c.String(nullable: false, maxLength: 128),
                        ShortIndicator_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AConnections", t => t.Connection_Id)
                .ForeignKey("dbo.LengthIndicators", t => t.LongIndicator_Id)
                .ForeignKey("dbo.SecurityInfoes", t => t.SecurityID_SecurityID, cascadeDelete: true)
                .ForeignKey("dbo.LengthIndicators", t => t.ShortIndicator_Id)
                .Index(t => t.Connection_Id)
                .Index(t => t.LongIndicator_Id)
                .Index(t => t.SecurityID_SecurityID)
                .Index(t => t.ShortIndicator_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StrategyGenerics", "ShortIndicator_Id", "dbo.LengthIndicators");
            DropForeignKey("dbo.StrategyGenerics", "SecurityID_SecurityID", "dbo.SecurityInfoes");
            DropForeignKey("dbo.StrategyGenerics", "LongIndicator_Id", "dbo.LengthIndicators");
            DropForeignKey("dbo.StrategyGenerics", "Connection_Id", "dbo.AConnections");
            DropForeignKey("dbo.AConnections", "Portfolio_ID", "dbo.Portfolios");
            DropForeignKey("dbo.OptimizerOptions", "BestIndicatorPair_Id", "dbo.IndicatorPairs");
            DropForeignKey("dbo.IndicatorPairs", "ShortIndicator_Id", "dbo.LengthIndicators");
            DropForeignKey("dbo.IndicatorPairs", "LongIndicator_Id", "dbo.LengthIndicators");
            DropForeignKey("dbo.Candles", "LengthIndicator_Id", "dbo.LengthIndicators");
            DropForeignKey("dbo.Orders", "Portfolio_ID", "dbo.Portfolios");
            DropForeignKey("dbo.Orders", "SecurityId_SecurityID", "dbo.SecurityInfoes");
            DropIndex("dbo.StrategyGenerics", new[] { "ShortIndicator_Id" });
            DropIndex("dbo.StrategyGenerics", new[] { "SecurityID_SecurityID" });
            DropIndex("dbo.StrategyGenerics", new[] { "LongIndicator_Id" });
            DropIndex("dbo.StrategyGenerics", new[] { "Connection_Id" });
            DropIndex("dbo.OptimizerOptions", new[] { "BestIndicatorPair_Id" });
            DropIndex("dbo.IndicatorPairs", new[] { "ShortIndicator_Id" });
            DropIndex("dbo.IndicatorPairs", new[] { "LongIndicator_Id" });
            DropIndex("dbo.Orders", new[] { "Portfolio_ID" });
            DropIndex("dbo.Orders", new[] { "SecurityId_SecurityID" });
            DropIndex("dbo.AConnections", new[] { "Portfolio_ID" });
            DropIndex("dbo.Candles", new[] { "LengthIndicator_Id" });
            DropTable("dbo.StrategyGenerics");
            DropTable("dbo.OptimizerOptions");
            DropTable("dbo.LengthIndicators");
            DropTable("dbo.IndicatorPairs");
            DropTable("dbo.SecurityInfoes");
            DropTable("dbo.Orders");
            DropTable("dbo.Portfolios");
            DropTable("dbo.AConnections");
            DropTable("dbo.Candles");
        }
    }
}
