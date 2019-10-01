using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealLib
{
    public static class SqlQueries
    {
        public static string BeforeInserts = @"
ALTER DATABASE [StockHistDB] SET QUERY_STORE = ON;  
GO
USE [StockHistDB] 
GO
/* Delete Old Data */
GO 
DROP TABLE IF EXISTS [dbo].[CombinationResult] 
GO 
DROP TABLE IF EXISTS [dbo].[Security] 
GO 
/* Create Empty Tables ******/

/* Object: Table [dbo].[Security] Script Date: 6/10/2019 11:23:46 PM ******/
SET ANSI_NULLS ON 

GO 
SET QUOTED_IDENTIFIER ON 

GO 
CREATE TABLE [dbo].[Security](
	[SecurityId] [nvarchar](20) NOT NULL, 
	CONSTRAINT [PK_Security] PRIMARY KEY CLUSTERED ([SecurityId] ASC) WITH (
		PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, 
		IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, 
		ALLOW_PAGE_LOCKS = ON
	) ON [PRIMARY]
) ON [PRIMARY] 
GO 
/* Object: Table [dbo].[CombinationResult] Script Date: 6/10/2019 11:23:46 PM ******/
SET ANSI_NULLS ON 

GO 
SET QUOTED_IDENTIFIER ON 

GO 
CREATE TABLE [dbo].[CombinationResult](
	[SecurityId] [nvarchar](20) NOT NULL, 
	[ShortIndicator] [nvarchar](30) NOT NULL, 
	[LongIndicator] [nvarchar](30) NOT NULL, 
	[LastResult] [decimal](11, 2) NOT NULL, 
	[Orders] [int] NOT NULL, 
	[PositiveOrderPct] [decimal](15, 4) NOT NULL, 
	[ClosePrice] [decimal](20, 8) NOT NULL, 
	[Nr] [int] NOT NULL, 
	[LoseLimitMin] [decimal](11, 2) NOT NULL
) ON [PRIMARY] 
/* BEGIN */
GO
DROP TABLE IF EXISTS [AvgIndicatorResults] 

GO 
CREATE TABLE [dbo].[AvgIndicatorResults](
	[ShortIndicator] [nvarchar](30) NOT NULL, 
	[LongIndicator] [nvarchar](30) NOT NULL, 
	[AvgOrders] [int] NULL, 
	[SumOrders] [int] NULL, 
	[LoseLimitMin] [decimal](11, 2) NULL, 
	[AvgResultPct_NoLimit] [decimal](38, 6) NULL, 
	[AvgResultPct] [decimal](38, 6) NULL
) ON [PRIMARY] 

GO 
DROP TABLE IF EXISTS [SettingsResults] 

GO 
/*
CREATE TABLE [dbo].[SettingsResults](
[AvgIndicatorMin] [int] NOT NULL,
[MinResAvg] [decimal](18, 2) NOT NULL,
[ResAvg] [decimal](18, 2) NOT NULL,
[CountNr] [int] NOT NULL,
[MinResult] [int] NOT NULL,
[MinOrders] [int] NOT NULL,
[MaxOrders] [int] NOT NULL,
[LoseLimitMin] [decimal](18, 2) NOT NULL,
[Nr3] [int] NOT NULL
) ON [PRIMARY]
*/
CREATE TABLE [dbo].[SettingsResults](
	[AvgIndicatorMin] [int] NOT NULL, 
	[MinResAvg] [decimal](18, 2) NOT NULL, 
	[ResAvg] [decimal](18, 2) NOT NULL, 
	[CountNr] [int] NOT NULL, 
	[MinResult] [int] NOT NULL, 
	[MinOrders] [int] NOT NULL, 
	[MaxOrders] [int] NOT NULL, 
	[LoseLimitMin] [decimal](18, 2) NOT NULL, 
	[Nr3] [int] NOT NULL
) ON [PRIMARY] 

GO 

GO 
DROP TABLE IF EXISTS [StockHistDB].[dbo].[AvgIndicatorResults] 

";

        public static string AfterInserts = @"
SELECT 
	[ShortIndicator], 
	[LongIndicator], 
	COUNT([ShortIndicator]) AS Nr, 
	AVG([ORDERS]) AS AvgOrders, 
	SUM([ORDERS]) AS SumOrders, 
	AVG([LoseLimitMin]) AS AvgLoseLimitMin, 
	MIN([LoseLimitMin]) AS LoseLimitMin, 
	AVG([PositiveOrderPct]) AS AvgPositiveOrderPct, 
	AVG([LastResult]) AS AvgResultPct INTO [StockHistDB].[dbo].[AvgIndicatorResults] 
FROM 
	[StockHistDB].[dbo].[CombinationResult] cb 
GROUP BY 
	[ShortIndicator], 
	[LongIndicator] 
GO 
ALTER TABLE 
	[StockHistDB].[dbo].[AvgIndicatorResults] REBUILD PARTITION = ALL WITH (DATA_COMPRESSION = PAGE) 
GO 
DROP 
	TABLE IF EXISTS [StockHistDB].[dbo].[AvgIndicatorResults3_NotForUse] 
GO 
DROP 
	INDEX IF EXISTS OptimizedIndex1 ON [dbo].[CombinationResult] 
GO 
CREATE NONCLUSTERED INDEX OptimizedIndex1 ON [dbo].[CombinationResult] ([LastResult]) INCLUDE (
		[ShortIndicator], [LongIndicator], [Orders], [Nr], [LoseLimitMin]
	) 
GO
DECLARE @i int = 0 
DECLARE @loseLimitIncr decimal(11, 2) = -0.01;
DECLARE @loseLimit decimal(11, 2) = -0.00;
DECLARE @minResultPct decimal(38, 6) = @loseLimit * 100 - 1;
CREATE TABLE [dbo].[AvgIndicatorResults3_NotForUse](
	[Nr] [int] NOT NULL, 
	[ShortIndicator] [nvarchar](30) NOT NULL, 
	[LongIndicator] [nvarchar](30) NOT NULL, 
	[NrTotal] [int] NULL, 
	[AvgOrders] [int] NULL, 
	[SumOrders] [int] NULL, 
	[AvgLoseLimitMin] [decimal](38, 6) NULL, 
	[LoseLimitMin_] [decimal](11, 2) NULL, 
	[AvgResultPct] [decimal](38, 6) NULL, 
	[LoseLimit] [decimal](38, 6) NULL
) ON [PRIMARY] 

WHILE @i <= 20 BEGIN 
SET 
	@loseLimit = 0.00 + @i * @loseLimitIncr;
SET 
	@minResultPct = @loseLimit * 100 - 1;
/* IGNORE BELLOW X ORDERS */

/* MAKE FUTURE ESTIMATES */

/* ABOVE, BELOW OR BETWEEN POSITIVE PCT */

/* Simulate Choice On Each Security */

/* TIME: 30 MIN */
INSERT INTO [StockHistDB].[dbo].[AvgIndicatorResults3_NotForUse] 
SELECT 
	[Nr], 
	[ShortIndicator], 
	[LongIndicator], 
	COUNT([ShortIndicator]) AS NrTotal, 
	COALESCE(SUM(CASE WHEN [LoseLimitMin] >= @loseLimit THEN [ORDERS] ELSE 0 END)/ NULLIF(SUM(CASE WHEN [LoseLimitMin] >= @loseLimit THEN 1 ELSE 0 END), 0), 0) AS AvgOrders, 
	SUM(CASE WHEN [LoseLimitMin] >= @loseLimit THEN [ORDERS] ELSE 0 END) AS SumOrders, 
	AVG(CASE WHEN [LoseLimitMin] >= @loseLimit THEN [LoseLimitMin] ELSE @loseLimit END) AS AvgLoseLimitMin, 
	MIN([LoseLimitMin]) AS LoseLimitMin,
	/*,AVG([PositiveOrderPct]) AS AvgPositiveOrderPct */
	AVG(CASE WHEN [LoseLimitMin] >= @loseLimit THEN [LastResult] ELSE @minResultPct END) AS AvgResultPct, 
	(@loseLimit * 100) AS LoseLimit 
FROM 
	[StockHistDB].[dbo].[CombinationResult] cb 
WHERE 
	cb.LastResult >= 2.0 
	AND cb.LastResult < 600 
GROUP BY 
	[ShortIndicator], 
	[LongIndicator], 
	[Nr] 
SET 
	@i = @i + 1 END 
GO 
ALTER TABLE 
	[StockHistDB].[dbo].[AvgIndicatorResults3_NotForUse] REBUILD PARTITION = ALL WITH (DATA_COMPRESSION = PAGE) 
	/* LIST Best Indicators */
GO 
DROP 
	TABLE IF EXISTS #TEMP 
GO
DROP 
	TABLE IF EXISTS #TEMP2 
GO
DROP 
	TABLE IF EXISTS [AvgIndicatorResults_Extended] 
GO 
DROP 
	TABLE IF EXISTS [BestIndicatorCombination_TEMP] 
GO 
SELECT 
	[ShortIndicator], 
	[LongIndicator], 
	SUM([NrTotal]) AS [NrTotal], 
	COALESCE(SUM([AvgOrders])/ NULLIF(COUNT([AvgOrders]), 0),0) AS [AvgOrders], 
	SUM([SumOrders]) AS [SumOrders], 
	[AvgLoseLimitMin], 
	MIN([LoseLimitMin_]) AS [LoseLimitMin_], 
	COALESCE(SUM([AvgResultPct])/ NULLIF(COUNT([AvgResultPct]), 0), 0) AS [AvgResultPct], 
	[LoseLimit] INTO #TEMP
FROM 
	[StockHistDB].[dbo].[AvgIndicatorResults3_NotForUse] 
GROUP BY 
	[ShortIndicator], 
	[LongIndicator], 
	[NrTotal], 
	[AvgOrders], 
	[SumOrders], 
	[AvgLoseLimitMin], 
	[LoseLimitMin_], 
	[AvgResultPct], 
	[LoseLimit] 
GO 
DROP INDEX IF EXISTS [SmallPerfImpr] ON [LoseLimit] 

GO 
CREATE NONCLUSTERED INDEX [SmallPerfImpr] ON [dbo].[ #TEMP] ([ShortIndicator],
	[LongIndicator], 
	[AvgResultPct]
) INCLUDE ([LoseLimit]) 

GO 
SELECT 
	t.[ShortIndicator], 
	t.[LongIndicator], 
	CAST(
		MAX(t.[LoseLimit])/ 100 AS DECIMAL(10, 2)
	) AS [LoseLimit], 
	t.[AvgResultPct] INTO #TEMP2
FROM 
	(
		SELECT 
			MAX([AvgResultPct]) AS [AvgResultPct], 
			[ShortIndicator], 
			[LongIndicator] 
		FROM 
			#TEMP
		GROUP BY 
			[ShortIndicator], 
			[LongIndicator]
	) TEST, 
	#TEMP t
WHERE 
	TEST.[ShortIndicator] = t.[ShortIndicator] 
	AND TEST.[LongIndicator] = t.[LongIndicator] 
	AND TEST.AvgResultPct = t.AvgResultPct 
GROUP BY 
	t.[ShortIndicator], 
	t.[LongIndicator], 
	t.[AvgResultPct] 
ORDER BY 
	t.AvgResultPct DESC 
SELECT 
	cb.ShortIndicator, 
	cb.LongIndicator, 
	AVG(cb.Orders) AS AvgOrders, 
	SUM(cb.Orders) AS SumOrders, 
	MIN(cb.LoseLimitMin) AS LoseLimitMin, 
	AVG(cb.LastResult) AS AvgResultPct_NoLimit, 
	(
		SELECT 
			t.AvgResultPct 
		FROM 
			#TEMP2 t
		WHERE 
			t.ShortIndicator = cb.ShortIndicator 
			AND t.LongIndicator = cb.LongIndicator
	) AS AvgResultPct, 
	(
		SELECT 
			t.LoseLimit 
		FROM 
			#TEMP2 t
		WHERE 
			t.ShortIndicator = cb.ShortIndicator 
			AND t.LongIndicator = cb.LongIndicator
	) AS LoseLimit INTO [AvgIndicatorResults_Extended] 
FROM 
	[CombinationResult] cb 
GROUP BY 
	cb.ShortIndicator, 
	cb.LongIndicator 
GO 
DROP TABLE IF EXISTS #TEMP 
GO
DROP TABLE IF EXISTS #TEMP2 
GO
DROP TABLE IF EXISTS CombinationResult1_TEMP 
GO 
DROP TABLE IF EXISTS CombinationResult2_TEMP 
GO 
DROP TABLE IF EXISTS CombinationResult3_TEMP 
GO 
DECLARE @minAvgResult decimal = 10.00;
/* DECLARE @minResult decimal = 10.00; */
DECLARE @mintNr int;
DECLARE @lastNr int;
DECLARE @closePriceMin decimal(18, 2) = 2.0;
DECLARE @loseLimitMin decimal(18, 2) = -0.4;
SELECT 
	@mintNr = min(Nr), 
	@lastNr = max(Nr) 
FROM 
	[StockHistDB].[dbo].[CombinationResult] 
SELECT 
	bcbv.* INTO BestIndicatorCombination_TEMP 
FROM 
	[StockHistDB].[dbo].AvgIndicatorResults bcbv 
WHERE 
	bcbv.AvgResultPct >= @minAvgResult 
	/* First - Valied Combination */
SELECT 
	cb1.SecurityId, 
	cb1.ShortIndicator, 
	cb1.LongIndicator, 
	cb1.LastResult, 
	cb1.Orders, 
	cb1.Nr, 
	cb1.LoseLimitMin INTO CombinationResult1_TEMP 
FROM 
	[StockHistDB].[dbo].[CombinationResult] cb1, 
	BestIndicatorCombination_TEMP bic 
WHERE 
	cb1.ShortIndicator = bic.ShortIndicator 
	AND cb1.LongIndicator = bic.LongIndicator 
	AND cb1.Nr >= @mintNr 
	AND cb1.Nr <= @lastNr 
	AND cb1.ClosePrice >= @closePriceMin 
	/* Second - Combinate Valied Combinations (Two Test) */
SELECT 
	cb1.SecurityId, 
	cb1.ShortIndicator, 
	cb1.LongIndicator, 
	cb1.LastResult AS Result1, 
	cb2.LastResult AS Result2, 
	cb1.Orders AS Orders1, 
	cb2.Orders AS Orders2, 
	cb1.Nr AS Nr1, 
	cb2.Nr AS Nr2, 
	cb1.LoseLimitMin AS LoseLimitMin1, 
	cb2.LoseLimitMin AS LoseLimitMin2 INTO CombinationResult2_TEMP 
FROM 
	CombinationResult1_TEMP cb1, 
	CombinationResult1_TEMP cb2 
WHERE 
	cb2.Nr = (cb1.Nr + 1) 
	AND cb1.SecurityId = cb2.SecurityId 
	AND cb1.ShortIndicator = cb2.ShortIndicator 
	AND cb1.LongIndicator = cb2.LongIndicator 
	AND cb2.Nr <= @lastNr 
	/* Third */
SELECT 
	cb_res.SecurityId, 
	cb_res.ShortIndicator, 
	cb_res.LongIndicator, 
	cb1.Result1, 
	cb1.Result2, 
	(CASE WHEN cb_res.LoseLimitMin <= @loseLimitMin THEN (@loseLimitMin * 100 - 1) ELSE cb_res.LastResult END) AS Result3, 
	cb1.Orders1, 
	cb1.Orders2, 
	cb_res.Orders AS Orders3, 
	cb1.Nr1, 
	cb1.Nr2, 
	cb_res.Nr AS Nr3, 
	cb1.LoseLimitMin1, 
	cb1.LoseLimitMin2, 
	cb_res.LoseLimitMin AS LoseLimit3, 
	(CASE WHEN cb1.LoseLimitMin1 <= cb1.LoseLimitMin2 THEN cb1.LoseLimitMin1 ELSE cb1.LoseLimitMin2 END) AS LoseLimit INTO CombinationResult3_TEMP 
FROM 
	CombinationResult2_TEMP cb1, 
	CombinationResult cb_res 
WHERE 
	cb_res.SecurityId = cb1.SecurityId 
	AND cb_res.ShortIndicator = cb1.ShortIndicator 
	AND cb_res.LongIndicator = cb1.LongIndicator 
	AND cb_res.Nr = (cb1.Nr2 + 1) 
ALTER TABLE 
	CombinationResult3_TEMP 
ADD 
	Id INT IDENTITY(1, 1) 
GO 
DROP INDEX IF EXISTS [PerformanceIndexTemp3] ON [dbo].[combinationresult3_temp] 

GO 
CREATE NONCLUSTERED INDEX [PerformanceIndexTemp3] 
ON [dbo].[CombinationResult3_TEMP] ([ShortIndicator], [LongIndicator]) 
INCLUDE (
	[SecurityId], [Result1], [Result2], 
	[Result3], [Orders1], [Orders2], 
	[Orders3], [Nr1], [Nr2], [Nr3], [LoseLimitMin1], 
	[LoseLimitMin2], [LoseLimit3], [LoseLimit], 
	[Id]
) 
GO 
IF(EXISTS (	SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE 
			TABLE_CATALOG = 'StockHistDB' 
			AND TABLE_SCHEMA = 'dbo' 
			AND TABLE_NAME = 'combinationresult4_temp'
	)
) 
BEGIN 
	DROP TABLE combinationresult4_temp 
END 
GO
/* TODO */
SELECT 
	cb_tmp.*, 
	CASE WHEN cb_tmp.LoseLimit3 >= avg_res.LoseLimit THEN cb_tmp.Result3 ELSE avg_res.LoseLimit * 100 END AS Result3_New INTO combinationresult4_temp 
FROM 
	combinationresult3_temp cb_tmp, 
	[StockHistDB].[dbo].[AvgIndicatorResults_Extended] avg_res 
WHERE 
	cb_tmp.ShortIndicator = avg_res.ShortIndicator 
	AND cb_tmp.LongIndicator = avg_res.LongIndicator 

GO 
ALTER TABLE combinationresult4_temp 
DROP COLUMN Result3;
GO 
CREATE NONCLUSTERED INDEX [combinationresult4_temp_index1] ON [dbo].[combinationresult4_temp] (
	[Orders1], [Orders2], [Result1], [Result2], 
	[LoseLimit]
) INCLUDE (
	[SecurityId], [ShortIndicator], [LongIndicator], 
	[Nr3]
) 
GO 

CREATE NONCLUSTERED INDEX [combinationresult4_temp_index2] ON [dbo].[combinationresult4_temp] ([SecurityId], [Result2], [Nr3]) INCLUDE (
	[ShortIndicator], [LongIndicator], 
	[Result3_New]
) 
/* Compression */
GO 
USE [StockHistDB] 
ALTER TABLE 
	[dbo].[combinationresult4_temp] REBUILD PARTITION = ALL WITH (DATA_COMPRESSION = PAGE) 
GO 
USE [StockHistDB] ALTER INDEX [combinationresult4_temp_index1] ON [dbo].[combinationresult4_temp] REBUILD PARTITION = ALL WITH (
	PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, 
	SORT_IN_TEMPDB = OFF, ONLINE = OFF, 
	ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, 
	DATA_COMPRESSION = PAGE
) 
GO 
USE [StockHistDB] ALTER INDEX [combinationresult4_temp_index2] ON [dbo].[combinationresult4_temp] REBUILD PARTITION = ALL WITH (
	PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, 
	SORT_IN_TEMPDB = OFF, ONLINE = OFF, 
	ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, 
	DATA_COMPRESSION = PAGE
) 
GO 
";

        public static string FindOptimalValuesLatest = @"

/* *********************************************** */
/* ************FIND OPTIMAL VALUES *************** */
/* *********************************************** */
/* **Runs Forever If No Values At MinNr And MaxNr* */
/* *********************************************** */
GO 
TRUNCATE TABLE SettingsResults

GO
SET NOCOUNT ON DECLARE @OutputString varchar(255) = Cast(Getdate() AS varchar(20));
RAISERROR (@OutputString, 0, 1) WITH NOWAIT 
/* Shouldn't Be Less Than 300 ??? */
DECLARE @StartDateTime datetime = GETDATE() 
DECLARE @EndDateTime datetime 
DECLARE @SecondsFromStart int = 0 
DECLARE @MinCount int = 25 
DECLARE @Every1000 int = 0 
DECLARE @Every100 int = 0 
DECLARE @SkippedNr int = 0 
DECLARE @CurrentCount int = -1 
DECLARE @CurrentMinAvg [decimal](18, 2) = 0.0;
DECLARE @CurrentAvgRes [decimal](18, 2) = 0.0;

DECLARE @AvgResultPctMin TABLE (
	avgResultPctMin int
) 

DECLARE @LoseLimitMin TABLE (
	loseLimitMin decimal(11, 2)
) 

DECLARE @OrdersCount TABLE (
	ordersCount int
) 
DECLARE @ResultMin TABLE(
	resultMin int
) 
/* TESTED
DECLARE @AvgResultPctMinStart int = 4 
DECLARE @AvgResultPctMinEnd int = 100 
DECLARE @AvgResultPctMinIncr int = 4 
*/

DECLARE @AvgResultPctMinStart int = 4 
DECLARE @AvgResultPctMinEnd int = 28 
DECLARE @AvgResultPctMinIncr int = 2 
/* TESTED @LoseLimitMinStart=0.00*/
DECLARE @LoseLimitMinStart decimal(11, 2) = -0.20 
DECLARE @LoseLimitMinEnd decimal(11, 2) = -0.30 
DECLARE @LoseLimitMinIncr decimal(11, 2) = 0.01 
/*
TESTED VALUES
DECLARE @OrdersCountStart int = 4 
DECLARE @OrdersCountEnd int = 30 
*/
DECLARE @OrdersCountStart int = 5 
DECLARE @OrdersCountEnd int = 10 
DECLARE @OrdersCountIncr int = 1 
DECLARE @ResultMinStart int = 4 
DECLARE @ResultMinEnd int = 70 
DECLARE @ResultMinIncr int = 2 
DECLARE @MinNr int;
DECLARE @MaxNr int;
DECLARE @CurrentNr int;
SELECT 
	/* @MinNr = Min(cb_tmp.Nr3), */ 
	@MaxNr = MAX(cb_tmp.Nr3) 
FROM 
	combinationresult4_temp cb_tmp 

SET @MinNr = @MaxNr

DECLARE @AvgResultPctMinCurrent int = @AvgResultPctMinStart 

WHILE (@AvgResultPctMinCurrent <= @AvgResultPctMinEnd) 
BEGIN 
	INSERT INTO @AvgResultPctMin VALUES(@AvgResultPctMinCurrent) 
	SET @AvgResultPctMinCurrent = @AvgResultPctMinCurrent + @AvgResultPctMinIncr;
END 

DECLARE @LoseLimitMinCurrent decimal(11, 2) = @LoseLimitMinStart 

WHILE (@LoseLimitMinCurrent >= @LoseLimitMinEnd) 
BEGIN 
	INSERT INTO @LoseLimitMin VALUES(@LoseLimitMinCurrent) 
	SET @LoseLimitMinCurrent = @LoseLimitMinCurrent - @LoseLimitMinIncr;
END 

DECLARE @OrdersCountCurrent int = @OrdersCountStart 

WHILE (@OrdersCountCurrent <= @OrdersCountEnd) 
BEGIN INSERT INTO @OrdersCount 
	VALUES (@OrdersCountCurrent) 
	SET @OrdersCountCurrent = @OrdersCountCurrent + @OrdersCountIncr;
END 

DECLARE @ResultMinCurrent int = @ResultMinStart 

WHILE (@ResultMinCurrent <= @ResultMinEnd) 
BEGIN 
	INSERT INTO @ResultMin VALUES (@ResultMinCurrent) 
	SET @ResultMinCurrent = @ResultMinCurrent + @ResultMinIncr;
END 

PRINT Cast(Getdate() AS DATETIME2 (3)) 

DECLARE @TestValuesAll TABLE (
	avgResultPctMin int, 
	loseLimitMin decimal(11, 2), 
	ordersCount int, 
	resultMin int, 
	PRIMARY KEY (
		resultMin, loseLimitMin, ordersCount, 
		avgResultPctMin
	)
) 


SET @OutputString = 'Add All Test values' + Cast(Getdate() AS varchar(20));

RAISERROR (@OutputString, 0, 1) WITH NOWAIT 

INSERT INTO @TestValuesAll 
SELECT 
	avgResultPctMin, 
	loseLimitMin, 
	ordersCount, 
	resultMin 
FROM 
	@AvgResultPctMin, 
	@LoseLimitMin, 
	@OrdersCount, 
	@ResultMin 

DROP TABLE IF EXISTS #TestValues 
CREATE TABLE #TestValues (
	avgResultPctMin int, 
	loseLimitMin decimal(11, 2), 
	ordersCount int, 
	resultMin int, 
	PRIMARY KEY (resultMin, loseLimitMin, ordersCount,avgResultPctMin)
) 

DROP TABLE IF EXISTS [#InvaliedSettings] 
CREATE TABLE [#InvaliedSettings]
(
	[AvgResultPctMinCurrent] [int] NOT NULL, 
	[LoseLimitMinCurrent] [decimal](18, 2) NOT NULL, 
	[OrdersCountCurrent] [int] NOT NULL, 
	[ResultMinCurrent] [int] NOT NULL, 
	[Nr3] [int] NOT NULL, 
	[MinCount] [int] NOT NULL
) ON [PRIMARY] 

CREATE NONCLUSTERED INDEX [InvaliedSettingsIndex]
ON [#InvaliedSettings] ( [AvgResultPctMinCurrent] ASC, [LoseLimitMinCurrent] ASC, [OrdersCountCurrent] ASC, [ResultMinCurrent] ASC, [Nr3] ASC, [MinCount] ASC ) 

SET @OutputString = 'Remove Existing Test Values ' + Cast(Getdate() AS varchar(20));
RAISERROR (@OutputString, 0, 1) WITH NOWAIT 


/* AvgIndicatorMin  = AvgResultPctMinCurrent */ 
INSERT INTO #TestValues 
SELECT 
	a.avgResultPctMin, 
	a.loseLimitMin, 
	a.ordersCount, 
	a.resultMin 
FROM 
	@TestValuesAll a 
	/*
	INSERT INTO #TestValues
	SELECT a.avgResultPctMin, a.loseLimitMin, a.ordersCount, a.resultMin
	FROM @TestValuesAll a
	EXCEPT 
	SELECT b.AvgIndicatorMin, b.LoseLimitMin, b.MaxOrders, b.MinResult
	FROM SettingsResults b
	*/
	
SET @OutputString = 'Begin Inserts' + Cast(	Getdate() AS varchar(20));
RAISERROR (@OutputString, 0, 1) WITH NOWAIT 
/*
SELECT * 
FROM #TestValues
*/
DECLARE @rows int = (SELECT COUNT(*) FROM #TestValues) 
DECLARE @currentRows int = 0 

SET @OutputString = 'Begin Inserting Values' + Cast(Getdate() AS varchar(20));
RAISERROR (@OutputString, 0, 1) WITH NOWAIT 
DECLARE @TestValuesAllCount int 
DECLARE @TestValuesCount int 
DECLARE @SettingsResultsCount int 
DECLARE @TotalSettingsToTest int 
DECLARE @TotalCount int 

SELECT 	@TestValuesAllCount = COUNT(*) FROM @TestValuesAll 
SELECT 	@TestValuesCount = COUNT(*) FROM #TestValues 
SELECT 	@SettingsResultsCount = COUNT(*) FROM [settingsresults] 

SET @TotalSettingsToTest = @TestValuesAllCount * (@maxNr - @MinNr + 1) 
SET @TotalCount = 0 
SET @OutputString = 'Nr3 Min: ' + Cast(@MinNr AS varchar(20)) + ' Nr3 Max: ' + Cast(@maxNr AS varchar(20)) 
RAISERROR (@OutputString, 0, 1) WITH NOWAIT 
SET @OutputString = 'Want To Insert: ' + Cast(@TotalSettingsToTest AS varchar(20)) + ' Values' 
SET @OutputString = @OutputString + ' - Existing Total: ' + Cast(@SettingsResultsCount AS varchar(20)) + ' Values' 
RAISERROR (@OutputString, 0, 1) WITH NOWAIT 

DECLARE @TestValuesActive 
TABLE (
	avgResultPctMin int, 
	loseLimitMin decimal(11, 2), 
	ordersCount int, 
	resultMin int, 
	IsEnabled bit, 
	nr3 int,
	PRIMARY KEY (
		resultMin, 
		loseLimitMin, 
		ordersCount, 
		avgResultPctMin,
		IsEnabled,
		nr3
	)
)

/* SKIP VALUES */
DELETE 
FROM #TestValues
WHERE ordersCount = 8
/*
DECLARE @TestValuesActiveId TABLE (
	Id int,
	Nr3 int,
	PRIMARY KEY 
	(
		Id, 
		Nr3
	)
	Reffrence?
)
*/

CREATE NONCLUSTERED INDEX [TestValuesIndex] ON #TestValues 
(
	avgResultPctMin ASC,
	resultMin ASC, 
	loseLimitMin ASC,
	ordersCount
)

WHILE @currentRows < @rows 
BEGIN 
	SELECT 
		@AvgResultPctMinCurrent = tv.avgResultPctMin, 
		@LoseLimitMinCurrent = tv.loseLimitMin, 
		@OrdersCountCurrent = tv.ordersCount, 
		@ResultMinCurrent = tv.resultMin 
	FROM 
		#TestValues tv 
	ORDER BY 
		avgResultPctMin ASC,
		resultMin ASC, 
		loseLimitMin ASC, 
		ordersCount
		OFFSET @currentRows ROWS FETCH NEXT 1 ROWS ONLY;
		
	SET @CurrentNr = @MinNr 
	WHILE @CurrentNr <= @maxNr 
	BEGIN 
		IF(NOT EXISTS (
					
					/*
					SELECT tva.IsEnabled
					FROM @TestValues tv, @TestValuesActive tva 
					WHERE @AvgResultPctMinCurrent >= AvgResultPctMinCurrent 
						AND @LoseLimitMinCurrent >= LoseLimitMinCurrent 
						AND @OrdersCountCurrent = OrdersCountCurrent 
						AND @ResultMinCurrent >= ResultMinCurrent 
						AND @MinCount = MinCount 
						AND @CurrentNr = Nr3
					*/
					SELECT 
						Nr3 
					FROM 
						[#InvaliedSettings]
					WHERE 
						@AvgResultPctMinCurrent >= AvgResultPctMinCurrent 
						AND @LoseLimitMinCurrent >= LoseLimitMinCurrent 
						AND @OrdersCountCurrent = OrdersCountCurrent 
						AND @ResultMinCurrent >= ResultMinCurrent 
						AND @MinCount = MinCount 
						AND @CurrentNr = Nr3
				)
			) 
		BEGIN 
			BEGIN TRY 
				SET @CurrentCount = 0;
				SET @CurrentMinAvg = 0.0;
				SET @CurrentAvgRes = 0.0;
				SELECT 
					@CurrentMinAvg = MIN(results.AvgGroup), 
					@CurrentAvgRes = COALESCE(SUM(results.SumRes)/ NULLIF(SUM(results.GroupCount), 0), 0), 
					@CurrentCount = SUM(results.GroupCount) 
				FROM (
						SELECT 
							bestRows.Nr3, 
							AVG(bestRows.Result3_New) AS AvgGroup, 
							SUM(bestRows.Result3_New) AS SumRes, 
							COUNT(bestRows.Result3_New) AS GroupCount 
						FROM (
								SELECT 
									cb_tmp2.Nr3, 
									cb_tmp2.Result3_New, 
									ROW_NUMBER() OVER (
										PARTITION BY cb_tmp2.nr3, 
										cb_tmp2.securityid, 
										cb_tmp2.result2 
										ORDER BY 
											RAND()
									) AS RowNum 
								FROM 
									combinationresult4_temp cb_tmp2, 
									[StockHistDB].[dbo].AvgIndicatorResults_Extended avg_res2 
								WHERE 
									(cb_tmp2.result2) IN (
										SELECT 
											MAX(cb_tmp.result2) AS result2 
										FROM 
											combinationresult4_temp cb_tmp, 
											[StockHistDB].[dbo].AvgIndicatorResults_Extended avg_res 
										WHERE 
											cb_tmp.result1 >= @ResultMinCurrent 
											AND cb_tmp.result2 >= @ResultMinCurrent 
											AND cb_tmp.result1 < 1200 
											AND cb_tmp.result2 < 1200 
											AND cb_tmp.LoseLimit >= @LoseLimitMinCurrent 
											AND cb_tmp.orders1 = @OrdersCountCurrent 
											AND cb_tmp.orders2 = cb_tmp.orders1 
											AND avg_res.AvgResultPct >= @AvgResultPctMinCurrent 
											AND cb_tmp.ShortIndicator = avg_res.ShortIndicator 
											AND cb_tmp.LongIndicator = avg_res.LongIndicator 
											AND cb_tmp.securityid = cb_tmp2.securityid 
											AND cb_tmp.nr3 = cb_tmp2.nr3 
										GROUP BY cb_tmp.nr3, cb_tmp.securityid
									) 
									AND avg_res2.AvgResultPct >= @AvgResultPctMinCurrent 
									AND cb_tmp2.ShortIndicator = avg_res2.ShortIndicator 
									AND cb_tmp2.LongIndicator = avg_res2.LongIndicator 
									AND cb_tmp2.result3_new < 800 
									AND cb_tmp2.nr3 = @currentNr
							) bestRows 
						WHERE 
							bestRows.RowNum = 1 
						GROUP BY bestRows.Nr3
				) results 
					
					IF(@CurrentCount >= @MinCount) 
					BEGIN 
						INSERT INTO [dbo].[settingsresults] (
							[AvgIndicatorMin], 
							[MinResAvg], 
							[resavg], 
							[countnr], 
							[minresult], 
							[minorders], 
							[maxorders], 
							[loselimitmin], 
							[Nr3]
						) 
						VALUES (
							@AvgResultPctMinCurrent, 
							@CurrentMinAvg, 
							@CurrentAvgRes,
							@CurrentCount, 
							@ResultMinCurrent, 
							@OrdersCountCurrent,
							@OrdersCountCurrent, 
							@LoseLimitMinCurrent,
							@CurrentNr
						)
					END 
				ELSE 
				BEGIN 
					THROW 51000, 'InvaliedSettings', 1 
				END 
				END TRY 
			BEGIN CATCH 	
				INSERT INTO [#InvaliedSettings]
				VALUES 
				(
					@AvgResultPctMinCurrent, @LoseLimitMinCurrent, 
					@OrdersCountCurrent, @ResultMinCurrent, 
					@CurrentNr, @MinCount
				) 
			/*
				INSERT INTO @TestValuesActive(avgResultPctMin, loseLimitMin, ordersCount, resultMin, IsEnabled, nr3)
				SELECT avgResultPctMin, loseLimitMin, ordersCount, resultMin, 0, @CurrentNr
				FROM #TestValues tv
				WHERE avgResultPctMin >= @AvgResultPctMinCurrent 
					AND loseLimitMin >= @LoseLimitMinCurrent
					AND ordersCount = @OrdersCountCurrent
					AND resultMin >= @ResultMinCurrent
			*/
			 
			END CATCH 
		END
			/*
			ELSE
			BEGIN
			SET @Every1000 = @Every1000 + 1
			SET @SkippedNr = @SkippedNr + 1
			END
			*/
		SET @TotalCount = @TotalCount + 1 
		SET @Every100 = @Every100 + 1
		SET @CurrentNr = @CurrentNr + 1 
	END 
	
	/* About 5-10 Min */
	IF(@Every100 >= 100000) 
	BEGIN 
		SELECT 
			@SettingsResultsCount = COUNT(*) 
		FROM 
			[settingsresults] 
		SET @Every100 = 0 
		SET @OutputString = 'Want To Insert: ' + Cast(@TotalSettingsToTest AS varchar(20)) + ' Values' 
		SET @OutputString = @OutputString + ' - Remaning: ' + Cast((@TotalSettingsToTest - @TotalCount) AS varchar(20)) + ' Values' 
		SET @OutputString = @OutputString + ' - Existing Total: ' + Cast(@SettingsResultsCount AS varchar(20)) + ' Values' 
		SET @OutputString = @OutputString + ' - Settings Tried: ' + Cast(@TotalCount AS varchar(20)) + ' Values' 
		
		SELECT @SecondsFromStart = DATEDIFF(SECOND, @StartDateTime, GETDATE()) 
		
		SET @EndDateTime = DATEADD(SECOND, ((@TotalSettingsToTest) /(@TotalCount / @SecondsFromStart)), @StartDateTime)
		SET @OutputString = @OutputString + ' - Completes At: ' + convert(varchar, @EndDateTime, 0)
		SET @OutputString = @OutputString + ' - Minutes Remaning: ' + Cast((DATEDIFF(MINUTE, @StartDateTime,  @EndDateTime)) AS varchar(20)) 
		/*SET @OutputString = @OutputString + ' - Skipped Values Total: ' + Cast(@SkippedNr AS varchar(20)) + ' Values' */
		RAISERROR (@OutputString, 0, 1) WITH NOWAIT 
	END 
	SET @currentRows = @currentRows + 1 
END

PRINT Cast(Getdate() AS DATETIME2 (3)) 
";

        public static string SelectBestSettingsLatest = @"
TRUNCATE TABLE SettingsResults

/* *********************************************** */
/* ************FIND OPTIMAL VALUES *************** */
/* *********************************************** */
/* **Runs Forever If No Values At MinNr And MaxNr* */
/* *********************************************** */
GO 

SET NOCOUNT ON DECLARE @OutputString varchar(255) = Cast(Getdate() AS varchar(20));
RAISERROR (@OutputString, 0, 1) WITH NOWAIT 
/* Shouldn't Be Less Than 300 ??? */
DECLARE @StartDateTime datetime = GETDATE() 
DECLARE @EndDateTime datetime 
DECLARE @SecondsFromStart int = 0 
DECLARE @MinCount int = 25 
DECLARE @Every1000 int = 0 
DECLARE @Every100 int = 0 
DECLARE @SkippedNr int = 0 
DECLARE @CurrentCount int = -1 
DECLARE @CurrentMinAvg [decimal](18, 2) = 0.0;
DECLARE @CurrentAvgRes [decimal](18, 2) = 0.0;

DECLARE @AvgResultPctMin TABLE (
	avgResultPctMin int
) 

DECLARE @LoseLimitMin TABLE (
	loseLimitMin decimal(11, 2)
) 

DECLARE @OrdersCount TABLE (
	ordersCount int
) 
DECLARE @ResultMin TABLE(
	resultMin int
) 
/* TESTED
DECLARE @AvgResultPctMinStart int = 4 
DECLARE @AvgResultPctMinEnd int = 100 
DECLARE @AvgResultPctMinIncr int = 4 
*/

DECLARE @AvgResultPctMinStart int = 4 
DECLARE @AvgResultPctMinEnd int = 28 
DECLARE @AvgResultPctMinIncr int = 2 
/* TESTED @LoseLimitMinStart=0.00*/
DECLARE @LoseLimitMinStart decimal(11, 2) = -0.20 
DECLARE @LoseLimitMinEnd decimal(11, 2) = -0.30 
DECLARE @LoseLimitMinIncr decimal(11, 2) = 0.01 
/*
TESTED VALUES
DECLARE @OrdersCountStart int = 4 
DECLARE @OrdersCountEnd int = 30 
*/
DECLARE @OrdersCountStart int = 5 
DECLARE @OrdersCountEnd int = 10 
DECLARE @OrdersCountIncr int = 1 
DECLARE @ResultMinStart int = 4 
DECLARE @ResultMinEnd int = 70 
DECLARE @ResultMinIncr int = 2 
DECLARE @MinNr int;
DECLARE @MaxNr int;
DECLARE @CurrentNr int;
SELECT 
	/* @MinNr = Min(cb_tmp.Nr3), */ 
	@MaxNr = MAX(cb_tmp.Nr3) 
FROM 
	combinationresult4_temp cb_tmp 

SET @MinNr = @MaxNr

DECLARE @AvgResultPctMinCurrent int = @AvgResultPctMinStart 

WHILE (@AvgResultPctMinCurrent <= @AvgResultPctMinEnd) 
BEGIN 
	INSERT INTO @AvgResultPctMin VALUES(@AvgResultPctMinCurrent) 
	SET @AvgResultPctMinCurrent = @AvgResultPctMinCurrent + @AvgResultPctMinIncr;
END 

DECLARE @LoseLimitMinCurrent decimal(11, 2) = @LoseLimitMinStart 

WHILE (@LoseLimitMinCurrent >= @LoseLimitMinEnd) 
BEGIN 
	INSERT INTO @LoseLimitMin VALUES(@LoseLimitMinCurrent) 
	SET @LoseLimitMinCurrent = @LoseLimitMinCurrent - @LoseLimitMinIncr;
END 

DECLARE @OrdersCountCurrent int = @OrdersCountStart 

WHILE (@OrdersCountCurrent <= @OrdersCountEnd) 
BEGIN INSERT INTO @OrdersCount 
	VALUES (@OrdersCountCurrent) 
	SET @OrdersCountCurrent = @OrdersCountCurrent + @OrdersCountIncr;
END 

DECLARE @ResultMinCurrent int = @ResultMinStart 

WHILE (@ResultMinCurrent <= @ResultMinEnd) 
BEGIN 
	INSERT INTO @ResultMin VALUES (@ResultMinCurrent) 
	SET @ResultMinCurrent = @ResultMinCurrent + @ResultMinIncr;
END 

PRINT Cast(Getdate() AS DATETIME2 (3)) 

DECLARE @TestValuesAll TABLE (
	avgResultPctMin int, 
	loseLimitMin decimal(11, 2), 
	ordersCount int, 
	resultMin int, 
	PRIMARY KEY (
		resultMin, loseLimitMin, ordersCount, 
		avgResultPctMin
	)
) 


SET @OutputString = 'Add All Test values' + Cast(Getdate() AS varchar(20));

RAISERROR (@OutputString, 0, 1) WITH NOWAIT 

INSERT INTO @TestValuesAll 
SELECT 
	avgResultPctMin, 
	loseLimitMin, 
	ordersCount, 
	resultMin 
FROM 
	@AvgResultPctMin, 
	@LoseLimitMin, 
	@OrdersCount, 
	@ResultMin 

DROP TABLE IF EXISTS #TestValues 
CREATE TABLE #TestValues (
	avgResultPctMin int, 
	loseLimitMin decimal(11, 2), 
	ordersCount int, 
	resultMin int, 
	PRIMARY KEY (resultMin, loseLimitMin, ordersCount,avgResultPctMin)
) 

DROP TABLE IF EXISTS [#InvaliedSettings] 
CREATE TABLE [#InvaliedSettings]
(
	[AvgResultPctMinCurrent] [int] NOT NULL, 
	[LoseLimitMinCurrent] [decimal](18, 2) NOT NULL, 
	[OrdersCountCurrent] [int] NOT NULL, 
	[ResultMinCurrent] [int] NOT NULL, 
	[Nr3] [int] NOT NULL, 
	[MinCount] [int] NOT NULL
) ON [PRIMARY] 

CREATE NONCLUSTERED INDEX [InvaliedSettingsIndex]
ON [#InvaliedSettings] ( [AvgResultPctMinCurrent] ASC, [LoseLimitMinCurrent] ASC, [OrdersCountCurrent] ASC, [ResultMinCurrent] ASC, [Nr3] ASC, [MinCount] ASC ) 

SET @OutputString = 'Remove Existing Test Values ' + Cast(Getdate() AS varchar(20));
RAISERROR (@OutputString, 0, 1) WITH NOWAIT 


/* AvgIndicatorMin  = AvgResultPctMinCurrent */ 
INSERT INTO #TestValues 
SELECT 
	a.avgResultPctMin, 
	a.loseLimitMin, 
	a.ordersCount, 
	a.resultMin 
FROM 
	@TestValuesAll a 
	/*
	INSERT INTO #TestValues
	SELECT a.avgResultPctMin, a.loseLimitMin, a.ordersCount, a.resultMin
	FROM @TestValuesAll a
	EXCEPT 
	SELECT b.AvgIndicatorMin, b.LoseLimitMin, b.MaxOrders, b.MinResult
	FROM SettingsResults b
	*/
	
SET @OutputString = 'Begin Inserts' + Cast(	Getdate() AS varchar(20));
RAISERROR (@OutputString, 0, 1) WITH NOWAIT 
/*
SELECT * 
FROM #TestValues
*/
DECLARE @rows int = (SELECT COUNT(*) FROM #TestValues) 
DECLARE @currentRows int = 0 

SET @OutputString = 'Begin Inserting Values' + Cast(Getdate() AS varchar(20));
RAISERROR (@OutputString, 0, 1) WITH NOWAIT 
DECLARE @TestValuesAllCount int 
DECLARE @TestValuesCount int 
DECLARE @SettingsResultsCount int 
DECLARE @TotalSettingsToTest int 
DECLARE @TotalCount int 

SELECT 	@TestValuesAllCount = COUNT(*) FROM @TestValuesAll 
SELECT 	@TestValuesCount = COUNT(*) FROM #TestValues 
SELECT 	@SettingsResultsCount = COUNT(*) FROM [settingsresults] 

SET @TotalSettingsToTest = @TestValuesAllCount * (@maxNr - @MinNr + 1) 
SET @TotalCount = 0 
SET @OutputString = 'Nr3 Min: ' + Cast(@MinNr AS varchar(20)) + ' Nr3 Max: ' + Cast(@maxNr AS varchar(20)) 
RAISERROR (@OutputString, 0, 1) WITH NOWAIT 
SET @OutputString = 'Want To Insert: ' + Cast(@TotalSettingsToTest AS varchar(20)) + ' Values' 
SET @OutputString = @OutputString + ' - Existing Total: ' + Cast(@SettingsResultsCount AS varchar(20)) + ' Values' 
RAISERROR (@OutputString, 0, 1) WITH NOWAIT 

DECLARE @TestValuesActive 
TABLE (
	avgResultPctMin int, 
	loseLimitMin decimal(11, 2), 
	ordersCount int, 
	resultMin int, 
	IsEnabled bit, 
	nr3 int,
	PRIMARY KEY (
		resultMin, 
		loseLimitMin, 
		ordersCount, 
		avgResultPctMin,
		IsEnabled,
		nr3
	)
)

/* SKIP VALUES */
DELETE 
FROM #TestValues
WHERE ordersCount = 8
/*
DECLARE @TestValuesActiveId TABLE (
	Id int,
	Nr3 int,
	PRIMARY KEY 
	(
		Id, 
		Nr3
	)
	Reffrence?
)
*/

CREATE NONCLUSTERED INDEX [TestValuesIndex] ON #TestValues 
(
	avgResultPctMin ASC,
	resultMin ASC, 
	loseLimitMin ASC,
	ordersCount
)

WHILE @currentRows < @rows 
BEGIN 
	SELECT 
		@AvgResultPctMinCurrent = tv.avgResultPctMin, 
		@LoseLimitMinCurrent = tv.loseLimitMin, 
		@OrdersCountCurrent = tv.ordersCount, 
		@ResultMinCurrent = tv.resultMin 
	FROM 
		#TestValues tv 
	ORDER BY 
		avgResultPctMin ASC,
		resultMin ASC, 
		loseLimitMin ASC, 
		ordersCount
		OFFSET @currentRows ROWS FETCH NEXT 1 ROWS ONLY;
		
	SET @CurrentNr = @MinNr 
	WHILE @CurrentNr <= @maxNr 
	BEGIN 
		IF(NOT EXISTS (
					
					/*
					SELECT tva.IsEnabled
					FROM @TestValues tv, @TestValuesActive tva 
					WHERE @AvgResultPctMinCurrent >= AvgResultPctMinCurrent 
						AND @LoseLimitMinCurrent >= LoseLimitMinCurrent 
						AND @OrdersCountCurrent = OrdersCountCurrent 
						AND @ResultMinCurrent >= ResultMinCurrent 
						AND @MinCount = MinCount 
						AND @CurrentNr = Nr3
					*/
					SELECT 
						Nr3 
					FROM 
						[#InvaliedSettings]
					WHERE 
						@AvgResultPctMinCurrent >= AvgResultPctMinCurrent 
						AND @LoseLimitMinCurrent >= LoseLimitMinCurrent 
						AND @OrdersCountCurrent = OrdersCountCurrent 
						AND @ResultMinCurrent >= ResultMinCurrent 
						AND @MinCount = MinCount 
						AND @CurrentNr = Nr3
				)
			) 
		BEGIN 
			BEGIN TRY 
				SET @CurrentCount = 0;
				SET @CurrentMinAvg = 0.0;
				SET @CurrentAvgRes = 0.0;
				SELECT 
					@CurrentMinAvg = MIN(results.AvgGroup), 
					@CurrentAvgRes = COALESCE(SUM(results.SumRes)/ NULLIF(SUM(results.GroupCount), 0), 0), 
					@CurrentCount = SUM(results.GroupCount) 
				FROM (
						SELECT 
							bestRows.Nr3, 
							AVG(bestRows.Result3_New) AS AvgGroup, 
							SUM(bestRows.Result3_New) AS SumRes, 
							COUNT(bestRows.Result3_New) AS GroupCount 
						FROM (
								SELECT 
									cb_tmp2.Nr3, 
									cb_tmp2.Result3_New, 
									ROW_NUMBER() OVER (
										PARTITION BY cb_tmp2.nr3, 
										cb_tmp2.securityid, 
										cb_tmp2.result2 
										ORDER BY 
											RAND()
									) AS RowNum 
								FROM 
									combinationresult4_temp cb_tmp2, 
									[StockHistDB].[dbo].AvgIndicatorResults_Extended avg_res2 
								WHERE 
									(cb_tmp2.result2) IN (
										SELECT 
											MAX(cb_tmp.result2) AS result2 
										FROM 
											combinationresult4_temp cb_tmp, 
											[StockHistDB].[dbo].AvgIndicatorResults_Extended avg_res 
										WHERE 
											cb_tmp.result1 >= @ResultMinCurrent 
											AND cb_tmp.result2 >= @ResultMinCurrent 
											AND cb_tmp.result1 < 1200 
											AND cb_tmp.result2 < 1200 
											AND cb_tmp.LoseLimit >= @LoseLimitMinCurrent 
											AND cb_tmp.orders1 = @OrdersCountCurrent 
											AND cb_tmp.orders2 = cb_tmp.orders1 
											AND avg_res.AvgResultPct >= @AvgResultPctMinCurrent 
											AND cb_tmp.ShortIndicator = avg_res.ShortIndicator 
											AND cb_tmp.LongIndicator = avg_res.LongIndicator 
											AND cb_tmp.securityid = cb_tmp2.securityid 
											AND cb_tmp.nr3 = cb_tmp2.nr3 
										GROUP BY cb_tmp.nr3, cb_tmp.securityid
									) 
									AND avg_res2.AvgResultPct >= @AvgResultPctMinCurrent 
									AND cb_tmp2.ShortIndicator = avg_res2.ShortIndicator 
									AND cb_tmp2.LongIndicator = avg_res2.LongIndicator 
									AND cb_tmp2.result3_new < 800 
									AND cb_tmp2.nr3 = @currentNr
							) bestRows 
						WHERE 
							bestRows.RowNum = 1 
						GROUP BY bestRows.Nr3
				) results 
					
					IF(@CurrentCount >= @MinCount) 
					BEGIN 
						INSERT INTO [dbo].[settingsresults] (
							[AvgIndicatorMin], 
							[MinResAvg], 
							[resavg], 
							[countnr], 
							[minresult], 
							[minorders], 
							[maxorders], 
							[loselimitmin], 
							[Nr3]
						) 
						VALUES (
							@AvgResultPctMinCurrent, 
							@CurrentMinAvg, 
							@CurrentAvgRes,
							@CurrentCount, 
							@ResultMinCurrent, 
							@OrdersCountCurrent,
							@OrdersCountCurrent, 
							@LoseLimitMinCurrent,
							@CurrentNr
						)
					END 
				ELSE 
				BEGIN 
					THROW 51000, 'InvaliedSettings', 1 
				END 
				END TRY 
			BEGIN CATCH 	
				INSERT INTO [#InvaliedSettings]
				VALUES 
				(
					@AvgResultPctMinCurrent, @LoseLimitMinCurrent, 
					@OrdersCountCurrent, @ResultMinCurrent, 
					@CurrentNr, @MinCount
				) 
			/*
				INSERT INTO @TestValuesActive(avgResultPctMin, loseLimitMin, ordersCount, resultMin, IsEnabled, nr3)
				SELECT avgResultPctMin, loseLimitMin, ordersCount, resultMin, 0, @CurrentNr
				FROM #TestValues tv
				WHERE avgResultPctMin >= @AvgResultPctMinCurrent 
					AND loseLimitMin >= @LoseLimitMinCurrent
					AND ordersCount = @OrdersCountCurrent
					AND resultMin >= @ResultMinCurrent
			*/
			 
			END CATCH 
		END
			/*
			ELSE
			BEGIN
			SET @Every1000 = @Every1000 + 1
			SET @SkippedNr = @SkippedNr + 1
			END
			*/
		SET @TotalCount = @TotalCount + 1 
		SET @Every100 = @Every100 + 1
		SET @CurrentNr = @CurrentNr + 1 
	END 
	
	/* About 5-10 Min */
	IF(@Every100 >= 100000) 
	BEGIN 
		SELECT 
			@SettingsResultsCount = COUNT(*) 
		FROM 
			[settingsresults] 
		SET @Every100 = 0 
		SET @OutputString = 'Want To Insert: ' + Cast(@TotalSettingsToTest AS varchar(20)) + ' Values' 
		SET @OutputString = @OutputString + ' - Remaning: ' + Cast((@TotalSettingsToTest - @TotalCount) AS varchar(20)) + ' Values' 
		SET @OutputString = @OutputString + ' - Existing Total: ' + Cast(@SettingsResultsCount AS varchar(20)) + ' Values' 
		SET @OutputString = @OutputString + ' - Settings Tried: ' + Cast(@TotalCount AS varchar(20)) + ' Values' 
		
		SELECT @SecondsFromStart = DATEDIFF(SECOND, @StartDateTime, GETDATE()) 
		
		SET @EndDateTime = DATEADD(SECOND, ((@TotalSettingsToTest) /(@TotalCount / @SecondsFromStart)), @StartDateTime)
		SET @OutputString = @OutputString + ' - Completes At: ' + convert(varchar, @EndDateTime, 0)
		SET @OutputString = @OutputString + ' - Minutes Remaning: ' + Cast((DATEDIFF(MINUTE, @StartDateTime,  @EndDateTime)) AS varchar(20)) 
		/*SET @OutputString = @OutputString + ' - Skipped Values Total: ' + Cast(@SkippedNr AS varchar(20)) + ' Values' */
		RAISERROR (@OutputString, 0, 1) WITH NOWAIT 
	END 
	SET @currentRows = @currentRows + 1 
END

PRINT Cast(Getdate() AS DATETIME2 (3)) 


DROP TABLE IF EXISTS BestSettings

CREATE TABLE BestSettings
(
	[AvgIndicatorMin] [int] NOT NULL,
	[MinResAvg] [decimal](18, 2) NOT NULL,
	[ResAvg] [decimal](18, 2) NOT NULL,
	[CountNr] [int] NOT NULL,
	[MinResult] [int] NOT NULL,
	[MinOrders] [int] NOT NULL,
	[MaxOrders] [int] NOT NULL,
	[LoseLimitMin] [decimal](18, 2) NOT NULL,
	[Nr3] [int] NOT NULL
) ON [PRIMARY]

DECLARE @MixedOrdersCurrent int = 0
DECLARE @MixedOrdersMax int = 3
SET @MinCount = 30;


SELECT 
	@CurrentNr = MAX(cb_tmp.Nr3) 
FROM 
	combinationresult4_temp cb_tmp


WHILE @MixedOrdersCurrent < @MixedOrdersMax
BEGIN
	INSERT INTO BestSettings([AvgIndicatorMin],[MinResAvg],[ResAvg],[CountNr],[MinResult],[MinOrders],[MaxOrders],[LoseLimitMin],[Nr3]) 
	SELECT TOP(1) 
		[AvgIndicatorMin],[MinResAvg],[ResAvg],[CountNr],[MinResult],
		[MinOrders],[MaxOrders],[LoseLimitMin],[Nr3]
	FROM 
		[settingsresults] sr
	WHERE 
		/* [countnr] >= @MinCount AND */
		[Nr3]      = @CurrentNr AND
		NOT EXISTS (
			SELECT *
			FROM BestSettings bs
			WHERE bs.[MinOrders] = sr.[MinOrders]
		)
	ORDER BY 
		[ResAvg] DESC, [MinResAvg] DESC, [loselimitmin] DESC, [minresult] DESC

	SET @MixedOrdersCurrent = @MixedOrdersCurrent + 1;
END
";

        public static string FindLatestSecuritySettings = @"
GO
DROP TABLE IF EXISTS CombinationResult_Choosen
GO
CREATE TABLE CombinationResult_Choosen(
	[SecurityId] [nvarchar](20) NOT NULL, 
	[ShortIndicator] [nvarchar](30) NOT NULL, 
	[LongIndicator] [nvarchar](30) NOT NULL, 
	[Result1] [decimal](11, 2) NOT NULL, 
	[Result2] [decimal](11, 2) NOT NULL, 
	[Orders1] [int] NOT NULL,
	[Orders2] [int] NOT NULL, 
	[Nr1] [int] NOT NULL, 
	[Nr2] [int] NOT NULL, 
	[LoseLimitMin1] [decimal](11, 2) NOT NULL,
	[LoseLimitMin2] [decimal](11, 2) NOT NULL,
	[LoseLimit] [decimal](11, 2) NOT NULL
) ON [PRIMARY] 
GO

DECLARE @rowsCount int = 0
DECLARE @rowsCountEnd int = (SELECT COUNT(*) FROM BestSettings)

DECLARE @avgIndicatorMin int;
DECLARE @loseLimitMin [decimal](18, 2);
DECLARE @minResult int;
DECLARE @orders int;

WHILE @rowsCount < @rowsCountEnd
BEGIN
	SELECT 
		@avgIndicatorMin = bs.AvgIndicatorMin, 
		@loseLimitMin = bs.LoseLimitMin, 
		@minResult = bs.MinResult,
		@orders = bs.MinOrders
	FROM 
		BestSettings bs
	ORDER BY 
		ResAvg ASC,
		AvgIndicatorMin ASC,
		LoseLimitMin ASC, 
		MinResult ASC
		OFFSET @rowsCount ROWS FETCH NEXT 1 ROWS ONLY;


	WHILE(EXISTS ( 
			SELECT TOP 1 cr.*, avg_res.LoseLimit
			FROM
				CombinationResult_Lastest cr,
				AvgIndicatorResults_Extended avg_res
			WHERE 
				cr.Orders1 = @orders AND
				cr.Orders2 = @orders AND
				cr.LoseLimitMin1 >= @loseLimitMin AND
				cr.LoseLimitMin2 >= @loseLimitMin AND
				cr.Result1 >= @minResult AND
				cr.Result2 >= @minResult AND
				cr.result1 < 1200 AND
				cr.result2 < 1200 AND
				avg_res.ShortIndicator = cr.ShortIndicator AND
				avg_res.LongIndicator = cr.LongIndicator AND
				avg_res.AvgResultPct >= @avgIndicatorMin AND
				avg_res.LoseLimit = @loseLimitMin AND
				NOT EXISTS (SELECT * FROM CombinationResult_Choosen crc WHERE crc.SecurityId = cr.SecurityId)
		)
	)
	BEGIN
		INSERT INTO CombinationResult_Choosen
		SELECT TOP 1  cr.*, avg_res.LoseLimit
		FROM
			CombinationResult_Lastest cr,
			AvgIndicatorResults_Extended avg_res
		WHERE 
			cr.Orders1 = @orders AND
			cr.Orders2 = @orders AND
			cr.LoseLimitMin1 >= @loseLimitMin AND
			cr.LoseLimitMin2 >= @loseLimitMin AND
			cr.Result1 >= @minResult AND
			cr.Result2 >= @minResult AND
			cr.result1 < 1200 AND
			cr.result2 < 1200 AND 
			avg_res.ShortIndicator = cr.ShortIndicator AND
			avg_res.LongIndicator = cr.LongIndicator AND
			avg_res.AvgResultPct >= @avgIndicatorMin AND
			avg_res.LoseLimit = @loseLimitMin AND
			NOT EXISTS (SELECT * FROM CombinationResult_Choosen crc WHERE crc.SecurityId = cr.SecurityId)
		ORDER BY cr.Result2 DESC
	END

	SET @rowsCount = @rowsCount + 1;
END

SELECT c.*
FROM
	CombinationResult_Choosen c
ORDER BY c.Result2 DESC
";

        public static string SelectBestSecuritySettingsLatest(List<string> bestSettings)
        {
            string SelectBestSecuritySettingsLatestQuery = $"";

            foreach(string setting in bestSettings)
            {
                SelectBestSecuritySettingsLatestQuery += setting;
            }
            

            return SelectBestSecuritySettingsLatestQuery;
        }
    }
}
