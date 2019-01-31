/****** Object:  StoredProcedure [dbo].[UspReportGetContentLog]    Script Date: 11/14/2018 4:20:43 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UspReportGetContentLog]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UspReportGetContentLog]
GO
/****** Object:  StoredProcedure [dbo].[UspReportContentAuditSummary]    Script Date: 11/14/2018 4:20:43 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UspReportContentAuditSummary]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UspReportContentAuditSummary]
GO
/****** Object:  StoredProcedure [dbo].[UspReportChartData]    Script Date: 11/14/2018 4:20:43 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UspReportChartData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UspReportChartData]
GO
/****** Object:  StoredProcedure [dbo].[UspReportADGAgent]    Script Date: 11/14/2018 4:20:43 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UspReportADGAgent]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UspReportADGAgent]
GO
/****** Object:  StoredProcedure [dbo].[UspEventSettingGetMappingCount]    Script Date: 11/14/2018 4:20:43 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UspEventSettingGetMappingCount]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UspEventSettingGetMappingCount]
GO
/****** Object:  StoredProcedure [dbo].[UspDeactivateUser]    Script Date: 11/14/2018 4:20:43 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UspDeactivateUser]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UspDeactivateUser]
GO
/****** Object:  StoredProcedure [dbo].[UspCreateOrganization]    Script Date: 11/14/2018 4:20:43 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UspCreateOrganization]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UspCreateOrganization]
GO
/****** Object:  StoredProcedure [dbo].[UspActivateUser]    Script Date: 11/14/2018 4:20:43 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UspActivateUser]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UspActivateUser]
GO
/****** Object:  View [dbo].[VwOrganizationUser]    Script Date: 11/14/2018 4:20:43 AM ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[VwOrganizationUser]'))
DROP VIEW [dbo].[VwOrganizationUser]
GO
/****** Object:  View [dbo].[VwOrganization]    Script Date: 11/14/2018 4:20:43 AM ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[VwOrganization]'))
DROP VIEW [dbo].[VwOrganization]
GO
/****** Object:  View [dbo].[VwEventSettingUserSetup]    Script Date: 11/14/2018 4:20:43 AM ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[VwEventSettingUserSetup]'))
DROP VIEW [dbo].[VwEventSettingUserSetup]
GO
/****** Object:  View [dbo].[VwEventSetting]    Script Date: 11/14/2018 4:20:43 AM ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[VwEventSetting]'))
DROP VIEW [dbo].[VwEventSetting]
GO
/****** Object:  View [dbo].[VwADGAgent]    Script Date: 11/14/2018 4:20:43 AM ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[VwADGAgent]'))
DROP VIEW [dbo].[VwADGAgent]
GO
/****** Object:  View [dbo].[VwOrganizationUserLicense]    Script Date: 11/14/2018 4:20:43 AM ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[VwOrganizationUserLicense]'))
DROP VIEW [dbo].[VwOrganizationUserLicense]
GO
/****** Object:  View [dbo].[VwProductDetail]    Script Date: 11/14/2018 4:20:43 AM ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[VwProductDetail]'))
DROP VIEW [dbo].[VwProductDetail]
GO
/****** Object:  Table [dbo].[SystemUser]    Script Date: 11/14/2018 4:20:43 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SystemUser]') AND type in (N'U'))
DROP TABLE [dbo].[SystemUser]
GO
/****** Object:  Table [dbo].[Sessions]    Script Date: 11/14/2018 4:20:43 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sessions]') AND type in (N'U'))
DROP TABLE [dbo].[Sessions]
GO
/****** Object:  Table [dbo].[ProductUser]    Script Date: 11/14/2018 4:20:43 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductUser]') AND type in (N'U'))
DROP TABLE [dbo].[ProductUser]
GO
/****** Object:  Table [dbo].[ProductLicense]    Script Date: 11/14/2018 4:20:43 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductLicense]') AND type in (N'U'))
DROP TABLE [dbo].[ProductLicense]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 11/14/2018 4:20:43 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Product]') AND type in (N'U'))
DROP TABLE [dbo].[Product]
GO
/****** Object:  Table [dbo].[OrganizationUser]    Script Date: 11/14/2018 4:20:43 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrganizationUser]') AND type in (N'U'))
DROP TABLE [dbo].[OrganizationUser]
GO
/****** Object:  Table [dbo].[Organization]    Script Date: 11/14/2018 4:20:43 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Organization]') AND type in (N'U'))
DROP TABLE [dbo].[Organization]
GO
/****** Object:  Table [dbo].[EventSetting]    Script Date: 11/14/2018 4:20:43 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EventSetting]') AND type in (N'U'))
DROP TABLE [dbo].[EventSetting]
GO
/****** Object:  Table [dbo].[ErrorMessage]    Script Date: 11/14/2018 4:20:43 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ErrorMessage]') AND type in (N'U'))
DROP TABLE [dbo].[ErrorMessage]
GO
/****** Object:  Table [dbo].[ContentLog]    Script Date: 11/14/2018 4:20:43 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContentLog]') AND type in (N'U'))
DROP TABLE [dbo].[ContentLog]
GO
/****** Object:  Table [dbo].[AgentExecutionTime]    Script Date: 11/14/2018 4:20:43 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AgentExecutionTime]') AND type in (N'U'))
DROP TABLE [dbo].[AgentExecutionTime]
GO
/****** Object:  Table [dbo].[ADGAgent]    Script Date: 11/14/2018 4:20:43 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ADGAgent]') AND type in (N'U'))
DROP TABLE [dbo].[ADGAgent]
GO
/****** Object:  UserDefinedFunction [dbo].[UfnReportContentLog]    Script Date: 11/14/2018 4:20:43 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UfnReportContentLog]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[UfnReportContentLog]
GO
/****** Object:  UserDefinedFunction [dbo].[UfnReportADGAgent]    Script Date: 11/14/2018 4:20:43 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UfnReportADGAgent]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[UfnReportADGAgent]
GO
/****** Object:  UserDefinedFunction [dbo].[UfnGetCalenderSeries]    Script Date: 11/14/2018 4:20:43 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UfnGetCalenderSeries]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[UfnGetCalenderSeries]
GO
/****** Object:  UserDefinedFunction [dbo].[UfnCountContentLog]    Script Date: 11/14/2018 4:20:43 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UfnCountContentLog]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[UfnCountContentLog]
GO
/****** Object:  UserDefinedFunction [dbo].[UfnCountADGAgent]    Script Date: 11/14/2018 4:20:43 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UfnCountADGAgent]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[UfnCountADGAgent]
GO
/****** Object:  UserDefinedFunction [dbo].[UfnCountADGAgent]    Script Date: 11/14/2018 4:20:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UfnCountADGAgent]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[UfnCountADGAgent] (
	@OrganizationUrl VARCHAR(max)
	,@StartDate DATETIME = NULL
	,@EndDate DATETIME
	,@Status VARCHAR(50) = ''-1''
	)
RETURNS BIGINT
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Result BIGINT

	SELECT @Result = count(*)
	FROM [dbo].[ADGAgent] adg WITH (NOLOCK)
	INNER JOIN [dbo].[Organization] o WITH (NOLOCK) ON adg.OrganizationMasterId = o.OrganizationMasterId
	WHERE o.OrganizationRootUrl = @OrganizationUrl
		AND adg.IsProcessed = 1
		AND (
			CONVERT(NVARCHAR(10), adg.createdon, 120) >= CONVERT(NVARCHAR(10), @StartDate, 120)
			OR @StartDate IS NULL
			)
		AND CONVERT(NVARCHAR(10), adg.createdon, 120) <= @EndDate
		AND ProcessStatus = @Status
		OR @Status = ''-1''

	-- Return the result of the function
	RETURN @Result
END
' 
END

GO
/****** Object:  UserDefinedFunction [dbo].[UfnCountContentLog]    Script Date: 11/14/2018 4:20:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UfnCountContentLog]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[UfnCountContentLog] (
	@OrganizationUrl VARCHAR(max)
	,@StartDate DATETIME = NULL
	,@EndDate DATETIME
	,@CountError BIT = 0
	)
RETURNS BIGINT
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Result BIGINT

	SELECT @Result = count(*)
	FROM [dbo].[ContentLog] cl WITH (NOLOCK)
	INNER JOIN [dbo].[Organization] o WITH (NOLOCK) ON cl.OrganizationMasterId = o.OrganizationMasterId
	WHERE o.OrganizationRootUrl = @OrganizationUrl
		AND (
			(
				(
					cl.EventColumnValue IS NULL
					OR cl.ComplianceTagId IS NULL
					)
				AND @CountError = 1
				)
			OR @CountError = 0
			)
		AND (
			CONVERT(NVARCHAR(10), cl.createdon, 120) >= CONVERT(NVARCHAR(10), @StartDate, 120)
			OR @StartDate IS NULL
			)
		AND CONVERT(NVARCHAR(10), cl.createdon, 120) <= @EndDate

	-- Return the result of the function
	RETURN @Result
END
' 
END

GO
/****** Object:  UserDefinedFunction [dbo].[UfnGetCalenderSeries]    Script Date: 11/14/2018 4:20:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UfnGetCalenderSeries]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
CREATE FUNCTION [dbo].[UfnGetCalenderSeries] (
	@Start DATETIME
	,@End DATETIME
	,@ReportTimeline CHAR(1) = ''d''
	)
RETURNS @result TABLE (Result INT)
AS
BEGIN
		;

	WITH DateList
	AS (
		SELECT CASE 
				WHEN @ReportTimeline = ''y''
					THEN Year(@Start)
				WHEN @ReportTimeline = ''m''
					THEN Month(@Start)
				ELSE DAy(@Start)
				END [Date]
		
		UNION ALL
		
		SELECT [Date] + 1
		FROM DateList
		WHERE [Date] < CASE 
				WHEN @ReportTimeline = ''y''
					THEN Year(@End)
				WHEN @ReportTimeline = ''m''
					THEN Month(@End)
				ELSE DAy(@End)
				END
		)
	INSERT INTO @result
	SELECT [Date] [List of Date]
	FROM DateList

	RETURN
END
' 
END

GO
/****** Object:  UserDefinedFunction [dbo].[UfnReportADGAgent]    Script Date: 11/14/2018 4:20:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UfnReportADGAgent]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
CREATE FUNCTION [dbo].[UfnReportADGAgent] (
	@OrganizationUrl VARCHAR(max)
	,@StartDate DATETIME
	,@EndDate DATETIME
	,@Status VARCHAR(50) = ''-1''
	,@ReportPeriod CHAR(1) = ''d''
	)
RETURNS @result TABLE (
	OrganizationMasterId INT
	,ResultSeries INT
	,Result INT
	)
AS
BEGIN
	INSERT INTO @result
	SELECT adg.OrganizationMasterId
		,CASE 
			WHEN @ReportPeriod = ''y''
				THEN Year(adg.createdon)
			WHEN @ReportPeriod = ''m''
				THEN Month(adg.createdon)
			ELSE DAy(adg.createdon)
			END AS ResultSeries
		,count(*) AS Result
	FROM [dbo].[ADGAgent] adg WITH (NOLOCK)
	INNER JOIN [dbo].[Organization] o WITH (NOLOCK) ON adg.OrganizationMasterId = o.OrganizationMasterId
	WHERE o.OrganizationRootUrl = @OrganizationUrl
		AND adg.IsProcessed = 1
		AND CONVERT(NVARCHAR(10), adg.createdon, 120) >= CONVERT(NVARCHAR(10), @StartDate, 120)
		AND CONVERT(NVARCHAR(10), adg.createdon, 120) <= @EndDate
		AND ProcessStatus = @Status
		OR @Status = ''-1''
	GROUP BY adg.OrganizationMasterId
		,CASE 
			WHEN @ReportPeriod = ''y''
				THEN Year(adg.createdon)
			WHEN @ReportPeriod = ''m''
				THEN Month(adg.createdon)
			ELSE DAy(adg.createdon)
			END

	RETURN
END
' 
END

GO
/****** Object:  UserDefinedFunction [dbo].[UfnReportContentLog]    Script Date: 11/14/2018 4:20:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UfnReportContentLog]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[UfnReportContentLog] (
	@OrganizationUrl VARCHAR(max)
	,@StartDate DATETIME
	,@EndDate DATETIME
	,@CountError BIT = 0
	,@ReportPeriod CHAR(1) = ''d''
	)
RETURNS @result TABLE (
	OrganizationMasterId INT
	,ResultSeries INT
	,Result INT
	)
AS
BEGIN
	INSERT INTO @result
	SELECT cl.OrganizationMasterId
		,CASE 
			WHEN @ReportPeriod = ''y''
				THEN Year(cl.createdon)
			WHEN @ReportPeriod = ''m''
				THEN Month(cl.createdon)
			ELSE DAy(cl.createdon)
			END AS ResultSeries
		,count(*) AS Result
	FROM [dbo].[ContentLog] cl WITH (NOLOCK)
	INNER JOIN [dbo].[Organization] o WITH (NOLOCK) ON cl.OrganizationMasterId = o.OrganizationMasterId
	WHERE o.OrganizationRootUrl = @OrganizationUrl
		AND (
			(
				(
					cl.EventColumnValue IS NULL
					OR cl.ComplianceTagId IS NULL
					)
				AND @CountError = 1
				)
			OR @CountError = 0
			)
		AND (
			CONVERT(NVARCHAR(10), cl.createdon, 120) >= CONVERT(NVARCHAR(10), @StartDate, 120)
			OR @StartDate IS NULL
			)
		AND CONVERT(NVARCHAR(10), cl.createdon, 120) <= @EndDate
	GROUP BY cl.OrganizationMasterId
		,CASE 
			WHEN @ReportPeriod = ''y''
				THEN Year(cl.createdon)
			WHEN @ReportPeriod = ''m''
				THEN Month(cl.createdon)
			ELSE DAy(cl.createdon)
			END
	RETURN
END
' 
END

GO
/****** Object:  Table [dbo].[ADGAgent]    Script Date: 11/14/2018 4:20:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ADGAgent]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ADGAgent](
	[ADGAgentId] [bigint] IDENTITY(100,1) NOT NULL,
	[ContentLogId] [bigint] NOT NULL,
	[OrganizationMasterId] [int] NOT NULL,
	[ContentType] [varchar](100) NOT NULL,
	[ComplianceTag] [varchar](100) NOT NULL,
	[ComplianceTagId] [varchar](100) NOT NULL,
	[ComplainceAssetId] [varchar](100) NULL,
	[EventColumnValue] [datetime] NOT NULL,
	[DocId] [varchar](max) NULL,
	[IsProcessed] [bit] NOT NULL CONSTRAINT [DF_ADGAgent_IsProcessed]  DEFAULT ((0)),
	[ProcessStatus] [varchar](50) NULL,
	[ProcessDescription] [varchar](max) NULL,
	[LibraryName] [varchar](100) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_ADGAgent] PRIMARY KEY CLUSTERED 
(
	[ADGAgentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AgentExecutionTime]    Script Date: 11/14/2018 4:20:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AgentExecutionTime]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AgentExecutionTime](
	[AgentExecutionTimeId] [int] IDENTITY(1,1) NOT NULL,
	[OrganizationMasterId] [int] NOT NULL,
	[TimeZoneDisplayName] [varchar](100) NOT NULL,
	[TimeZone] [varchar](50) NOT NULL,
	[Time] [nvarchar](5) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_AgentExecutionTime] PRIMARY KEY CLUSTERED 
(
	[AgentExecutionTimeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ContentLog]    Script Date: 11/14/2018 4:20:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContentLog]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ContentLog](
	[ContentLogId] [bigint] IDENTITY(100,1) NOT NULL,
	[OrganizationMasterId] [int] NOT NULL,
	[ContentType] [varchar](100) NOT NULL,
	[ContentTypeId] [varchar](max) NOT NULL,
	[DocId] [varchar](max) NULL,
	[LastModifiedTime] [datetime] NOT NULL,
	[FileType] [varchar](100) NULL,
	[Title] [varchar](max) NULL,
	[EventColumnValue] [datetime] NULL,
	[Author] [varchar](100) NULL,
	[ComplianceTag] [varchar](100) NULL,
	[ComplianceTagId] [varchar](100) NULL,
	[ComplainceAssetId] [varchar](100) NULL,
	[DocUrl] [varchar](max) NOT NULL,
	[IsProcessed] [bit] NOT NULL,
	[LibraryName] [varchar](100) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedOn] [datetime] NULL,
	[DocumentName] [varchar](200) NOT NULL DEFAULT (''),
 CONSTRAINT [PK_ContentLog] PRIMARY KEY CLUSTERED 
(
	[ContentLogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ErrorMessage]    Script Date: 11/14/2018 4:20:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ErrorMessage]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ErrorMessage](
	[ErrorMessageId] [int] IDENTITY(100,1) NOT NULL,
	[ErrorKey] [varchar](80) NOT NULL,
	[Message] [varchar](250) NOT NULL,
	[Description] [varchar](400) NOT NULL,
	[IsActive] [bit] NOT NULL DEFAULT ((1)),
	[CreatedBy] [int] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_ErrorMessage] PRIMARY KEY CLUSTERED 
(
	[ErrorMessageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EventSetting]    Script Date: 11/14/2018 4:20:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EventSetting]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[EventSetting](
	[EventSettingId] [bigint] IDENTITY(100,1) NOT NULL,
	[OrganizationMasterId] [int] NOT NULL,
	[ContentType] [varchar](100) NOT NULL,
	[ContentTypeId] [varchar](max) NOT NULL,
	[EventColumnName] [varchar](100) NOT NULL,
	[EventColumnInternalName] [varchar](100) NOT NULL,
	[IsActive] [bit] NOT NULL CONSTRAINT [DF_EventSetting_IsActive]  DEFAULT ((1)),
	[Comments] [varchar](max) NULL,
	[LastRetrivalTime] [datetime] NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedOn] [datetime] NULL,
	[ContentSiteUrl] [varchar](max) NOT NULL DEFAULT (''),
 CONSTRAINT [PK_EventSetting] PRIMARY KEY CLUSTERED 
(
	[EventSettingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Organization]    Script Date: 11/14/2018 4:20:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Organization]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Organization](
	[OrganizationMasterId] [int] IDENTITY(100,1) NOT NULL,
	[OrganizationId] [varchar](255) NOT NULL,
	[OrganizationName] [varchar](100) NOT NULL,
	[OrganizationRootUrl] [varchar](max) NOT NULL,
	[IsActive] [bit] NOT NULL CONSTRAINT [DF__Organizat__IsAct__1BC821DD]  DEFAULT ((1)),
	[ExpiryDate] [datetime] NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_Organization] PRIMARY KEY CLUSTERED 
(
	[OrganizationMasterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[OrganizationUser]    Script Date: 11/14/2018 4:20:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrganizationUser]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[OrganizationUser](
	[OrganizationUserId] [int] IDENTITY(100,1) NOT NULL,
	[OrganizationMasterId] [int] NOT NULL,
	[UserId] [varchar](100) NOT NULL,
	[Password] [varchar](100) NOT NULL,
	[IsActive] [bit] NOT NULL CONSTRAINT [DF_OrganizationUser_IsActive]  DEFAULT ((1)),
	[UserAccessTo] [varchar](10) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_OrganizationUser] PRIMARY KEY CLUSTERED 
(
	[OrganizationUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Product]    Script Date: 11/14/2018 4:20:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Product]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Product](
	[ProductId] [int] IDENTITY(100,1) NOT NULL,
	[Name] [varchar](20) NOT NULL,
	[Description] [varchar](20) NOT NULL,
	[IsActive] [bit] NOT NULL DEFAULT ((1)),
	[CreatedBy] [int] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProductLicense]    Script Date: 11/14/2018 4:20:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductLicense]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ProductLicense](
	[ProductLicenseId] [int] IDENTITY(100,1) NOT NULL,
	[OrganizationMasterId] [int] NOT NULL,
	[OrganizationId] [varchar](255) NOT NULL,
	[ProductId] [int] NOT NULL,
	[LicenseCount] [int] NOT NULL,
	[LicenseUsed] [int] NULL DEFAULT ((0)),
	[CreatedBy] [int] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_ProductLicense] PRIMARY KEY CLUSTERED 
(
	[ProductLicenseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProductUser]    Script Date: 11/14/2018 4:20:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductUser]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ProductUser](
	[ProductUserId] [int] IDENTITY(100,1) NOT NULL,
	[ProductId] [int] NOT NULL,
	[OrganizationMasterId] [int] NOT NULL,
	[SystemUserId] [int] NOT NULL,
	[IsActive] [bit] NOT NULL CONSTRAINT [DF_ProductUser_IsActive]  DEFAULT ((1)),
	[CreatedBy] [int] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_ProductUser] PRIMARY KEY CLUSTERED 
(
	[ProductUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Sessions]    Script Date: 11/14/2018 4:20:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sessions]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Sessions](
	[SessionId] [nvarchar](88) NOT NULL,
	[Created] [datetime] NOT NULL,
	[Expires] [datetime] NOT NULL,
	[LockDate] [datetime] NOT NULL,
	[LockCookie] [int] NOT NULL,
	[Locked] [bit] NOT NULL,
	[SessionItem] [image] NULL,
	[Flags] [int] NOT NULL,
	[Timeout] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[SessionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[SystemUser]    Script Date: 11/14/2018 4:20:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SystemUser]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SystemUser](
	[SystemUserId] [int] IDENTITY(100,1) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[DisplayName] [varchar](101) NOT NULL,
	[EmailAddress] [varchar](100) NOT NULL,
	[OrganizationMasterId] [int] NOT NULL,
	[IsActive] [bit] NOT NULL CONSTRAINT [DF__SystemUse__IsAct__2E1BDC42]  DEFAULT ((1)),
	[CreatedBy] [int] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_SystemUser] PRIMARY KEY CLUSTERED 
(
	[SystemUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  View [dbo].[VwProductDetail]    Script Date: 11/14/2018 4:20:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[VwProductDetail]'))
EXEC dbo.sp_executesql @statement = N'

CREATE VIEW [dbo].[VwProductDetail]
AS
SELECT p.ProductId
	,Name
	,Description
	,p.IsActive AS IsProductActive
	,LicenseCount
	,LicenseUsed
	,pl.OrganizationMasterId
	,SystemUserId
	,pu.IsActive AS IsProductUserActive
FROM product p
INNER JOIN productLicense pl ON p.productid = pl.productid
LEFT JOIN [dbo].[ProductUser] pu ON p.productid = pu.productid
	AND pu.[OrganizationMasterId] = pl.[OrganizationMasterId]


' 
GO
/****** Object:  View [dbo].[VwOrganizationUserLicense]    Script Date: 11/14/2018 4:20:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[VwOrganizationUserLicense]'))
EXEC dbo.sp_executesql @statement = N'









CREATE VIEW [dbo].[VwOrganizationUserLicense]
AS
SELECT 
    o.OrganizationMasterId
	,o.OrganizationName
	,o.OrganizationId	
	,su.SystemUserId
	,su.EmailAddress
	,su.DisplayName
	,su.FirstName
	,su.LastName
	,o.IsActive AS IsOrganizationActive
	,su.IsActive AS IsUserActive
	,Name
	,Description
	,IsProductActive
	,LicenseCount
	,pd.[ProductId]
	,[IsProductUserActive]
	,LicenseUsed

FROM Organization o 
left join SystemUser su ON su.OrganizationMasterId = o.OrganizationMasterId
left join VwProductDetail pd on pd.OrganizationMasterId=su.OrganizationMasterId and pd.systemuserid= su.SystemUserId








' 
GO
/****** Object:  View [dbo].[VwADGAgent]    Script Date: 11/14/2018 4:20:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[VwADGAgent]'))
EXEC dbo.sp_executesql @statement = N'


CREATE VIEW [dbo].[VwADGAgent]
AS
SELECT ADGAgentId
	,ContentLogId
	,ad.OrganizationMasterId
	,ContentType
	,ComplianceTag
	,ComplianceTagId
	,ComplainceAssetId
	,EventColumnValue
	,[DocId]
	,IsProcessed
	,ProcessStatus
	,ProcessDescription
	,LibraryName
	,ad.CreatedBy
	,ad.CreatedOn
	,[UserId]
	,[Password]
	,ou.IsActive AS UserIsActive
	,cast(ad.organizationMasterId as varchar)+ ContentType+ComplianceTagId+convert(nvarchar(10),EventColumnValue,120) as ADGProcessId
FROM [ADGAgent] ad WITH (NOLOCK)
INNER JOIN [dbo].[OrganizationUser] ou WITH (NOLOCK) ON ad.OrganizationMasterId = ou.OrganizationMasterId
WHERE ou.[UserAccessTo] = ''adg''


' 
GO
/****** Object:  View [dbo].[VwEventSetting]    Script Date: 11/14/2018 4:20:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[VwEventSetting]'))
EXEC dbo.sp_executesql @statement = N'
CREATE VIEW [dbo].[VwEventSetting]
AS
SELECT EventSettingId
	,es.OrganizationMasterId
	,ContentType
	,ContentTypeId
	,EventColumnName
	,EventColumnInternalName
	,es.IsActive
	,Comments
	,es.CreatedBy
	,es.CreatedOn
	,es.[LastRetrivalTime]
	,o.[OrganizationRootUrl]
	,[ContentSiteUrl]
	,[UserId]
	,[Password]
	,ou.IsActive AS UserIsActive
	,[TimeZone]
	,[Time]
FROM [dbo].[EventSetting] es WITH (NOLOCK)
INNER JOIN dbo.organization o WITH (NOLOCK) ON es.OrganizationMasterId = o.OrganizationMasterId
INNER JOIN [dbo].[OrganizationUser] ou WITH (NOLOCK) ON es.OrganizationMasterId = ou.OrganizationMasterId
LEFT JOIN [dbo].[AgentExecutionTime] aet WITH (NOLOCK) ON es.OrganizationMasterId = aet.OrganizationMasterId
WHERE ou.[UserAccessTo] = ''sp''
' 
GO
/****** Object:  View [dbo].[VwEventSettingUserSetup]    Script Date: 11/14/2018 4:20:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[VwEventSettingUserSetup]'))
EXEC dbo.sp_executesql @statement = N'

CREATE VIEW [dbo].[VwEventSettingUserSetup]
AS
SELECT 

o.OrganizationRootUrl,
o.OrganizationMasterId
	,o.OrganizationName
	,o.OrganizationId	
	,o.IsActive AS IsOrganizationActive
	,ou.OrganizationUserId
	,ou.IsActive AS IsOrganizationUserActive

FROM Organization o
inner JOIN OrganizationUser ou ON ou.OrganizationMasterId = o.OrganizationMasterId	




' 
GO
/****** Object:  View [dbo].[VwOrganization]    Script Date: 11/14/2018 4:20:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[VwOrganization]'))
EXEC dbo.sp_executesql @statement = N'
CREATE VIEW [dbo].[VwOrganization]
AS
SELECT OrganizationMasterId
	,OrganizationId
	,OrganizationName
	,OrganizationRootUrl
	,IsActive
FROM Organization o
' 
GO
/****** Object:  View [dbo].[VwOrganizationUser]    Script Date: 11/14/2018 4:20:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[VwOrganizationUser]'))
EXEC dbo.sp_executesql @statement = N'
CREATE VIEW [dbo].[VwOrganizationUser]
AS
SELECT o.OrganizationMasterId
	,o.OrganizationName
	,o.OrganizationId	
	,su.SystemUserId
	,su.EmailAddress
	,su.DisplayName
	,su.FirstName
	,su.LastName
	,o.IsActive AS IsOrganizationActive
	,su.IsActive AS IsUserActive
,[OrganizationRootUrl]
FROM Organization o
INNER JOIN SystemUser su ON su.OrganizationMasterId = o.OrganizationMasterId	
' 
GO
/****** Object:  StoredProcedure [dbo].[UspActivateUser]    Script Date: 11/14/2018 4:20:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UspActivateUser]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[UspActivateUser] AS' 
END
GO

--exec [dbo].[UspActivateUser] 'Vikram.Wadhawan@rsystems.com'
--select * from 
ALTER PROCEDURE [dbo].[UspActivateUser] (@EmailAddress NVARCHAR(50))
AS
BEGIN
	BEGIN TRAN t1

	SET NOCOUNT ON

	BEGIN TRY
		DECLARE @Mail NVARCHAR(50)
		DECLARE @SystemUserId INT
		DECLARE @OrganizationMasterId INT
		DECLARE @Message NVARCHAR(500)
		DECLARE @tblProduct TABLE (
			ProductId INT
			,ProductName NVARCHAR(50)
			,ProductCount INT
			,LicenseCount INT DEFAULT(0)
			,LicenseUsed INT DEFAULT(0)
			,Message NVARCHAR(max) DEFAULT(0)
			)

		SET @Mail = LTRIM(RTRIM(@EmailAddress))

		SELECT @SystemUserId = SystemUserId
			,@OrganizationMasterId = OrganizationMasterId
		FROM SystemUser
		WHERE EmailAddress = @Mail

		INSERT INTO @tblProduct (
			ProductId
			,ProductCount
			,LicenseCount
			,LicenseUsed
			)
		SELECT pd.ProductId
			,count(pu.ProductUserId)
			,0
			,0
		FROM ProductUser pu
		INNER JOIN Product pd ON pu.ProductId = pd.ProductId
		WHERE pu.SystemUserId = @SystemUserId
			AND pu.OrganizationMasterId = @OrganizationMasterId
			AND pu.IsActive = 0
		GROUP BY pd.ProductId

		UPDATE tp
		SET tp.LicenseCount = pl.LicenseCount
			,tp.LicenseUsed = pl.LicenseUsed
			,ProductName = pd.NAME
		FROM @tblProduct tp
		INNER JOIN productlicense pl ON tp.ProductId = pl.ProductId
		INNER JOIN Product pd ON tp.ProductId = pd.ProductId
		WHERE pl.OrganizationMasterId = @OrganizationMasterId

		UPDATE @tblProduct
		SET Message = CASE 
				WHEN LicenseCount = LicenseUsed
					THEN 'Licenses finished for ' + ProductName
				ELSE 'User Activated Successfully.'
				END

		UPDATE pl
		SET pl.LicenseUsed = CASE 
				WHEN pl.LicenseUsed < tt.LicenseCount
					AND tt.Message = 'User Activated Successfully.'
					THEN pl.LicenseUsed + 1
				END
		FROM ProductLicense pl
		INNER JOIN @tblProduct tt ON pl.ProductId = tt.ProductId
		WHERE pl.OrganizationMasterId = @OrganizationMasterId

		IF @@ROWCOUNT <> 0
		BEGIN
			UPDATE ProductUser
			SET IsActive = 1
			WHERE SystemUserId = @SystemUserId

			UPDATE SystemUser
			SET IsActive = 1
			WHERE SystemUserId = @SystemUserId
		END

		SELECT *
		FROM @tblProduct

		COMMIT TRAN t1

		COMMIT;
	END TRY

	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRAN t1;
	END CATCH
END

GO
/****** Object:  StoredProcedure [dbo].[UspCreateOrganization]    Script Date: 11/14/2018 4:20:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UspCreateOrganization]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[UspCreateOrganization] AS' 
END
GO

ALTER PROCEDURE [dbo].[UspCreateOrganization] (
	 @OrganizationId NVARCHAR(255)
	,@Name NVARCHAR(255)	
	,@RootUrl NVARCHAR(max)=''
	,@RMOLicences INT		
	)
AS
BEGIN
	SET NOCOUNT ON

	IF NOT EXISTS (
			SELECT TOP 1 *
			FROM organization
			WHERE OrganizationId = @OrganizationId
				OR OrganizationName = @Name
			)
	BEGIN
		BEGIN TRAN t1

		BEGIN TRY
			
			DECLARE @OrganizationMasterId INT					
			
			INSERT INTO Organization (
				OrganizationId
				,OrganizationName				
				,OrganizationRootUrl
				,CreatedBy
				,CreatedOn
				)
			VALUES (
				@OrganizationId
				,@Name
				,@RootUrl
				,1
				,GETDATE()
				)

			SELECT @OrganizationMasterId = SCOPE_IDENTITY()

		

			INSERT INTO ProductLicense (
				OrganizationMasterId
				,OrganizationId
				,ProductId
				,LicenseCount
				,createdby
				,createdon
				)
			SELECT @OrganizationMasterId
				,@OrganizationId
				,ProductId
				,CASE 
					WHEN lower(NAME) = 'rmo'
						THEN @RMOLicences					
					END
				,1
				,getdate()
			FROM Product

			COMMIT TRAN t1
		END TRY

		BEGIN CATCH
			IF @@TRANCOUNT > 0
				ROLLBACK TRAN t1;
		END CATCH
	END
	ELSE
	BEGIN
		PRINT ('Organization already exists in the systems')
	END
END

GO
/****** Object:  StoredProcedure [dbo].[UspDeactivateUser]    Script Date: 11/14/2018 4:20:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UspDeactivateUser]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[UspDeactivateUser] AS' 
END
GO

ALTER PROCEDURE [dbo].[UspDeactivateUser] (@EmailAddress NVARCHAR(50))
AS
BEGIN
	BEGIN TRAN t1

	SET NOCOUNT ON

	BEGIN TRY
		DECLARE @Mail NVARCHAR(50)
		DECLARE @SystemUserId INT
		DECLARE @OrganizationMasterId INT
		DECLARE @tblProduct TABLE (
			ProductId INT
			,ProductCount INT
			)

		SET @Mail = LTRIM(RTRIM(@EmailAddress))

		SELECT @SystemUserId = SystemUserId
			,@OrganizationMasterId = OrganizationMasterId
		FROM SystemUser
		WHERE EmailAddress = @Mail

		INSERT INTO @tblProduct
		SELECT pd.ProductId
			,count(pu.ProductUserId)
		FROM ProductUser pu
		INNER JOIN Product pd ON pu.ProductId = pd.ProductId
		WHERE pu.SystemUserId = @SystemUserId
			AND pu.OrganizationMasterId = @OrganizationMasterId
			AND pu.IsActive = 1
		GROUP BY pd.ProductId

		UPDATE pl
		SET pl.LicenseUsed = CASE 
				WHEN pl.LicenseUsed >= tt.ProductCount
					THEN pl.LicenseUsed - tt.ProductCount
				WHEN pl.LicenseUsed = 0
					THEN pl.LicenseUsed
				END
		FROM ProductLicense pl
		INNER JOIN @tblProduct tt ON pl.ProductId = tt.ProductId
		WHERE pl.OrganizationMasterId = @OrganizationMasterId

		IF @@ROWCOUNT <> 0
		BEGIN
			UPDATE ProductUser
			SET IsActive = 0
			WHERE SystemUserId = @SystemUserId

			UPDATE SystemUser
			SET IsActive = 0
			WHERE SystemUserId = @SystemUserId
		END

		COMMIT TRAN t1

		--execute all your stored proc code here and then commit
		COMMIT;
	END TRY

	BEGIN CATCH
		--if an exception occurs execute your rollback, also test that you have had some successful transactions
		IF @@TRANCOUNT > 0
			ROLLBACK TRAN t1;
	END CATCH
END

GO
/****** Object:  StoredProcedure [dbo].[UspEventSettingGetMappingCount]    Script Date: 11/14/2018 4:20:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UspEventSettingGetMappingCount]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[UspEventSettingGetMappingCount] AS' 
END
GO
ALTER PROC [dbo].[UspEventSettingGetMappingCount]
(@OrganizationUrl VARCHAR(max)='')
AS
BEGIN
	SELECT isnull(count(EventSettingId), 0) AS MappedCount
		,o.[OrganizationRootUrl],ContentSiteUrl
	FROM [dbo].[EventSetting] es WITH (NOLOCK)
	INNER JOIN dbo.organization o WITH (NOLOCK) ON es.OrganizationMasterId = o.OrganizationMasterId
	GROUP BY OrganizationRootUrl,ContentSiteUrl
END

GO
/****** Object:  StoredProcedure [dbo].[UspReportADGAgent]    Script Date: 11/14/2018 4:20:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UspReportADGAgent]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[UspReportADGAgent] AS' 
END
GO

ALTER PROC [dbo].[UspReportADGAgent] (
	@OrganizationUrl VARCHAR(max)
	,@StartDate DATETIME = NULL
	,@EndDate DATETIME
	,@Status VARCHAR(50) = '-1'
	)
AS
BEGIN
	DECLARE @Result TABLE (
		ResultId INT identity(1, 1) NOT NULL
		,ItemProcessedToday BIGINT NOT NULL
		,ItemProcessedToDate BIGINT NOT NULL
		,ItemErrorsToDate BIGINT NOT NULL
		,EventCreatedToday BIGINT NOT NULL
		,EventUpdatedToday BIGINT NOT NULL
		,EventCreatedToDate BIGINT NOT NULL
		,TotalActiveContentType BIGINT NOT NULL
		)
	DECLARE @currentDate DATETIME = getdate()

	INSERT @Result
	SELECT dbo.UfnCountContentLog(@OrganizationUrl, @currentDate, @EndDate, 0)
		,dbo.UfnCountContentLog(@OrganizationUrl, NULL, @EndDate, 0)
		,dbo.UfnCountContentLog(@OrganizationUrl, NULL, @EndDate, 1)
		,dbo.UfnCountADGAgent(@OrganizationUrl, @currentDate, @EndDate, 'Created')
		,dbo.UfnCountADGAgent(@OrganizationUrl, @currentDate, @EndDate, 'Updated')
		,dbo.UfnCountADGAgent(@OrganizationUrl, NULL, @EndDate, 'Created')
		,(select count(*) from [dbo].VwEventSetting with(nolock) where OrganizationRootUrl= @OrganizationUrl and IsActive=1)
	SELECT *
	FROM @Result
END

GO
/****** Object:  StoredProcedure [dbo].[UspReportChartData]    Script Date: 11/14/2018 4:20:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UspReportChartData]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[UspReportChartData] AS' 
END
GO

ALTER PROC [dbo].[UspReportChartData] (
	@OrganizationUrl VARCHAR(max)
	,@StartDate DATETIME
	,@EndDate DATETIME
	)
AS
BEGIN
	DECLARE @ReportPeriod CHAR(1) = 'd'

	IF (Year(@StartDate) <> Year(@endDate))
	BEGIN
		SET @ReportPeriod = 'y'
	END
	ELSE
	BEGIN
		IF (month(@StartDate) <> month(@endDate))
		BEGIN
			SET @ReportPeriod = 'm'
		END
		ELSE
		BEGIN
			SET @ReportPeriod = 'd'
		END
	END

	SELECT cal.Result AS ResultSeries
		,isnull(isnull(isnull(pr.OrganizationMasterId, er.OrganizationMasterId), ev.OrganizationMasterId), 0) AS OrganizationMasterId
		,isnull(pr.Result, 0) AS ItemProcessed
		,isnull(er.Result, 0) AS ItemFailed
		,isnull(ev.Result, 0) AS EventCreated
		,@ReportPeriod AS ReportTimeline
	FROM [dbo].[UfnGetCalenderSeries](@StartDate, @EndDate, @ReportPeriod) cal
	LEFT JOIN dbo.UfnReportContentLog(@OrganizationUrl, @StartDate, @EndDate, 0, @ReportPeriod) AS pr ON cal.result = pr.ResultSeries
	LEFT JOIN dbo.UfnReportContentLog(@OrganizationUrl, @StartDate, @EndDate, 1, @ReportPeriod) AS er ON cal.result = er.ResultSeries
	LEFT JOIN [dbo].[UfnReportADGAgent](@OrganizationUrl, @StartDate, @EndDate, 'Created', @ReportPeriod) AS ev ON cal.result = ev.ResultSeries
	ORDER BY ResultSeries
END

GO
/****** Object:  StoredProcedure [dbo].[UspReportContentAuditSummary]    Script Date: 11/14/2018 4:20:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UspReportContentAuditSummary]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[UspReportContentAuditSummary] AS' 
END
GO

ALTER PROC [dbo].[UspReportContentAuditSummary] (
	@OrganizationUrl VARCHAR(max)
	,@SearchTerm VARCHAR(max)
	,@PageNbr INT = 1
	,@PageSize INT = 100
	)
AS
BEGIN
	DECLARE @lFirstRec INT
		,@lLastRec INT

	SET @lFirstRec = (@PageNbr - 1) * @PageSize
	SET @lLastRec = (@PageNbr * @PageSize + 1)

	DECLARE @OrganizationMasterId INT

	SELECT TOP 1 @OrganizationMasterId = OrganizationMasterId
	FROM [dbo].[Organization]
	WHERE OrganizationRootUrl = @OrganizationUrl

	DECLARE @Result TABLE (
		Id INT identity(1, 1)
		,RowNumber INT
		,[ContentLogId] [bigint] NOT NULL
		,[ContentType] [varchar](100) NOT NULL
		,[DocId] [varchar](max) NULL
		,[LastModifiedTime] [datetime] NOT NULL
		,[FileType] [varchar](100) NULL
		,[EventColumnValue] [datetime] NULL
		,[ComplianceTag] [varchar](100) NULL
		,[ComplianceTagId] [varchar](100) NULL
		,[ComplainceAssetId] [varchar](100) NULL
		,[DocUrl] [varchar](max) NOT NULL
		,[DocumentName] [varchar](200) NOT NULL
		,[CreatedOn] [datetime] NOT NULL
		,[ModifiedOn] [datetime] NULL
		,[ProcessStatus] [varchar](50) NOT NULL
		,[EventCreatedDate] [datetime] NULL
		,[ProcessDescription] [varchar](max) NULL
		)

	INSERT @result
	SELECT ROW_NUMBER() OVER (
			ORDER BY cl.documentname
				,cl.contentlogid DESC
			) AS rownum
		,cl.ContentLogId
		,cl.ContentType
		,cl.DocId
		,LastModifiedTime
		,FileType
		,cl.EventColumnValue
		,cl.ComplianceTag
		,cl.ComplianceTagId
		,cl.ComplainceAssetId
		,DocUrl
		,cl.DocumentName
		,cl.CreatedOn
		,cl.ModifiedOn
		,isnull(ag.ProcessStatus, 'Not Processed')
		,CASE 
			WHEN ag.ProcessStatus IS NULL
				THEN NULL
			ELSE ag.ModifiedOn
			END AS EventCreatedDate
		,ProcessDescription
	FROM [dbo].[ContentLog] cl WITH (NOLOCK)
	LEFT JOIN [dbo].[ADGAgent] ag WITH (NOLOCK) ON cl.ContentLogId = ag.ContentLogId
	WHERE cl.OrganizationMasterId = @OrganizationMasterId
		AND cl.documentname LIKE '%' + @SearchTerm + '%'

	SELECT *
	FROM @result
	WHERE RowNumber > @lFirstRec
		AND RowNumber < @lLastRec
	ORDER BY RowNumber ASC
END

GO
/****** Object:  StoredProcedure [dbo].[UspReportGetContentLog]    Script Date: 11/14/2018 4:20:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UspReportGetContentLog]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[UspReportGetContentLog] AS' 
END
GO

ALTER PROC [dbo].[UspReportGetContentLog] (
	@OrganizationUrl VARCHAR(max)
	,@StartDate DATETIME = NULL
	,@EndDate DATETIME
	,@CountError BIT = 1
	,@PageNbr INT = 1
	,@PageSize INT = 10
	)
AS
BEGIN
	DECLARE @lFirstRec INT
		,@lLastRec INT

	SET @lFirstRec = (@PageNbr - 1) * @PageSize
	SET @lLastRec = (@PageNbr * @PageSize + 1)

	DECLARE @Result TABLE (
		Id INT identity(1, 1)
		,RowNumber INT
		,[ContentLogId] [bigint] NOT NULL
		,[ContentType] [varchar](100) NOT NULL
		,[DocId] [varchar](max) NULL
		,[LastModifiedTime] [datetime] NOT NULL
		,[FileType] [varchar](100) NULL
		,[Title] [varchar](max) NULL
		,[EventColumnValue] [datetime] NULL
		,[ComplianceTag] [varchar](100) NULL
		,[ComplianceTagId] [varchar](100) NULL
		,[ComplainceAssetId] [varchar](100) NULL
		,[DocUrl] [varchar](max) NOT NULL
		,[LibraryName] [varchar](100) NOT NULL
		,[CreatedOn] [datetime] NOT NULL
		,[ModifiedOn] [datetime] NULL
		,[DocumentName] VARCHAR(200) NOT NULL
		,[ErrorLog] VARCHAR(100) NULL
		)

	INSERT @result
	SELECT ROW_NUMBER() OVER (
			ORDER BY contentlogid DESC
			) AS rownum
		,ContentLogId
		,ContentType
		,DocId
		,LastModifiedTime
		,FileType
		,Title
		,EventColumnValue
		,ComplianceTag
		,ComplianceTagId
		,ComplainceAssetId
		,DocUrl
		,LibraryName
		,cl.CreatedOn
		,cl.ModifiedOn
		,cl.[DocumentName]
		,CASE 
			WHEN cl.EventColumnValue IS NULL
				AND (cl.ComplianceTagId IS NULL or cl.ComplianceTagId='')
				THEN 'Both Event date and Compliance Tag is missing'
			WHEN cl.EventColumnValue IS NULL
				THEN 'Event date is missing'
			WHEN (cl.ComplianceTagId IS NULL or cl.ComplianceTagId='')
				THEN 'Compliance Tag is missing'
			ELSE ''
			END
	FROM [dbo].[ContentLog] cl WITH (NOLOCK)
	INNER JOIN [dbo].[Organization] o WITH (NOLOCK) ON cl.OrganizationMasterId = o.OrganizationMasterId
	WHERE o.OrganizationRootUrl = @OrganizationUrl
		AND (
			(
				(
					cl.EventColumnValue IS NULL
					OR cl.ComplianceTagId IS NULL
					)
				AND @CountError = 1
				)
			OR @CountError = 0
			)
		AND (
			CONVERT(NVARCHAR(10), cl.createdon, 120) >= CONVERT(NVARCHAR(10), @StartDate, 120)
			OR @StartDate IS NULL
			)
		AND CONVERT(NVARCHAR(10), cl.createdon, 120) <= @EndDate

	SELECT *
	FROM @result
	WHERE RowNumber > @lFirstRec
		AND RowNumber < @lLastRec
	ORDER BY RowNumber ASC
END

GO
