CREATE DATABASE BinTracking;

USE [BinTracking]
GO
/****** Object:  Table [dbo].[AppMenus]    Script Date: 09-06-2025 11:46:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AppMenus](
	[MenuType] [int] NOT NULL,
	[MenuId] [int] NOT NULL,
	[MenuDesc] [varchar](100) NOT NULL,
	[PgAction] [char](5) NOT NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_AppMenus] PRIMARY KEY CLUSTERED 
(
	[MenuId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_AppMenus] UNIQUE NONCLUSTERED 
(
	[MenuDesc] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 09-06-2025 11:46:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[CustId] [bigint] NOT NULL,
	[CustCode] [varchar](40) NOT NULL,
	[CustDesc] [varchar](100) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[ContactNo] [varchar](40) NOT NULL,
	[Status] [int] NOT NULL,
	[CreatedOn] [date] NOT NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[CustId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_Customers] UNIQUE NONCLUSTERED 
(
	[CustCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_Customers_1] UNIQUE NONCLUSTERED 
(
	[CustDesc] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DBLog]    Script Date: 09-06-2025 11:46:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DBLog](
	[PgIdx] [int] NOT NULL,
	[EmpId] [bigint] NOT NULL,
	[ProcName] [varchar](100) NOT NULL,
	[SlNo] [int] NOT NULL,
	[LogData] [varchar](max) NOT NULL,
	[CreatedOn] [datetime] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 09-06-2025 11:46:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[EmpId] [bigint] NOT NULL,
	[EmpCode] [varchar](20) NOT NULL,
	[EmpName] [varchar](100) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[Mobile] [varchar](10) NOT NULL,
	[AppPwd] [varchar](20) NOT NULL,
	[SessionId] [varchar](100) NOT NULL,
	[Status] [int] NOT NULL,
	[CreatedOn] [date] NOT NULL,
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
(
	[EmpId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_Employees] UNIQUE NONCLUSTERED 
(
	[EmpCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_Employees_1] UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_Employees_2] UNIQUE NONCLUSTERED 
(
	[Mobile] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MenuMapping]    Script Date: 09-06-2025 11:46:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MenuMapping](
	[EmpId] [bigint] NOT NULL,
	[MenuId] [int] NOT NULL,
	[PgAction] [char](5) NOT NULL,
 CONSTRAINT [PK_MenuMapping] PRIMARY KEY CLUSTERED 
(
	[EmpId] ASC,
	[MenuId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 09-06-2025 11:46:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProductId] [int] NOT NULL,
	[ProductCode] [varchar](40) NOT NULL,
	[ProductDesc] [varchar](100) NOT NULL,
	[Stock] [int] NOT NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_Products] UNIQUE NONCLUSTERED 
(
	[ProductCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_Products_1] UNIQUE NONCLUSTERED 
(
	[ProductDesc] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductStock]    Script Date: 09-06-2025 11:46:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductStock](
	[ProductId] [int] NOT NULL,
	[CustId] [bigint] NOT NULL,
	[Barcode] [varchar](20) NOT NULL,
	[SlNo] [int] NOT NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_ProductStock] PRIMARY KEY CLUSTERED 
(
	[Barcode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_ProductStock] UNIQUE NONCLUSTERED 
(
	[Barcode] ASC,
	[SlNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reasons]    Script Date: 09-06-2025 11:46:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reasons](
	[ReasonId] [int] NOT NULL,
	[ReasonDesc] [varchar](100) NOT NULL,
	[Status] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ReasonId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[ReasonDesc] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Shifts]    Script Date: 09-06-2025 11:46:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Shifts](
	[ShiftId] [int] NOT NULL,
	[ShiftCode] [varchar](20) NOT NULL,
	[ShiftDesc] [varchar](50) NOT NULL,
	[FromTime] [time](7) NOT NULL,
	[ToTime] [time](7) NOT NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_Shifts] PRIMARY KEY CLUSTERED 
(
	[ShiftId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_Shifts] UNIQUE NONCLUSTERED 
(
	[ShiftCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_Shifts_1] UNIQUE NONCLUSTERED 
(
	[ShiftDesc] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StkAdjHeader]    Script Date: 09-06-2025 11:46:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StkAdjHeader](
	[StkAdjId] [bigint] NOT NULL,
	[EmpId] [bigint] NOT NULL,
	[CustId] [bigint] NOT NULL,
	[ProductId] [int] NOT NULL,
	[StkAdjType] [int] NOT NULL,
	[AdjustQty] [int] NOT NULL,
	[Stock] [int] NOT NULL,
	[ReasonId] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[CreatedOn] [date] NOT NULL,
 CONSTRAINT [PK_StkAdjHeader] PRIMARY KEY CLUSTERED 
(
	[StkAdjId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_StkAdjHeader] UNIQUE NONCLUSTERED 
(
	[ProductId] ASC,
	[Stock] ASC,
	[AdjustQty] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StkAdjLines]    Script Date: 09-06-2025 11:46:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StkAdjLines](
	[StkAdjId] [bigint] NOT NULL,
	[SlNo] [int] NOT NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_StkAdjLines] PRIMARY KEY CLUSTERED 
(
	[StkAdjId] ASC,
	[SlNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StockAdjust]    Script Date: 09-06-2025 11:46:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StockAdjust](
	[StkAdjId] [bigint] NOT NULL,
	[EmpId] [bigint] NOT NULL,
	[CustId] [bigint] NOT NULL,
	[ProductId] [int] NOT NULL,
	[StkAdjType] [int] NOT NULL,
	[AdjustQty] [int] NOT NULL,
	[Stock] [int] NOT NULL,
	[SlNos] [varchar](max) NOT NULL,
	[ReasonId] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[CreatedOn] [date] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[StkAdjId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[ProductId] ASC,
	[Stock] ASC,
	[AdjustQty] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StockInward]    Script Date: 09-06-2025 11:46:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StockInward](
	[SDate] [datetime] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[FromSlNo] [int] NOT NULL,
	[ToSlNo] [int] NOT NULL,
	[EmpId] [bigint] NOT NULL,
	[Status] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TmpTable]    Script Date: 09-06-2025 11:46:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TmpTable](
	[Idx] [bigint] NOT NULL,
	[EmpId] [bigint] NOT NULL,
	[Flag] [int] NOT NULL,
	[IData1] [bigint] NOT NULL,
	[IData2] [bigint] NOT NULL,
	[IData3] [bigint] NOT NULL,
	[IData4] [bigint] NOT NULL,
	[IData5] [bigint] NOT NULL,
	[IData6] [bigint] NOT NULL,
	[IData7] [bigint] NOT NULL,
	[IData8] [bigint] NOT NULL,
	[IData9] [bigint] NOT NULL,
	[IData10] [bigint] NOT NULL,
	[IData11] [bigint] NOT NULL,
	[IData12] [bigint] NOT NULL,
	[IData13] [bigint] NOT NULL,
	[IData14] [bigint] NOT NULL,
	[IData15] [bigint] NOT NULL,
	[VData1] [nvarchar](max) NOT NULL,
	[VData2] [nvarchar](max) NOT NULL,
	[VData3] [nvarchar](max) NOT NULL,
	[VData4] [nvarchar](max) NOT NULL,
	[VData5] [nvarchar](max) NOT NULL,
	[VData6] [nvarchar](max) NOT NULL,
	[VData7] [nvarchar](max) NOT NULL,
	[VData8] [nvarchar](max) NOT NULL,
	[VData9] [nvarchar](max) NOT NULL,
	[VData10] [nvarchar](max) NOT NULL,
	[VData11] [nvarchar](max) NOT NULL,
	[VData12] [nvarchar](max) NOT NULL,
	[VData13] [nvarchar](max) NOT NULL,
	[VData14] [nvarchar](max) NOT NULL,
	[VData15] [nvarchar](max) NOT NULL,
	[DData1] [decimal](12, 3) NOT NULL,
	[DData2] [decimal](12, 3) NOT NULL,
	[DData3] [decimal](12, 3) NOT NULL,
	[DData4] [decimal](12, 3) NOT NULL,
	[DData5] [decimal](12, 3) NOT NULL,
	[DTData1] [datetime] NOT NULL,
	[DTData2] [datetime] NOT NULL,
	[DTData3] [datetime] NOT NULL,
	[DTData4] [datetime] NOT NULL,
	[DTData5] [datetime] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TranHeader]    Script Date: 09-06-2025 11:46:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TranHeader](
	[TranId] [bigint] NOT NULL,
	[TranDate] [datetime] NOT NULL,
	[EmpId] [bigint] NOT NULL,
	[TransporterId] [int] NOT NULL,
	[VehicleNo] [varchar](20) NOT NULL,
	[TranType] [int] NOT NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_TranHeader] PRIMARY KEY CLUSTERED 
(
	[TranId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TranLines]    Script Date: 09-06-2025 11:46:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TranLines](
	[TranId] [bigint] NOT NULL,
	[CustId] [bigint] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Barcode] [varchar](20) NOT NULL,
	[SlNo] [int] NOT NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_TranLines] PRIMARY KEY CLUSTERED 
(
	[TranId] ASC,
	[CustId] ASC,
	[ProductId] ASC,
	[Barcode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transporters]    Script Date: 09-06-2025 11:46:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transporters](
	[TransporterId] [int] NOT NULL,
	[TransporterCode] [varchar](40) NOT NULL,
	[TransporterDesc] [varchar](100) NOT NULL,
	[ContactNo] [varchar](40) NOT NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_Transporters] PRIMARY KEY CLUSTERED 
(
	[TransporterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_Transporters] UNIQUE NONCLUSTERED 
(
	[TransporterCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_Transporters_1] UNIQUE NONCLUSTERED 
(
	[TransporterDesc] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TransporterVehicles]    Script Date: 09-06-2025 11:46:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransporterVehicles](
	[TransporterId] [int] NOT NULL,
	[VehicleNo] [varchar](20) NOT NULL,
	[VehicleType] [varchar](40) NOT NULL,
	[Remarks] [varchar](200) NOT NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_TransporterVehicles_1] PRIMARY KEY CLUSTERED 
(
	[TransporterId] ASC,
	[VehicleNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AppMenus] ADD  CONSTRAINT [DF_AppMenus_MenuType]  DEFAULT ((0)) FOR [MenuType]
GO
ALTER TABLE [dbo].[AppMenus] ADD  CONSTRAINT [DF_AppMenus_MenuId]  DEFAULT ((0)) FOR [MenuId]
GO
ALTER TABLE [dbo].[AppMenus] ADD  CONSTRAINT [DF_AppMenus_MenuDesc]  DEFAULT ('') FOR [MenuDesc]
GO
ALTER TABLE [dbo].[AppMenus] ADD  CONSTRAINT [DF_AppMenus_PgAction]  DEFAULT ('00000') FOR [PgAction]
GO
ALTER TABLE [dbo].[AppMenus] ADD  CONSTRAINT [DF_AppMenus_Status]  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[Customers] ADD  CONSTRAINT [DF_Customers_CustId]  DEFAULT ((0)) FOR [CustId]
GO
ALTER TABLE [dbo].[Customers] ADD  CONSTRAINT [DF_Customers_CustCode]  DEFAULT ('') FOR [CustCode]
GO
ALTER TABLE [dbo].[Customers] ADD  CONSTRAINT [DF_Customers_CustDesc]  DEFAULT ('') FOR [CustDesc]
GO
ALTER TABLE [dbo].[Customers] ADD  CONSTRAINT [DF_Customers_Email]  DEFAULT ('') FOR [Email]
GO
ALTER TABLE [dbo].[Customers] ADD  CONSTRAINT [DF_Table_1_ContactNumber]  DEFAULT ('') FOR [ContactNo]
GO
ALTER TABLE [dbo].[Customers] ADD  CONSTRAINT [DF_Customers_Status]  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[Customers] ADD  CONSTRAINT [DF_Customers_CreatedOn]  DEFAULT ('') FOR [CreatedOn]
GO
ALTER TABLE [dbo].[DBLog] ADD  DEFAULT ((0)) FOR [PgIdx]
GO
ALTER TABLE [dbo].[DBLog] ADD  DEFAULT ((0)) FOR [EmpId]
GO
ALTER TABLE [dbo].[DBLog] ADD  DEFAULT ('') FOR [ProcName]
GO
ALTER TABLE [dbo].[DBLog] ADD  DEFAULT ((0)) FOR [SlNo]
GO
ALTER TABLE [dbo].[DBLog] ADD  DEFAULT ('') FOR [LogData]
GO
ALTER TABLE [dbo].[DBLog] ADD  DEFAULT (getdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[Employees] ADD  CONSTRAINT [DF_Employees_EmpId]  DEFAULT ((0)) FOR [EmpId]
GO
ALTER TABLE [dbo].[Employees] ADD  CONSTRAINT [DF_Employees_EmpCode]  DEFAULT ('') FOR [EmpCode]
GO
ALTER TABLE [dbo].[Employees] ADD  CONSTRAINT [DF_Employees_EmpName]  DEFAULT ('') FOR [EmpName]
GO
ALTER TABLE [dbo].[Employees] ADD  CONSTRAINT [DF_Employees_Mobile]  DEFAULT ('') FOR [Mobile]
GO
ALTER TABLE [dbo].[Employees] ADD  CONSTRAINT [DF_Employees_AppPwd]  DEFAULT ('') FOR [AppPwd]
GO
ALTER TABLE [dbo].[Employees] ADD  CONSTRAINT [DF_Employees_SessionId]  DEFAULT ('') FOR [SessionId]
GO
ALTER TABLE [dbo].[Employees] ADD  CONSTRAINT [DF_Employees_Status]  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[Employees] ADD  CONSTRAINT [DF_Employees_CreatedOn]  DEFAULT ('') FOR [CreatedOn]
GO
ALTER TABLE [dbo].[MenuMapping] ADD  CONSTRAINT [DF_MenuMapping_EmpId]  DEFAULT ((0)) FOR [EmpId]
GO
ALTER TABLE [dbo].[MenuMapping] ADD  CONSTRAINT [DF_MenuMapping_MenuId]  DEFAULT ((0)) FOR [MenuId]
GO
ALTER TABLE [dbo].[MenuMapping] ADD  CONSTRAINT [DF_MenuMapping_PgAction]  DEFAULT ('00000') FOR [PgAction]
GO
ALTER TABLE [dbo].[Products] ADD  CONSTRAINT [DF_Products_ProductId]  DEFAULT ((0)) FOR [ProductId]
GO
ALTER TABLE [dbo].[Products] ADD  CONSTRAINT [DF_Products_ProductCode]  DEFAULT ('') FOR [ProductCode]
GO
ALTER TABLE [dbo].[Products] ADD  CONSTRAINT [DF_Products_ProductDesc]  DEFAULT ('') FOR [ProductDesc]
GO
ALTER TABLE [dbo].[Products] ADD  CONSTRAINT [DF_Products_Stock]  DEFAULT ((0)) FOR [Stock]
GO
ALTER TABLE [dbo].[Products] ADD  CONSTRAINT [DF_Products_Status]  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[ProductStock] ADD  CONSTRAINT [DF_ProductStock_ProductId]  DEFAULT ((0)) FOR [ProductId]
GO
ALTER TABLE [dbo].[ProductStock] ADD  CONSTRAINT [DF_ProductStock_CustId]  DEFAULT ((0)) FOR [CustId]
GO
ALTER TABLE [dbo].[ProductStock] ADD  CONSTRAINT [DF_ProductStock_Barcode]  DEFAULT ('') FOR [Barcode]
GO
ALTER TABLE [dbo].[ProductStock] ADD  CONSTRAINT [DF_ProductStock_SlNo]  DEFAULT ((0)) FOR [SlNo]
GO
ALTER TABLE [dbo].[ProductStock] ADD  CONSTRAINT [DF_ProductStock_Status]  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[Reasons] ADD  DEFAULT ((0)) FOR [ReasonId]
GO
ALTER TABLE [dbo].[Reasons] ADD  DEFAULT ('') FOR [ReasonDesc]
GO
ALTER TABLE [dbo].[Reasons] ADD  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[Shifts] ADD  CONSTRAINT [DF_Shifts_ShiftId]  DEFAULT ((0)) FOR [ShiftId]
GO
ALTER TABLE [dbo].[Shifts] ADD  CONSTRAINT [DF_Shifts_ShiftCode]  DEFAULT ('') FOR [ShiftCode]
GO
ALTER TABLE [dbo].[Shifts] ADD  CONSTRAINT [DF_Shifts_ShiftDesc]  DEFAULT ('') FOR [ShiftDesc]
GO
ALTER TABLE [dbo].[Shifts] ADD  CONSTRAINT [DF_Shifts_FromTime]  DEFAULT ('') FOR [FromTime]
GO
ALTER TABLE [dbo].[Shifts] ADD  CONSTRAINT [DF_Shifts_ToTime]  DEFAULT ('') FOR [ToTime]
GO
ALTER TABLE [dbo].[Shifts] ADD  CONSTRAINT [DF_Shifts_Status]  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[StkAdjHeader] ADD  CONSTRAINT [DF_StkAdjHeader_StkAdjId]  DEFAULT ((0)) FOR [StkAdjId]
GO
ALTER TABLE [dbo].[StkAdjHeader] ADD  CONSTRAINT [DF_StkAdjHeader_EmpId]  DEFAULT ((0)) FOR [EmpId]
GO
ALTER TABLE [dbo].[StkAdjHeader] ADD  CONSTRAINT [DF_StkAdjHeader_CustId]  DEFAULT ((0)) FOR [CustId]
GO
ALTER TABLE [dbo].[StkAdjHeader] ADD  CONSTRAINT [DF_StkAdjHeader_ProductId]  DEFAULT ((0)) FOR [ProductId]
GO
ALTER TABLE [dbo].[StkAdjHeader] ADD  CONSTRAINT [DF_StkAdjHeader_StkAdjType]  DEFAULT ((0)) FOR [StkAdjType]
GO
ALTER TABLE [dbo].[StkAdjHeader] ADD  CONSTRAINT [DF_StkAdjHeader_AdjustQty]  DEFAULT ((0)) FOR [AdjustQty]
GO
ALTER TABLE [dbo].[StkAdjHeader] ADD  CONSTRAINT [DF_StkAdjHeader_Stock]  DEFAULT ((0)) FOR [Stock]
GO
ALTER TABLE [dbo].[StkAdjHeader] ADD  CONSTRAINT [DF_StkAdjHeader_ReasonId]  DEFAULT ((0)) FOR [ReasonId]
GO
ALTER TABLE [dbo].[StkAdjHeader] ADD  CONSTRAINT [DF_StkAdjHeader_Status]  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[StkAdjHeader] ADD  CONSTRAINT [DF_StkAdjHeader_CreatedOn]  DEFAULT ('') FOR [CreatedOn]
GO
ALTER TABLE [dbo].[StkAdjLines] ADD  CONSTRAINT [DF_StkAdjLines_StkAdjId]  DEFAULT ((0)) FOR [StkAdjId]
GO
ALTER TABLE [dbo].[StkAdjLines] ADD  CONSTRAINT [DF_StkAdjLines_SlNo]  DEFAULT ((0)) FOR [SlNo]
GO
ALTER TABLE [dbo].[StkAdjLines] ADD  CONSTRAINT [DF_StkAdjLines_Status]  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[StockAdjust] ADD  DEFAULT ((0)) FOR [StkAdjId]
GO
ALTER TABLE [dbo].[StockAdjust] ADD  DEFAULT ((0)) FOR [EmpId]
GO
ALTER TABLE [dbo].[StockAdjust] ADD  DEFAULT ((0)) FOR [CustId]
GO
ALTER TABLE [dbo].[StockAdjust] ADD  DEFAULT ((0)) FOR [ProductId]
GO
ALTER TABLE [dbo].[StockAdjust] ADD  DEFAULT ((0)) FOR [StkAdjType]
GO
ALTER TABLE [dbo].[StockAdjust] ADD  DEFAULT ((0)) FOR [AdjustQty]
GO
ALTER TABLE [dbo].[StockAdjust] ADD  DEFAULT ((0)) FOR [Stock]
GO
ALTER TABLE [dbo].[StockAdjust] ADD  DEFAULT ('') FOR [SlNos]
GO
ALTER TABLE [dbo].[StockAdjust] ADD  DEFAULT ((0)) FOR [ReasonId]
GO
ALTER TABLE [dbo].[StockAdjust] ADD  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[StockAdjust] ADD  DEFAULT (getdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[StockInward] ADD  CONSTRAINT [DF_StockInward_SDate]  DEFAULT ('') FOR [SDate]
GO
ALTER TABLE [dbo].[StockInward] ADD  CONSTRAINT [DF_StockInward_ProductId]  DEFAULT ((0)) FOR [ProductId]
GO
ALTER TABLE [dbo].[StockInward] ADD  CONSTRAINT [DF_StockInward_Quantity]  DEFAULT ((0)) FOR [Quantity]
GO
ALTER TABLE [dbo].[StockInward] ADD  CONSTRAINT [DF_StockInward_FromSlNo]  DEFAULT ((0)) FOR [FromSlNo]
GO
ALTER TABLE [dbo].[StockInward] ADD  CONSTRAINT [DF_StockInward_ToSlNo]  DEFAULT ((0)) FOR [ToSlNo]
GO
ALTER TABLE [dbo].[StockInward] ADD  CONSTRAINT [DF_StockInward_EmpId]  DEFAULT ((0)) FOR [EmpId]
GO
ALTER TABLE [dbo].[StockInward] ADD  CONSTRAINT [DF_StockInward_Status]  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[TmpTable] ADD  CONSTRAINT [DF_TmpTable_Idx]  DEFAULT ((0)) FOR [Idx]
GO
ALTER TABLE [dbo].[TmpTable] ADD  CONSTRAINT [DF_TmpTable_EmpId]  DEFAULT ((0)) FOR [EmpId]
GO
ALTER TABLE [dbo].[TmpTable] ADD  CONSTRAINT [DF_TmpTable_Flag]  DEFAULT ((0)) FOR [Flag]
GO
ALTER TABLE [dbo].[TmpTable] ADD  CONSTRAINT [DF_TmpTable_IData1]  DEFAULT ((0)) FOR [IData1]
GO
ALTER TABLE [dbo].[TmpTable] ADD  CONSTRAINT [DF_TmpTable_IData2]  DEFAULT ((0)) FOR [IData2]
GO
ALTER TABLE [dbo].[TmpTable] ADD  CONSTRAINT [DF_TmpTable_IData3]  DEFAULT ((0)) FOR [IData3]
GO
ALTER TABLE [dbo].[TmpTable] ADD  CONSTRAINT [DF_TmpTable_IData4]  DEFAULT ((0)) FOR [IData4]
GO
ALTER TABLE [dbo].[TmpTable] ADD  CONSTRAINT [DF_TmpTable_IData5]  DEFAULT ((0)) FOR [IData5]
GO
ALTER TABLE [dbo].[TmpTable] ADD  CONSTRAINT [DF_TmpTable_IData6]  DEFAULT ((0)) FOR [IData6]
GO
ALTER TABLE [dbo].[TmpTable] ADD  CONSTRAINT [DF_TmpTable_IData7]  DEFAULT ((0)) FOR [IData7]
GO
ALTER TABLE [dbo].[TmpTable] ADD  CONSTRAINT [DF_TmpTable_IData8]  DEFAULT ((0)) FOR [IData8]
GO
ALTER TABLE [dbo].[TmpTable] ADD  CONSTRAINT [DF_TmpTable_IData9]  DEFAULT ((0)) FOR [IData9]
GO
ALTER TABLE [dbo].[TmpTable] ADD  CONSTRAINT [DF_TmpTable_IData10]  DEFAULT ((0)) FOR [IData10]
GO
ALTER TABLE [dbo].[TmpTable] ADD  CONSTRAINT [DF_TmpTable_IData11]  DEFAULT ((0)) FOR [IData11]
GO
ALTER TABLE [dbo].[TmpTable] ADD  CONSTRAINT [DF_TmpTable_IData12]  DEFAULT ((0)) FOR [IData12]
GO
ALTER TABLE [dbo].[TmpTable] ADD  CONSTRAINT [DF_TmpTable_IData13]  DEFAULT ((0)) FOR [IData13]
GO
ALTER TABLE [dbo].[TmpTable] ADD  CONSTRAINT [DF_TmpTable_IData14]  DEFAULT ((0)) FOR [IData14]
GO
ALTER TABLE [dbo].[TmpTable] ADD  CONSTRAINT [DF_TmpTable_IData15]  DEFAULT ((0)) FOR [IData15]
GO
ALTER TABLE [dbo].[TmpTable] ADD  CONSTRAINT [DF_TmpTable_VData1]  DEFAULT ('') FOR [VData1]
GO
ALTER TABLE [dbo].[TmpTable] ADD  CONSTRAINT [DF_TmpTable_VData2]  DEFAULT ('') FOR [VData2]
GO
ALTER TABLE [dbo].[TmpTable] ADD  CONSTRAINT [DF_TmpTable_VData3]  DEFAULT ('') FOR [VData3]
GO
ALTER TABLE [dbo].[TmpTable] ADD  CONSTRAINT [DF_TmpTable_VData4]  DEFAULT ('') FOR [VData4]
GO
ALTER TABLE [dbo].[TmpTable] ADD  CONSTRAINT [DF_TmpTable_VData5]  DEFAULT ('') FOR [VData5]
GO
ALTER TABLE [dbo].[TmpTable] ADD  CONSTRAINT [DF_TmpTable_VData6]  DEFAULT ('') FOR [VData6]
GO
ALTER TABLE [dbo].[TmpTable] ADD  CONSTRAINT [DF_TmpTable_VData7]  DEFAULT ('') FOR [VData7]
GO
ALTER TABLE [dbo].[TmpTable] ADD  CONSTRAINT [DF_TmpTable_VData8]  DEFAULT ('') FOR [VData8]
GO
ALTER TABLE [dbo].[TmpTable] ADD  CONSTRAINT [DF_TmpTable_VData9]  DEFAULT ('') FOR [VData9]
GO
ALTER TABLE [dbo].[TmpTable] ADD  CONSTRAINT [DF_TmpTable_VData10]  DEFAULT ('') FOR [VData10]
GO
ALTER TABLE [dbo].[TmpTable] ADD  CONSTRAINT [DF_TmpTable_VData11]  DEFAULT ('') FOR [VData11]
GO
ALTER TABLE [dbo].[TmpTable] ADD  CONSTRAINT [DF_TmpTable_VData12]  DEFAULT ('') FOR [VData12]
GO
ALTER TABLE [dbo].[TmpTable] ADD  CONSTRAINT [DF_TmpTable_VData13]  DEFAULT ('') FOR [VData13]
GO
ALTER TABLE [dbo].[TmpTable] ADD  CONSTRAINT [DF_TmpTable_VData14]  DEFAULT ('') FOR [VData14]
GO
ALTER TABLE [dbo].[TmpTable] ADD  CONSTRAINT [DF_TmpTable_VData15]  DEFAULT ('') FOR [VData15]
GO
ALTER TABLE [dbo].[TmpTable] ADD  CONSTRAINT [DF_TmpTable_DData1]  DEFAULT ((0)) FOR [DData1]
GO
ALTER TABLE [dbo].[TmpTable] ADD  CONSTRAINT [DF_TmpTable_DData2]  DEFAULT ((0)) FOR [DData2]
GO
ALTER TABLE [dbo].[TmpTable] ADD  CONSTRAINT [DF_TmpTable_DData3]  DEFAULT ((0)) FOR [DData3]
GO
ALTER TABLE [dbo].[TmpTable] ADD  CONSTRAINT [DF_TmpTable_DData4]  DEFAULT ((0)) FOR [DData4]
GO
ALTER TABLE [dbo].[TmpTable] ADD  CONSTRAINT [DF_TmpTable_DData5]  DEFAULT ((0)) FOR [DData5]
GO
ALTER TABLE [dbo].[TmpTable] ADD  CONSTRAINT [DF_TmpTable_DTData1]  DEFAULT (getdate()) FOR [DTData1]
GO
ALTER TABLE [dbo].[TmpTable] ADD  CONSTRAINT [DF_TmpTable_DTData2]  DEFAULT (getdate()) FOR [DTData2]
GO
ALTER TABLE [dbo].[TmpTable] ADD  CONSTRAINT [DF_TmpTable_DTData3]  DEFAULT (getdate()) FOR [DTData3]
GO
ALTER TABLE [dbo].[TmpTable] ADD  CONSTRAINT [DF_TmpTable_DTData4]  DEFAULT (getdate()) FOR [DTData4]
GO
ALTER TABLE [dbo].[TmpTable] ADD  CONSTRAINT [DF_TmpTable_DTData5]  DEFAULT (getdate()) FOR [DTData5]
GO
ALTER TABLE [dbo].[TranHeader] ADD  CONSTRAINT [DF_TranHeader_TranId]  DEFAULT ((0)) FOR [TranId]
GO
ALTER TABLE [dbo].[TranHeader] ADD  CONSTRAINT [DF_TranHeader_TranDate]  DEFAULT ('') FOR [TranDate]
GO
ALTER TABLE [dbo].[TranHeader] ADD  CONSTRAINT [DF_TranHeader_EmpId]  DEFAULT ((0)) FOR [EmpId]
GO
ALTER TABLE [dbo].[TranHeader] ADD  CONSTRAINT [DF_TranHeader_TransporterId]  DEFAULT ((0)) FOR [TransporterId]
GO
ALTER TABLE [dbo].[TranHeader] ADD  CONSTRAINT [DF_TranHeader_VehicleNo]  DEFAULT ('') FOR [VehicleNo]
GO
ALTER TABLE [dbo].[TranHeader] ADD  CONSTRAINT [DF_TranHeader_TranType]  DEFAULT ((0)) FOR [TranType]
GO
ALTER TABLE [dbo].[TranHeader] ADD  CONSTRAINT [DF_TranHeader_Status]  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[TranLines] ADD  CONSTRAINT [DF_TranLines_TranId]  DEFAULT ((0)) FOR [TranId]
GO
ALTER TABLE [dbo].[TranLines] ADD  CONSTRAINT [DF_TranLines_CustId]  DEFAULT ((0)) FOR [CustId]
GO
ALTER TABLE [dbo].[TranLines] ADD  CONSTRAINT [DF_TranLines_ProductId]  DEFAULT ((0)) FOR [ProductId]
GO
ALTER TABLE [dbo].[TranLines] ADD  CONSTRAINT [DF_TranLines_Barcode]  DEFAULT ('') FOR [Barcode]
GO
ALTER TABLE [dbo].[TranLines] ADD  CONSTRAINT [DF_TranLines_SlNo]  DEFAULT ((0)) FOR [SlNo]
GO
ALTER TABLE [dbo].[TranLines] ADD  CONSTRAINT [DF_TranLines_Status]  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[Transporters] ADD  CONSTRAINT [DF_Transporters_TransporterId]  DEFAULT ((0)) FOR [TransporterId]
GO
ALTER TABLE [dbo].[Transporters] ADD  CONSTRAINT [DF_Transporters_TransporterCode]  DEFAULT ('') FOR [TransporterCode]
GO
ALTER TABLE [dbo].[Transporters] ADD  CONSTRAINT [DF_Transporters_TransporterDesc]  DEFAULT ('') FOR [TransporterDesc]
GO
ALTER TABLE [dbo].[Transporters] ADD  CONSTRAINT [DF_Transporters_ContactNo]  DEFAULT ('') FOR [ContactNo]
GO
ALTER TABLE [dbo].[Transporters] ADD  CONSTRAINT [DF_Transporters_Status]  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[TransporterVehicles] ADD  CONSTRAINT [DF_TransporterVehicles_TransporterId]  DEFAULT ((0)) FOR [TransporterId]
GO
ALTER TABLE [dbo].[TransporterVehicles] ADD  CONSTRAINT [DF_TransporterVehicles_VehicleNo]  DEFAULT ('') FOR [VehicleNo]
GO
ALTER TABLE [dbo].[TransporterVehicles] ADD  CONSTRAINT [DF_TransporterVehicles_VehicleType]  DEFAULT ('') FOR [VehicleType]
GO
ALTER TABLE [dbo].[TransporterVehicles] ADD  CONSTRAINT [DF_TransporterVehicles_Remarks]  DEFAULT ('') FOR [Remarks]
GO
ALTER TABLE [dbo].[TransporterVehicles] ADD  CONSTRAINT [DF_TransporterVehicles_Status]  DEFAULT ((1)) FOR [Status]
GO
/****** Object:  StoredProcedure [dbo].[Customer_Delete]    Script Date: 09-06-2025 11:46:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[Customer_Delete](@PgIdx int, @CustId bigint, @ModifiedBy bigint)
As
Begin
	Declare @CustomerName as Varchar(100);
	Declare @Cnt as int;
	
	Select @CustomerName = CustDesc from Customers Where CustId = @CustId;

	SELECT @Cnt = CASE WHEN EXISTS (SELECT 1 FROM DBLog WHERE PgIdx = @PgIdx) THEN (SELECT MAX(SlNo) + 1 FROM DBLog WHERE PgIdx = @PgIdx)
		ELSE 1 END;
	Insert into DBLog (PgIdx, EmpId, ProcName, SlNo, LogData, CreatedOn) Values(@PgIdx,@Modifiedby,'Product_Delete',@Cnt,
				'Customer : ' + @CustomerName +' Deleted', GETDATE());
	Update Customers Set Status = 0 Where CustId = @CustId;
End
GO
/****** Object:  StoredProcedure [dbo].[Customer_Grid]    Script Date: 09-06-2025 11:46:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[Customer_Grid](@Status int)
As
Begin
	Select CustId, CustCode, CustDesc, Email,ContactNo, [Status] from Customers Where [Status] = @Status;
End
GO
/****** Object:  StoredProcedure [dbo].[Customer_Save]    Script Date: 09-06-2025 11:46:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Customer_Save] @PgIdx int, @CustId bigint, @CustCode VARCHAR(20),@CustDesc VARCHAR(100),
@Email Varchar(100),@Contact Varchar(10),@Status Int, @ModifiedBy Bigint, @ErrorMsg VARCHAR(1000) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
	declare @Cnt as int;
	Declare @GenId as Varchar(10) = ''; 
	Declare @Old_Code as Varchar(20) = ''; 
	Declare @Old_Name as Varchar(100) = '';	
	Declare @Old_Email as Varchar(100) = '';
	Declare @Old_Contact as Varchar(10) = '';	
	Declare @Old_Status as Int = 0;	
	Declare @Log_Msg as Varchar(1000) = '';

	BEGIN TRY
	IF(@CustId = 0)
		SELECT @Cnt = count(1) FROM Customers WHERE CustCode = @CustCode;
	ELSE
		SELECT @Cnt = count(1) FROM Customers WHERE CustCode = @CustCode and CustId != @CustId;
	IF (@Cnt != 0)
		BEGIN
			SET @ErrorMsg = 'Custoemr Code already exists'
			RAISERROR (@ErrorMsg, 16, 1)
		END

	IF(@CustId = 0)
		SELECT @Cnt = count(1) FROM Customers WHERE CustDesc = @CustDesc;
	ELSE
		SELECT @Cnt = count(1) FROM Customers WHERE CustDesc = @CustDesc and CustId != @CustId;
	IF (@Cnt != 0)
		BEGIN
			SET @ErrorMsg = 'Customer Name already exists'
			RAISERROR (@ErrorMsg, 16, 1)
		END

	IF(@CustId = 0)
		SELECT @Cnt = count(1) FROM Customers WHERE Email = @Email;
	ELSE
		SELECT @Cnt = count(1) FROM Customers WHERE Email = @Email and CustId != @CustId;
	IF (@Cnt != 0)
		BEGIN
			SET @ErrorMsg = 'Customer Email already exists'
			RAISERROR (@ErrorMsg, 16, 1)
		END
    set @Cnt = 0;
	SELECT @Cnt = CASE WHEN EXISTS (SELECT 1 FROM DBLog WHERE PgIdx = @PgIdx) THEN (SELECT MAX(SlNo) + 1 FROM DBLog WHERE PgIdx = @PgIdx)
		ELSE 1 END;
	IF (@CustId = 0)
	BEGIN
		SELECT @GenId = CONCAT('2', FORMAT(GETDATE(), 'yyMMdd'), FORMAT(COUNT(1) + 1, '000'))
		FROM Customers 
		WHERE CAST(CreatedOn AS DATE) = CAST(GETDATE() AS DATE);

		IF (@GenId IS NULL OR LTRIM(RTRIM(@GenId)) = '')
		BEGIN
			SET @ErrorMsg = 'Customer ID Generate Failed. Retry Again.'
			RAISERROR (@ErrorMsg, 16, 1)
			RETURN;
		END

		INSERT INTO Customers(CustId, CustCode, CustDesc, Email, ContactNo, Status, CreatedOn)
		VALUES (@GenId, @CustCode, @CustDesc, @Email, @Contact, @Status, GETDATE());

		INSERT INTO DBLog (PgIdx, EmpId, ProcName, SlNo, LogData, CreatedOn) 
		VALUES (@PgIdx, @Modifiedby, 'Customer_save', @Cnt, 'New Customer : ' + @CustDesc + ' Created', GETDATE());
	END
	ELSE
		BEGIN
			Select @Old_Code = CustCode, @Old_Name = CustDesc, @Old_Email = Email, @Old_Contact = ContactNo, @Old_Status = [Status] 
				from Customers Where CustId = @CustId;
				If(@Old_Code != @CustCode)
					set @Log_Msg = 'Customer Code,';
				If(@Old_Name != @CustDesc)
					set @Log_Msg = @Log_Msg + 'Customer Name,';
				If(@Old_Email != @Email)
					set @Log_Msg = @Log_Msg + 'Email,';
				If(@Old_Contact != @Contact)
					set @Log_Msg = @Log_Msg + 'Contact Number,';
				If(@Old_Status != @Status)
					set @Log_Msg = @Log_Msg + 'Customer Status,';

				IF(Len(@Log_Msg) != 0)
				BEGIN
					SELECT @Log_Msg = LEFT(@Log_Msg, LEN(@Log_Msg) - 1);

					Insert into DBLog (PgIdx, EmpId, ProcName, SlNo, LogData, CreatedOn) Values(@PgIdx,@Modifiedby,'Customer_save',@Cnt,
					'Customer : ' + @Old_Name +' Modified '+ @Log_Msg, GETDATE());
				END
			Update Customers Set CustCode = @CustCode, CustDesc = @CustDesc, Email = @Email, ContactNo = @Contact, Status=@Status
			Where CustId = @CustId;
		END
	END TRY
	BEGIN CATCH
	if (len(@ErrorMsg) = 0)
			set @ErrorMsg = ERROR_MESSAGE() + '\n Save Failed';
		Select @ErrorMsg;
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[Employee_Delete]    Script Date: 09-06-2025 11:46:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[Employee_Delete](@PgIdx int, @EmpId bigint, @ModifiedBy bigint)
As
Begin
	Declare @EmpName as Varchar(100);
	Declare @Cnt as int;
	
	Select @EmpName = EmpName from Employees Where EmpId = @EmpId;

	SELECT @Cnt = CASE WHEN EXISTS (SELECT 1 FROM DBLog WHERE PgIdx = @PgIdx) THEN (SELECT MAX(SlNo) + 1 FROM DBLog WHERE PgIdx = @PgIdx)
		ELSE 1 END;
	Insert into DBLog (PgIdx, EmpId, ProcName, SlNo, LogData, CreatedOn) Values(@PgIdx,@Modifiedby,'Employee_Delete',@Cnt,
				'Employee : ' + @EmpName +' Deleted', GETDATE());
	Update Employees Set Status = 0 Where EmpId = @EmpId;
End
GO
/****** Object:  StoredProcedure [dbo].[Employee_Grid]    Script Date: 09-06-2025 11:46:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[Employee_Grid] (@Status VARCHAR(25))
As
Begin 
	Select EmpId, EmpCode, EmpName, Email, Mobile, [Status] from Employees Where [Status] = @Status Order by EmpName;
End
GO
/****** Object:  StoredProcedure [dbo].[Employee_Save]    Script Date: 09-06-2025 11:46:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Employee_Save] (@PgIdx int, @EmpId bigint,  @EmpCode Varchar(20), @EmpName Varchar(100), 
@Mobile Varchar(10),@Email Varchar(100), @AppPwd Varchar(20), @Status INT, @Modifiedby bigint, @ErrorMsg VARCHAR(1000) OUTPUT)
AS
BEGIN
	declare @Cnt as int;
	DECLARE @GenId as Bigint = 0;
	Declare @Old_Code as Varchar(20) = '';
	Declare @Old_Name as Varchar(100) = '';
	Declare @Old_Email as Varchar(100) = '';
	Declare @Old_Mobile as Varchar(10) = '';
	Declare @Old_Status as Int = 0;
	Declare @Log_Msg as Varchar(1000) = '';

	BEGIN TRY
		if (@EmpId = 0)
			BEGIN
				SELECT @Cnt = count(1) FROM Employees WHERE EmpCode = @EmpCode;
				IF (@Cnt != 0)
					BEGIN
					SET @ErrorMsg = 'Employee Code already exists'
					RAISERROR (@ErrorMsg, 16, 1)
				END
				SELECT @Cnt = count(1) FROM Employees WHERE EmpName = @EmpName;
				IF (@Cnt != 0)
					BEGIN
					SET @ErrorMsg = 'Employee Name already exists'
					RAISERROR (@ErrorMsg, 16, 1)
				END
				SELECT @Cnt = count(1) FROM Employees WHERE Mobile = @Mobile;
				IF (@Cnt != 0)
					BEGIN
					SET @ErrorMsg = 'Mobile Number already exists'
					RAISERROR (@ErrorMsg, 16, 1)
				END
				SELECT @Cnt = count(1) FROM Employees WHERE Email = @Email;
				IF (@Cnt != 0)
					BEGIN
					SET @ErrorMsg = 'Email already exists'
					RAISERROR (@ErrorMsg, 16, 1)
				END
			END
		else
			BEGIN
				SELECT @Cnt = count(1) FROM Employees WHERE EmpCode = @EmpCode AND  EmpId != @EmpId;
				IF (@Cnt != 0)
					BEGIN
						SET @ErrorMsg = 'Employee Code already exists'
						RAISERROR (@ErrorMsg, 16, 1)
					END
				SELECT @Cnt = count(1) FROM Employees WHERE EmpName = @EmpName AND  EmpId != @EmpId;
				IF (@Cnt != 0)
					BEGIN
						SET @ErrorMsg = 'Employee Name already exists'
						RAISERROR (@ErrorMsg, 16, 1)
					END
				SELECT @Cnt = count(1) FROM Employees WHERE Mobile = @Mobile AND  EmpId != @EmpId;
				IF (@Cnt != 0)
					BEGIN
						SET @ErrorMsg = 'Mobile Number already exists'
						RAISERROR (@ErrorMsg, 16, 1)
					END
				SELECT @Cnt = count(1) FROM Employees WHERE Email = @Email AND  EmpId != @EmpId;
				IF (@Cnt != 0)
					BEGIN
						SET @ErrorMsg = 'Email already exists'
						RAISERROR (@ErrorMsg, 16, 1)
					END
			END

		set @Cnt = 0;
		SELECT @Cnt = CASE WHEN EXISTS (SELECT 1 FROM DBLog WHERE PgIdx = @PgIdx) THEN (SELECT MAX(SlNo) + 1 FROM DBLog WHERE PgIdx = @PgIdx)
		ELSE 1 END;
		IF(@EmpId = 0)
			BEGIN
				SELECT @GenId = CONCAT('1',FORMAT(GETDATE(),'yyMMdd'),FORMAT(COUNT(1)+1,'000')) FROM Employees WHERE EmpId <> 0 and
									CONVERT(VARCHAR(10),CreatedOn,121) = CONVERT(VARCHAR(10),GETDATE(),121);
				IF (@GenId IS NULL or @GenId = 0)
					BEGIN
						SET @ErrorMsg = 'Employee ID Generate Failed.\n Retry Again'
						RAISERROR (@ErrorMsg, 16, 1)
					END
				INSERT INTO Employees(EmpId, EmpCode, EmpName, Email, Mobile, AppPwd, Status, CreatedOn) 
				VALUES (@GenId, @EmpCode, @EmpName, @Email, @Mobile, @AppPwd, @Status, GETDATE());

				Insert into DBLog (PgIdx, EmpId, ProcName, SlNo, LogData, CreatedOn) Values(@PgIdx,@Modifiedby,'Employee_save',@Cnt,
				'New Employee : ' + @EmpName +' Created', GETDATE());

				INSERT INTO MenuMapping (EmpId,MenuId,PgAction) select @GenId,MenuId,PgAction from AppMenus;
			END
		ELSE
			BEGIN
				Select @Old_Code = EmpCode, @Old_Name = EmpName, @Old_Email = Email, @Old_Mobile = Mobile, @Old_Status = [Status] 
				from Employees Where EmpId = @EmpId;
				If(@Old_Code != @EmpCode)
					set @Log_Msg = 'Employee Code,';
				If(@Old_Name != @EmpName)
					set @Log_Msg = @Log_Msg + 'Employee Name,';
				If(@Old_Email != @Email)
					set @Log_Msg = @Log_Msg + 'Employee Email,';
				If(@Old_Mobile != @Mobile)
					set @Log_Msg = @Log_Msg + 'Employee Mobile,';
				If(@Old_Status != @Status)
					set @Log_Msg = @Log_Msg + 'Employee Status,';

				IF(Len(@Log_Msg) != 0)
				BEGIN
					SELECT @Log_Msg = LEFT(@Log_Msg, LEN(@Log_Msg) - 1);

					Insert into DBLog (PgIdx, EmpId, ProcName, SlNo, LogData, CreatedOn) Values(@PgIdx,@Modifiedby,'Employee_save',@Cnt,
					'Employee : ' + @Old_Name +' Modified '+ @Log_Msg, GETDATE());
				END
				UPDATE Employees SET EmpCode = @EmpCode, EmpName = @EmpName, Email = @Email,Mobile=@Mobile, Status = @Status 
				WHERE EmpId = @EmpId;
			END
	END TRY
	BEGIN CATCH
		if (len(@ErrorMsg) = 0)
			set @ErrorMsg = ERROR_MESSAGE() + '\n Save Failed';
		Select @ErrorMsg;
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[PageAction_Get]    Script Date: 09-06-2025 11:46:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[PageAction_Get](@EmpId Varchar(15))
As
Begin 
declare @Qry as varchar(max);


	set @Qry = 'select Case am.MenuType When  1 Then ''Dashboard'' When  2 Then ''Masters'' When  3 Then ''Service'' 
	When  4 Then ''Reports'' Else '''' End as MenuType, am.MenuDesc, am.MenuId as MenuId,am.PgAction as ChbShow, 
	isnull(mm.PgAction,''00000'') as ChbFill from AppMenus am 
	Left Join MenuMapping mm on ( am.MenuId = mm.MenuId and mm.EmpId = '+ @EmpId +') order by am.MenuType,MenuId asc ';

Print (@Qry);
Exec (@Qry);

End
GO
/****** Object:  StoredProcedure [dbo].[PageAction_Save]    Script Date: 09-06-2025 11:46:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[PageAction_Save](@PgIdx int, @MapId bigint, @ModifiedBy bigint, @ErrorMsg Varchar(5000) OUTPUT)
AS
BEGIN
-- TmpTable --> Idx PgIdx,EmpId LgnUsr, I1 OrgType, I2 MenuId, V1 PageAction

	BEGIN TRY
		BEGIN TRANSACTION
			DELETE FROM MenuMapping WHERE EmpId = @MapId;

			INSERT INTO MenuMapping (EmpId,MenuId,PgAction) 
			select @MapId,IData1,VData1 from TmpTable where 
			Idx= @PgIdx and EmpId = @ModifiedBy;	-- and IData2 >= 100;

			delete from TmpTable where Idx= @PgIdx and EmpId = @ModifiedBy;

		COMMIT TRANSACTION;
			SELECT @ErrorMsg = 1;
			select @ErrorMsg;
	END TRY
	BEGIN CATCH
		IF(len(@ErrorMsg) = 0)
			set @ErrorMsg = ERROR_MESSAGE() + ' \n Save Failed';
		ROLLBACK TRANSACTION
		SELECT @ErrorMsg;
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[Product_Delete]    Script Date: 09-06-2025 11:46:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[Product_Delete](@PgIdx int, @ProductId bigint, @ModifiedBy bigint)
As
Begin
	Declare @ProductName as Varchar(100);
	Declare @Cnt as int;
	
	Select @ProductName = ProductDesc from Products Where ProductId = @ProductId;

	SELECT @Cnt = CASE WHEN EXISTS (SELECT 1 FROM DBLog WHERE PgIdx = @PgIdx) THEN (SELECT MAX(SlNo) + 1 FROM DBLog WHERE PgIdx = @PgIdx)
		ELSE 1 END;
	Insert into DBLog (PgIdx, EmpId, ProcName, SlNo, LogData, CreatedOn) Values(@PgIdx,@Modifiedby,'Product_Delete',@Cnt,
				'Product : ' + @ProductName +' Deleted', GETDATE());
	Update Products Set Status = 0 Where ProductId = @ProductId;
End
GO
/****** Object:  StoredProcedure [dbo].[Product_Grid]    Script Date: 09-06-2025 11:46:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[Product_Grid](@Status int)
As
Begin
	Select ProductId, ProductCode, ProductDesc, Stock, [Status] from Products Where [Status] = @Status;
End
GO
/****** Object:  StoredProcedure [dbo].[Product_Save]    Script Date: 09-06-2025 11:46:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Product_Save] @PgIdx int, @ProductId int, @ProductCode VARCHAR(20),@ProductDesc VARCHAR(100),
@Stock int,@Status Int, @ModifiedBy Bigint, @ErrorMsg VARCHAR(1000) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
	declare @Cnt as int;
	Declare @Old_Code as Varchar(20) = '';
	Declare @Old_Name as Varchar(100) = '';
	Declare @Old_Stock as Varchar(100) = '';
	Declare @Old_Status as Int = 0;
	Declare @Log_Msg as Varchar(1000) = '';
	Declare @PreBarcode as Varchar(50) = '';
	DECLARE @Counter INT = 1;

	BEGIN TRY
	IF(@ProductId = 0)
		SELECT @Cnt = count(1) FROM Products WHERE ProductCode = @ProductCode;
	ELSE
		SELECT @Cnt = count(1) FROM Products WHERE ProductCode = @ProductCode and ProductId != @ProductId;
	IF (@Cnt != 0)
		BEGIN
			SET @ErrorMsg = 'Product Code already exists'
			RAISERROR (@ErrorMsg, 16, 1)
		END

	IF(@ProductId = 0)
		SELECT @Cnt = count(1) FROM Products WHERE ProductDesc = @ProductDesc;
	ELSE
		SELECT @Cnt = count(1) FROM Products WHERE ProductDesc = @ProductDesc and ProductId != @ProductId;
	IF (@Cnt != 0)
		BEGIN
			SET @ErrorMsg = 'Product Name already exists'
			RAISERROR (@ErrorMsg, 16, 1)
		END
    
	SELECT @Cnt = CASE WHEN EXISTS (SELECT 1 FROM DBLog WHERE PgIdx = @PgIdx) THEN (SELECT MAX(SlNo) + 1 FROM DBLog WHERE PgIdx = @PgIdx)
		ELSE 1 END;
	If(@ProductId = 0)
		BEGIN
			Select @ProductId = ISNULL(Max(ProductId),0)+1 from Products;

			Set @PreBarcode = CONCAT(@ProductCode,@ProductId,FORMAT(GETDATE(),'yyMMdd'),COUNT(1)+1);
			-- Insert new Product
			INSERT INTO Products (ProductId,ProductCode, ProductDesc, Stock, Status)
			VALUES (@ProductId, @ProductCode, @ProductDesc, @Stock, @Status);

			WHILE @Counter <= @Stock
				BEGIN
					INSERT INTO ProductStock (ProductId, CustId, Barcode, SlNo, Status)
					VALUES (@ProductId,0,@PreBarcode + RIGHT('0000' + CAST(@Counter AS VARCHAR(4)), 4),@Counter,1);

					SET @Counter = @Counter + 1;
				END;

			Insert into DBLog (PgIdx, EmpId, ProcName, SlNo, LogData, CreatedOn) Values(@PgIdx,@Modifiedby,'Product_save',@Cnt,
				'New Product : ' + @ProductDesc +' Created', GETDATE());
		END
	ELSE
		BEGIN
			Select @Old_Code = ProductCode, @Old_Name = ProductDesc, @Old_Stock = Stock, @Old_Status = [Status] 
				from Products Where ProductId = @ProductId;
				If(@Old_Code != @ProductCode)
					set @Log_Msg = 'Product Code,';
				If(@Old_Name != @ProductDesc)
					set @Log_Msg = @Log_Msg + 'Product Name,';
				If(@Old_Stock != @Stock)
					set @Log_Msg = @Log_Msg + 'Stock,';
				If(@Old_Status != @Status)
					set @Log_Msg = @Log_Msg + 'Product Status,';

				IF(Len(@Log_Msg) != 0)
				BEGIN
					SELECT @Log_Msg = LEFT(@Log_Msg, LEN(@Log_Msg) - 1);

					Insert into DBLog (PgIdx, EmpId, ProcName, SlNo, LogData, CreatedOn) Values(@PgIdx,@Modifiedby,'Product_save',@Cnt,
					'Product : ' + @Old_Name +' Modified '+ @Log_Msg, GETDATE());
				END
			Update Products Set ProductCode = @ProductCode, ProductDesc = @ProductDesc, Stock = @Stock, Status=@Status
			Where ProductId = @ProductId;
		END
	END TRY
	BEGIN CATCH
	if (len(@ErrorMsg) = 0)
			set @ErrorMsg = ERROR_MESSAGE() + '\n Save Failed';
		Select @ErrorMsg;
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[Reason_Delete]    Script Date: 09-06-2025 11:46:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[Reason_Delete](@PgIdx int, @ReasonId bigint, @ModifiedBy bigint)
As
Begin
	Declare @ReasonName as Varchar(100);
	Declare @Cnt as int;
	
	Select @ReasonName = ReasonDesc from Reasons Where ReasonId = @ReasonId;

	SELECT @Cnt = CASE WHEN EXISTS (SELECT 1 FROM DBLog WHERE PgIdx = @PgIdx) THEN (SELECT MAX(SlNo) + 1 FROM DBLog WHERE PgIdx = @PgIdx)
		ELSE 1 END;
	Insert into DBLog (PgIdx, EmpId, ProcName, SlNo, LogData, CreatedOn) Values(@PgIdx,@Modifiedby,'Reason_Delete',@Cnt,
				'Reason : ' + @ReasonName +' Deleted', GETDATE());
	Update Reasons Set Status = 0 Where ReasonId = @ReasonId;
End
GO
/****** Object:  StoredProcedure [dbo].[Reason_Grid]    Script Date: 09-06-2025 11:46:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[Reason_Grid](@Status int)
As
Begin
	Select ReasonId, ReasonDesc, [Status] from Reasons Where [Status] = @Status;
End
GO
/****** Object:  StoredProcedure [dbo].[Reason_Save]    Script Date: 09-06-2025 11:46:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Reason_Save] @PgIdx int, @ReasonId int, @ReasonDesc VARCHAR(100),@Status Int, 
@ModifiedBy Bigint, @ErrorMsg VARCHAR(1000) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
	declare @Cnt as int;
	Declare @Old_Name as Varchar(100) = '';
	Declare @Old_Status as Int = 0;
	Declare @Log_Msg as Varchar(1000) = '';

	BEGIN TRY
	IF(@ReasonId = 0)
		SELECT @Cnt = count(1) FROM Reasons WHERE ReasonDesc = @ReasonDesc;
	ELSE
		SELECT @Cnt = count(1) FROM Reasons WHERE ReasonDesc = @ReasonDesc and ReasonId != @ReasonId;
	IF (@Cnt != 0)
		BEGIN
			SET @ErrorMsg = 'This Reason already exists'
			RAISERROR (@ErrorMsg, 16, 1)
		END

	SELECT @Cnt = CASE WHEN EXISTS (SELECT 1 FROM DBLog WHERE PgIdx = @PgIdx) THEN (SELECT MAX(SlNo) + 1 FROM DBLog WHERE PgIdx = @PgIdx)
		ELSE 1 END;
	If(@ReasonId = 0)
		BEGIN
			Select @ReasonId = ISNULL(Max(ReasonId),0)+1 from Reasons;
			
			INSERT INTO Reasons (ReasonId,ReasonDesc, Status)
			VALUES (@ReasonId, @ReasonDesc, @Status);

			Insert into DBLog (PgIdx, EmpId, ProcName, SlNo, LogData, CreatedOn) Values(@PgIdx,@Modifiedby,'Reason_save',@Cnt,
				'New Reason : ' + @ReasonDesc +' Created', GETDATE());
		END
	ELSE
		BEGIN
			Select @Old_Name = ReasonDesc,@Old_Status = [Status] from Reasons Where ReasonId = @ReasonId;
				If(@Old_Name != @ReasonDesc)
					set @Log_Msg = @Log_Msg + 'Reason Name,';
				If(@Old_Status != @Status)
					set @Log_Msg = @Log_Msg + 'Reason Status,';

				IF(Len(@Log_Msg) != 0)
				BEGIN
					SELECT @Log_Msg = LEFT(@Log_Msg, LEN(@Log_Msg) - 1);

					Insert into DBLog (PgIdx, EmpId, ProcName, SlNo, LogData, CreatedOn) Values(@PgIdx,@Modifiedby,'Reason_save',@Cnt,
					'Reason : ' + @Old_Name +' Modified '+ @Log_Msg, GETDATE());
				END
			Update Reasons Set ReasonDesc = @ReasonDesc, Status=@Status Where ReasonId = @ReasonId;
		END
	END TRY
	BEGIN CATCH
	if (len(@ErrorMsg) = 0)
			set @ErrorMsg = ERROR_MESSAGE() + '\n Save Failed';
		RAISERROR(@ErrorMsg, 16, 1);
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[Rpt_Inventory]    Script Date: 09-06-2025 11:46:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Rpt_Inventory](@PgIdx Varchar(5), @CustId Varchar(15))
As
BEGIN
	Declare @Where as Varchar(100) = '';
	Declare @Qry as nVarchar(max) = '';

	IF(@PgIdx = 403 or (@PgIdx = 404 and @CustId != '0'))
		Set @Where = ' and PS.CustId = '+ @CustId;
	ELSE IF(@PgIdx = 404)
		Set @Where = ' and PS.CustId != 0';

  Set @Qry = 'Select C.CustDesc, P.ProductDesc, Count(PS.Barcode) as BarcodeQty, PS.ProductId, STRING_AGG(PS.Barcode, '','') as Barcode
	From Products P, ProductStock PS LEFt JOIN Customers C On(PS.CustId = C.CustId) 
  Where PS.ProductId = P.ProductId and PS.Status = 1 '+ @Where +' Group By C.CustDesc, PS.ProductId, P.ProductDesc';

  Print(@Qry);
  EXEC (@Qry);
END

GO
/****** Object:  StoredProcedure [dbo].[Rpt_StockAdjust]    Script Date: 09-06-2025 11:46:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Rpt_StockAdjust](@PgIdx Varchar(5), @CustId Varchar(15), @FromDate Varchar(10), @ToDate Varchar(10))
As
BEGIN
	Declare @Where as Varchar(100) = '';
	Declare @Qry as nVarchar(max) = '';

	IF(@PgIdx = 401 or (@PgIdx = 402 and @CustId != '0'))
		Set @Where = ' and SA.CustId = '+ @CustId;
	ELSE IF(@PgIdx = 402)
		Set @Where = ' and SA.CustId != 0';

  Set @Qry = 'Select C.CustDesc, P.ProductDesc, SA.StkAdjType, SA.AdjustQty, SA.Stock, SA.SlNos, R.ReasonDesc, E.EmpName
	From Employees E, Products P, Reasons R, StockAdjust SA LEFt JOIN Customers C On(SA.CustId = C.CustId) 
  Where SA.EmpId = E.EmpId And SA.ProductId = P.ProductId and SA.ReasonId = R.ReasonId and 
  Convert(varchar(10),SA.CreatedOn,121) >= '''+ @FromDate +''' and Convert(varchar(10),SA.CreatedOn,121) <= '''+ @ToDate +''' '+ @Where+' ';

  Print(@Qry);
  EXEC (@Qry);
END
GO
/****** Object:  StoredProcedure [dbo].[Shift_Delete]    Script Date: 09-06-2025 11:46:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[Shift_Delete](@PgIdx int, @ShiftId bigint, @ModifiedBy bigint)
As
Begin
	Declare @ShiftName as Varchar(100);
	Declare @Cnt as int;
	
	Select @ShiftName = ShiftDesc from Shifts Where ShiftId = @ShiftId;

	SELECT @Cnt = CASE WHEN EXISTS (SELECT 1 FROM DBLog WHERE PgIdx = @PgIdx) THEN (SELECT MAX(SlNo) + 1 FROM DBLog WHERE PgIdx = @PgIdx)
		ELSE 1 END;
	Insert into DBLog (PgIdx, EmpId, ProcName, SlNo, LogData, CreatedOn) Values(@PgIdx,@Modifiedby,'Shift_Delete',@Cnt,
				'Shift : ' + @ShiftName +' Deleted', GETDATE());
	Update Shifts Set Status = 0 Where ShiftId = @ShiftId;
End
GO
/****** Object:  StoredProcedure [dbo].[Shift_Grid]    Script Date: 09-06-2025 11:46:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[Shift_Grid](@Status int)
As
Begin
	Select ShiftId, ShiftCode, ShiftDesc, CAST(FromTime AS VARCHAR(8)) AS FromTime, CAST(ToTime AS VARCHAR(8)) AS ToTime, [Status] from Shifts Where [Status] = @Status;
End
GO
/****** Object:  StoredProcedure [dbo].[Shift_Save]    Script Date: 09-06-2025 11:46:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Shift_Save] @PgIdx int, @ShiftId int, @ShiftCode VARCHAR(20),@ShiftDesc VARCHAR(100),
@FromTime VARCHAR(10),@ToTime VARCHAR(10),@Status Int, @ModifiedBy Bigint, @ErrorMsg VARCHAR(1000) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
	declare @Cnt as int;
	Declare @Old_Code as Varchar(20) = '';
	Declare @Old_Name as Varchar(100) = '';
	Declare @Old_FromTime as Varchar(100) = '';
	Declare @Old_ToTime as Varchar(10) = '';
	Declare @Old_Status as Int = 0;
	Declare @Log_Msg as Varchar(1000) = '';

	BEGIN TRY
	IF(@ShiftId = 0)
		SELECT @Cnt = count(1) FROM Shifts WHERE ShiftCode = @ShiftCode;
	ELSE
		SELECT @Cnt = count(1) FROM Shifts WHERE ShiftCode = @ShiftCode and ShiftId != @ShiftId;
	IF (@Cnt != 0)
		BEGIN
			SET @ErrorMsg = 'Shift Code already exists'
			RAISERROR (@ErrorMsg, 16, 1)
		END

	IF(@ShiftId = 0)
		SELECT @Cnt = count(1) FROM Shifts WHERE ShiftDesc = @ShiftDesc;
	ELSE
		SELECT @Cnt = count(1) FROM Shifts WHERE ShiftDesc = @ShiftDesc and ShiftId != @ShiftId;
	IF (@Cnt != 0)
		BEGIN
			SET @ErrorMsg = 'Shift Name already exists'
			RAISERROR (@ErrorMsg, 16, 1)
		END
    -- Check for overlapping shifts
    IF EXISTS (SELECT 1 FROM Shifts WHERE (@FromTime <= ToTime AND @ToTime >= FromTime) and Status = 1 and ShiftId != @ShiftId)
    BEGIN
        SET @ErrorMsg = 'Shift time overlaps with an existing shift.';
        RAISERROR (@ErrorMsg, 16, 1);
    END

	SELECT @Cnt = CASE WHEN EXISTS (SELECT 1 FROM DBLog WHERE PgIdx = @PgIdx) THEN (SELECT MAX(SlNo) + 1 FROM DBLog WHERE PgIdx = @PgIdx)
		ELSE 1 END;
	If(@ShiftId = 0)
		BEGIN
			Select @ShiftId = ISNULL(Max(ShiftId),0)+1 from Shifts;
			-- Insert new shift
			INSERT INTO Shifts (ShiftId,ShiftCode, ShiftDesc, FromTime, ToTime, Status)
			VALUES (@ShiftId, @ShiftCode, @ShiftDesc, @FromTime, @ToTime, @Status);

			Insert into DBLog (PgIdx, EmpId, ProcName, SlNo, LogData, CreatedOn) Values(@PgIdx,@Modifiedby,'Shift_save',@Cnt,
				'New Shift : ' + @ShiftDesc +' Created', GETDATE());
		END
	ELSE
		BEGIN
			Select @Old_Code = ShiftCode, @Old_Name = ShiftDesc, @Old_FromTime = FromTime, @Old_ToTime = ToTime, @Old_Status = [Status] 
				from Shifts Where ShiftId = @ShiftId;
				If(@Old_Code != @ShiftCode)
					set @Log_Msg = 'Shift Code,';
				If(@Old_Name != @ShiftDesc)
					set @Log_Msg = @Log_Msg + 'Shift Name,';
				If(@Old_FromTime != @FromTime)
					set @Log_Msg = @Log_Msg + 'From Time,';
				If(@Old_ToTime != @ToTime)
					set @Log_Msg = @Log_Msg + 'ToTime,';
				If(@Old_Status != @Status)
					set @Log_Msg = @Log_Msg + 'Shift Status,';

				IF(Len(@Log_Msg) != 0)
				BEGIN
					SELECT @Log_Msg = LEFT(@Log_Msg, LEN(@Log_Msg) - 1);

					Insert into DBLog (PgIdx, EmpId, ProcName, SlNo, LogData, CreatedOn) Values(@PgIdx,@Modifiedby,'Shift_save',@Cnt,
					'Shift : ' + @Old_Name +' Modified '+ @Log_Msg, GETDATE());
				END
			Update Shifts Set ShiftCode = @ShiftCode, ShiftDesc = @ShiftDesc, FromTime = @FromTime, ToTime = @ToTime, Status=@Status
			Where ShiftId = @ShiftId;
		END
	END TRY
	BEGIN CATCH
	if (len(@ErrorMsg) = 0)
			set @ErrorMsg = ERROR_MESSAGE() + '\n Save Failed';
		Select @ErrorMsg;
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[Transporter_Delete]    Script Date: 09-06-2025 11:46:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[Transporter_Delete](@PgIdx int, @TransporterId Int, @ModifiedBy bigint)
As
Begin
	Declare @TransporterName as Varchar(100);
	Declare @Cnt as int;
	
	Select @TransporterName = TransporterDesc from Transporters Where TransporterId = @TransporterId;

	SELECT @Cnt = CASE WHEN EXISTS (SELECT 1 FROM DBLog WHERE PgIdx = @PgIdx) THEN (SELECT MAX(SlNo) + 1 FROM DBLog WHERE PgIdx = @PgIdx)
		ELSE 1 END;
	Insert into DBLog (PgIdx, EmpId, ProcName, SlNo, LogData, CreatedOn) Values(@PgIdx,@Modifiedby,'Product_Delete',@Cnt,
				'Transporter : ' + @TransporterName +' Deleted', GETDATE());
	Update Transporters Set Status = 0 Where TransporterId = @TransporterId;
End
GO
/****** Object:  StoredProcedure [dbo].[Transporter_Grid]    Script Date: 09-06-2025 11:46:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[Transporter_Grid](@Status int)
As
Begin
	Select TransporterId, TransporterCode, TransporterDesc, ContactNo, [Status] from Transporters Where [Status] = @Status;
End
GO
/****** Object:  StoredProcedure [dbo].[Transporter_Save]    Script Date: 09-06-2025 11:46:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Transporter_Save] @PgIdx int, @TransporterId int, @TransporterCode VARCHAR(40),@TransporterDesc VARCHAR(100),
@Contact Varchar(10),@Status Int, @ModifiedBy Bigint, @ErrorMsg VARCHAR(1000) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
	declare @Cnt as int;
	Declare @Old_Code as Varchar(20) = ''; 
	Declare @Old_Name as Varchar(100) = '';	
	Declare @Old_Contact as Varchar(10) = '';	
	Declare @Old_Status as Int = 0;	
	Declare @Log_Msg as Varchar(1000) = '';

	BEGIN TRY
	IF(@TransporterId = 0)
		SELECT @Cnt = count(1) FROM Transporters WHERE TransporterCode = @TransporterCode;
	ELSE
		SELECT @Cnt = count(1) FROM Transporters WHERE TransporterCode = @TransporterCode and TransporterId != @TransporterId;
	IF (@Cnt != 0)
		BEGIN
			SET @ErrorMsg = 'Transporter Code already exists'
			RAISERROR (@ErrorMsg, 16, 1)
		END

	IF(@TransporterId = 0)
		SELECT @Cnt = count(1) FROM Transporters WHERE TransporterDesc = @TransporterDesc;
	ELSE
		SELECT @Cnt = count(1) FROM Transporters WHERE TransporterDesc = @TransporterDesc and TransporterId != @TransporterId;
	IF (@Cnt != 0)
		BEGIN
			SET @ErrorMsg = 'Transporter Name already exists'
			RAISERROR (@ErrorMsg, 16, 1)
		END

    set @Cnt = 0;
	SELECT @Cnt = CASE WHEN EXISTS (SELECT 1 FROM DBLog WHERE PgIdx = @PgIdx) THEN (SELECT MAX(SlNo) + 1 FROM DBLog WHERE PgIdx = @PgIdx)
		ELSE 1 END;
	Begin Transaction
	IF (@TransporterId = 0)
	BEGIN
		Select @TransporterId = ISNULL(Max(TransporterId),0)+1 from Transporters;

		INSERT INTO Transporters(TransporterId, TransporterCode, TransporterDesc, ContactNo, Status)
		VALUES (@TransporterId, @TransporterCode, @TransporterDesc, @Contact, @Status);

		INSERT INTO DBLog (PgIdx, EmpId, ProcName, SlNo, LogData, CreatedOn) 
		VALUES (@PgIdx, @Modifiedby, 'Transporter_save', @Cnt, 'New Transporter : ' + @TransporterDesc + ' Created', GETDATE());
	END
	ELSE
		BEGIN
			Select @Old_Code = TransporterCode, @Old_Name = TransporterDesc, @Old_Contact = ContactNo, @Old_Status = [Status] 
				from Transporters Where TransporterId = @TransporterId;
				If(@Old_Code != @TransporterCode)
					set @Log_Msg = 'Transporter Code,';
				If(@Old_Name != @TransporterDesc)
					set @Log_Msg = @Log_Msg + 'Transporter Name,';
				If(@Old_Contact != @Contact)
					set @Log_Msg = @Log_Msg + 'Contact Number,';
				If(@Old_Status != @Status)
					set @Log_Msg = @Log_Msg + 'Transporter Status,';

				IF(Len(@Log_Msg) != 0)
				BEGIN
					SELECT @Log_Msg = LEFT(@Log_Msg, LEN(@Log_Msg) - 1);

					Insert into DBLog (PgIdx, EmpId, ProcName, SlNo, LogData, CreatedOn) Values(@PgIdx,@Modifiedby,'Transporter_save',@Cnt,
					'Transporter : ' + @Old_Name +' Modified '+ @Log_Msg, GETDATE());
				END
			Update Transporters Set TransporterCode = @TransporterCode, TransporterDesc = @TransporterDesc, ContactNo = @Contact, Status=@Status
			Where TransporterId = @TransporterId;
		END

	Update TransporterVehicles Set Status = 0 Where TransporterId = @TransporterId;

	Update TV Set TV.VehicleType = T.VData2, TV.Remarks = T.VData3, TV.Status = T.IData2 
	from TransporterVehicles TV, TmpTable T Where T.Idx = @PgIdx and T.EmpId = @ModifiedBy and 
	TV.TransporterId = T.IData1 and TV.VehicleNo = T.VData1;

	Insert Into TransporterVehicles (TransporterId, VehicleNo, VehicleType, Remarks, [Status]) 
	Select @TransporterId, T.VData1, T.VData2, T.VData3, T.IData2 from TmpTable T Where T.Idx = @PgIdx and T.EmpId = @ModifiedBy and 
	NOT EXISTS(Select 1 From TransporterVehicles TV Where TV.TransporterId = @TransporterId and TV.VehicleNo = T.VData1);

	Commit Transaction
	END TRY
	BEGIN CATCH
	Rollback Transaction
	if (len(@ErrorMsg) = 0)
			set @ErrorMsg = ERROR_MESSAGE() + '\n Save Failed';
		Select @ErrorMsg;
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[TVehicle_Delete]    Script Date: 09-06-2025 11:46:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[TVehicle_Delete](@PgIdx int,@TransporterId int,@VehicleNo Varchar(20), @EmpId bigint)
As
Begin
--Define TmpTable --> IData1 => TransporterID, VData1 => VehicleNo, VData2 => VehicleType, VData3 => Remarks, Idata2 => [Status]
--***************
	IF(@TransporterId = 0)
		Delete TmpTable Where Idx = @PgIdx and EmpId = @EmpId;
	ELSE
		Update TmpTable Set Idata2 = 0 Where Idx = @PgIdx and EmpId = @EmpId and Idata1 = @TransporterId and Vdata1 = @VehicleNo;

	Select IData1 as TransporterId, VData1 as VehicleNo,VData2 as VehicleType,VData3 as Remarks,Idata2 as [Status] 
	From TmpTable Where Idx = @PgIdx and EmpId = @EmpId and IData2 = 1;
End
GO
/****** Object:  StoredProcedure [dbo].[TVehicle_Grid]    Script Date: 09-06-2025 11:46:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[TVehicle_Grid](@PgIdx int, @EmpId bigint,@TransporterId int)
As
Begin
--Define TmpTable --> IData1 => TransporterID, VData1 => VehicleNo, VData2 => VehicleType, VData3 => Remarks, Idata2 => [Status]
--***************
	
	delete from TmpTable Where Idx = @PgIdx and EmpId = @EmpId;

	Insert into TmpTable(Idx,EmpId,IData1,VData1,VData2,VData3,Idata2) 
	Select @PgIdx, @EmpId, TransporterId, VehicleNo, VehicleType,Remarks,[Status] from TransporterVehicles 
	Where TransporterId = @TransporterId;

	Select IData1 as TransporterId, VData1 as VehicleNo,VData2 as VehicleType,VData3 as Remarks,Idata2 as [Status] 
	From TmpTable Where Idx = @PgIdx and EmpId = @EmpId and IData2 = 1;
End
GO
/****** Object:  StoredProcedure [dbo].[TVehicle_Save]    Script Date: 09-06-2025 11:46:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[TVehicle_Save](@PgIdx int,@TransporterId int,@VehicleNo Varchar(20), @Vehicletype Varchar(40), @Remarks Varchar(100), 
@Status int, @EmpId bigint)
As
Begin
--Define TmpTable --> IData1 => TransporterID, VData1 => VehicleNo, VData2 => VehicleType, VData3 => Remarks, Idata2 => [Status]
--***************
	declare @Cnt as int;

	SELECT @Cnt = COUNT(1) FROM TmpTable WHERE Idx = @PgIdx and EmpId = @EmpID and IData1 = @TransporterId and VData1 = @VehicleNo;
	
	IF(@Cnt != 0)
		Update TmpTable Set Vdata2 = @Vehicletype, Vdata3 = @Remarks, Idata2 = @Status Where Idx = @PgIdx and EmpId = @EmpId;
	ELSE
		Insert into TmpTable(Idx,EmpId,IData1,VData1,VData2,VData3,Idata2) 
		Values(@PgIdx, @EmpId, @TransporterId, @VehicleNo, @VehicleType,@Remarks,@Status);

	Select IData1 as TransporterId, VData1 as VehicleNo,VData2 as VehicleType,VData3 as Remarks,Idata2 as [Status] 
	From TmpTable Where Idx = @PgIdx and EmpId = @EmpId and IData2 = 1;
End
GO
