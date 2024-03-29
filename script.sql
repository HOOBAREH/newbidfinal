use [QuiBids]
GO
/****** Object:  Table [dbo].[Auction]    Script Date: 5/19/2019 10:07:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Auction](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NOT NULL,
	[Auction_Time] [time](7) NOT NULL,
	[Close_Time] [int] NOT NULL,
	[Reserve_Price] [int] NULL,
	[EndPrice] [int] NULL,
	[CurrentBid_Id] [int] NULL,
	[Current_UserId] [int] NULL,
	[StartStatus] [bit] NOT NULL,
	[IsClose] [bit] NOT NULL,
 CONSTRAINT [PK_Auction] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AuctionLogs]    Script Date: 5/19/2019 10:07:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AuctionLogs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[AuctionId] [int] NOT NULL,
	[DateTime] [datetime] NOT NULL,
	[TypeBid] [tinyint] NOT NULL,
 CONSTRAINT [PK_LogsAuction] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Countries]    Script Date: 5/19/2019 10:07:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Countries](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CountryCode] [varchar](2) NOT NULL,
	[Country_name] [varchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 5/19/2019 10:07:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Brand] [nvarchar](50) NULL,
	[Model_Year] [tinyint] NULL,
	[Price] [int] NOT NULL,
	[Img] [nvarchar](100) NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 5/19/2019 10:07:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Fname] [nvarchar](50) NOT NULL,
	[Lname] [nvarchar](50) NOT NULL,
	[Mobile] [nchar](15) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Address] [nvarchar](max) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[VoucherBid] [int] NULL,
	[RealBid] [int] NULL,
	[LastLogin] [datetime] NULL,
	[CountryId] [int] NULL,
	[HideLocation] [bit] NULL,
	[Image] [nvarchar](max) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Auction] ON 

INSERT [dbo].[Auction] ([Id], [ProductId], [Auction_Time], [Close_Time], [Reserve_Price], [EndPrice], [CurrentBid_Id], [Current_UserId], [StartStatus], [IsClose]) VALUES (1, 1, CAST(N'00:00:00' AS Time), 10, 84, 0, 0, 4, 1, 1)
INSERT [dbo].[Auction] ([Id], [ProductId], [Auction_Time], [Close_Time], [Reserve_Price], [EndPrice], [CurrentBid_Id], [Current_UserId], [StartStatus], [IsClose]) VALUES (2, 2, CAST(N'00:00:00' AS Time), 5, 17, 0, 0, 4, 1, 1)
INSERT [dbo].[Auction] ([Id], [ProductId], [Auction_Time], [Close_Time], [Reserve_Price], [EndPrice], [CurrentBid_Id], [Current_UserId], [StartStatus], [IsClose]) VALUES (3, 3, CAST(N'00:00:02' AS Time), 5, 18, 0, 0, 4, 1, 1)
INSERT [dbo].[Auction] ([Id], [ProductId], [Auction_Time], [Close_Time], [Reserve_Price], [EndPrice], [CurrentBid_Id], [Current_UserId], [StartStatus], [IsClose]) VALUES (4, 4, CAST(N'00:00:02' AS Time), 5, 3, 0, 0, 4, 1, 1)
INSERT [dbo].[Auction] ([Id], [ProductId], [Auction_Time], [Close_Time], [Reserve_Price], [EndPrice], [CurrentBid_Id], [Current_UserId], [StartStatus], [IsClose]) VALUES (5, 5, CAST(N'10:24:35' AS Time), 3, 4, 0, 0, 4, 0, 0)
INSERT [dbo].[Auction] ([Id], [ProductId], [Auction_Time], [Close_Time], [Reserve_Price], [EndPrice], [CurrentBid_Id], [Current_UserId], [StartStatus], [IsClose]) VALUES (40, 6, CAST(N'00:05:13' AS Time), 4, 13, 0, 0, 4, 0, 0)
INSERT [dbo].[Auction] ([Id], [ProductId], [Auction_Time], [Close_Time], [Reserve_Price], [EndPrice], [CurrentBid_Id], [Current_UserId], [StartStatus], [IsClose]) VALUES (41, 7, CAST(N'00:00:09' AS Time), 5, 7, 0, 0, NULL, 0, 1)
INSERT [dbo].[Auction] ([Id], [ProductId], [Auction_Time], [Close_Time], [Reserve_Price], [EndPrice], [CurrentBid_Id], [Current_UserId], [StartStatus], [IsClose]) VALUES (42, 8, CAST(N'00:00:02' AS Time), 5, 1, 0, 0, NULL, 1, 1)
INSERT [dbo].[Auction] ([Id], [ProductId], [Auction_Time], [Close_Time], [Reserve_Price], [EndPrice], [CurrentBid_Id], [Current_UserId], [StartStatus], [IsClose]) VALUES (43, 9, CAST(N'00:00:00' AS Time), 5, 45, 0, 0, 4, 1, 1)
INSERT [dbo].[Auction] ([Id], [ProductId], [Auction_Time], [Close_Time], [Reserve_Price], [EndPrice], [CurrentBid_Id], [Current_UserId], [StartStatus], [IsClose]) VALUES (44, 10, CAST(N'00:00:00' AS Time), 3, 4, 0, 0, 4, 1, 1)
INSERT [dbo].[Auction] ([Id], [ProductId], [Auction_Time], [Close_Time], [Reserve_Price], [EndPrice], [CurrentBid_Id], [Current_UserId], [StartStatus], [IsClose]) VALUES (45, 11, CAST(N'00:00:00' AS Time), 4, 10, 0, 0, 4, 1, 1)
INSERT [dbo].[Auction] ([Id], [ProductId], [Auction_Time], [Close_Time], [Reserve_Price], [EndPrice], [CurrentBid_Id], [Current_UserId], [StartStatus], [IsClose]) VALUES (46, 12, CAST(N'00:00:00' AS Time), 5, 7, 0, 0, NULL, 1, 1)
INSERT [dbo].[Auction] ([Id], [ProductId], [Auction_Time], [Close_Time], [Reserve_Price], [EndPrice], [CurrentBid_Id], [Current_UserId], [StartStatus], [IsClose]) VALUES (47, 13, CAST(N'00:00:03' AS Time), 5, 1, 0, 0, NULL, 1, 1)
INSERT [dbo].[Auction] ([Id], [ProductId], [Auction_Time], [Close_Time], [Reserve_Price], [EndPrice], [CurrentBid_Id], [Current_UserId], [StartStatus], [IsClose]) VALUES (48, 14, CAST(N'00:00:00' AS Time), 5, 45, 0, 0, 4, 1, 1)
INSERT [dbo].[Auction] ([Id], [ProductId], [Auction_Time], [Close_Time], [Reserve_Price], [EndPrice], [CurrentBid_Id], [Current_UserId], [StartStatus], [IsClose]) VALUES (49, 15, CAST(N'00:00:00' AS Time), 3, 4, 0, 0, 4, 1, 1)
INSERT [dbo].[Auction] ([Id], [ProductId], [Auction_Time], [Close_Time], [Reserve_Price], [EndPrice], [CurrentBid_Id], [Current_UserId], [StartStatus], [IsClose]) VALUES (50, 16, CAST(N'00:00:00' AS Time), 3, 4, 0, 0, 4, 1, 1)
INSERT [dbo].[Auction] ([Id], [ProductId], [Auction_Time], [Close_Time], [Reserve_Price], [EndPrice], [CurrentBid_Id], [Current_UserId], [StartStatus], [IsClose]) VALUES (51, 17, CAST(N'00:00:00' AS Time), 4, 10, 0, 0, 4, 1, 1)
INSERT [dbo].[Auction] ([Id], [ProductId], [Auction_Time], [Close_Time], [Reserve_Price], [EndPrice], [CurrentBid_Id], [Current_UserId], [StartStatus], [IsClose]) VALUES (52, 18, CAST(N'00:00:00' AS Time), 5, 7, 0, 0, NULL, 1, 1)
INSERT [dbo].[Auction] ([Id], [ProductId], [Auction_Time], [Close_Time], [Reserve_Price], [EndPrice], [CurrentBid_Id], [Current_UserId], [StartStatus], [IsClose]) VALUES (53, 19, CAST(N'00:00:00' AS Time), 5, 1, 0, 0, NULL, 1, 1)
INSERT [dbo].[Auction] ([Id], [ProductId], [Auction_Time], [Close_Time], [Reserve_Price], [EndPrice], [CurrentBid_Id], [Current_UserId], [StartStatus], [IsClose]) VALUES (54, 20, CAST(N'00:00:00' AS Time), 5, 45, 0, 0, 4, 0, 1)
SET IDENTITY_INSERT [dbo].[Auction] OFF
SET IDENTITY_INSERT [dbo].[Countries] ON 

INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (1, N'AF', N'Afghanistan')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (2, N'AL', N'Albania')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (3, N'DZ', N'Algeria')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (4, N'DS', N'American Samoa')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (5, N'AD', N'Andorra')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (6, N'AO', N'Angola')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (7, N'AI', N'Anguilla')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (8, N'AQ', N'Antarctica')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (9, N'AG', N'Antigua and Barbuda')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (10, N'AR', N'Argentina')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (11, N'AM', N'Armenia')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (12, N'AW', N'Aruba')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (13, N'AU', N'Australia')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (14, N'AT', N'Austria')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (15, N'AZ', N'Azerbaijan')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (16, N'BS', N'Bahamas')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (17, N'BH', N'Bahrain')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (18, N'BD', N'Bangladesh')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (19, N'BB', N'Barbados')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (20, N'BY', N'Belarus')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (21, N'BE', N'Belgium')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (22, N'BZ', N'Belize')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (23, N'BJ', N'Benin')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (24, N'BM', N'Bermuda')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (25, N'BT', N'Bhutan')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (26, N'BO', N'Bolivia')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (27, N'BA', N'Bosnia and Herzegovina')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (28, N'BW', N'Botswana')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (29, N'BV', N'Bouvet Island')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (30, N'BR', N'Brazil')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (31, N'IO', N'British Indian Ocean Territory')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (32, N'BN', N'Brunei Darussalam')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (33, N'BG', N'Bulgaria')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (34, N'BF', N'Burkina Faso')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (35, N'BI', N'Burundi')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (36, N'KH', N'Cambodia')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (37, N'CM', N'Cameroon')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (38, N'CA', N'Canada')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (39, N'CV', N'Cape Verde')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (40, N'KY', N'Cayman Islands')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (41, N'CF', N'Central African Republic')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (42, N'TD', N'Chad')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (43, N'CL', N'Chile')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (44, N'CN', N'China')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (45, N'CX', N'Christmas Island')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (46, N'CC', N'Cocos (Keeling) Islands')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (47, N'CO', N'Colombia')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (48, N'KM', N'Comoros')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (49, N'CG', N'Congo')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (50, N'CK', N'Cook Islands')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (51, N'CR', N'Costa Rica')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (52, N'HR', N'Croatia (Hrvatska)')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (53, N'CU', N'Cuba')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (54, N'CY', N'Cyprus')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (55, N'CZ', N'Czech Republic')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (56, N'DK', N'Denmark')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (57, N'DJ', N'Djibouti')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (58, N'DM', N'Dominica')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (59, N'DO', N'Dominican Republic')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (60, N'TP', N'East Timor')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (61, N'EC', N'Ecuador')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (62, N'EG', N'Egypt')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (63, N'SV', N'El Salvador')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (64, N'GQ', N'Equatorial Guinea')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (65, N'ER', N'Eritrea')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (66, N'EE', N'Estonia')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (67, N'ET', N'Ethiopia')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (68, N'FK', N'Falkland Islands (Malvinas)')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (69, N'FO', N'Faroe Islands')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (70, N'FJ', N'Fiji')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (71, N'FI', N'Finland')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (72, N'FR', N'France')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (73, N'FX', N'France, Metropolitan')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (74, N'GF', N'French Guiana')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (75, N'PF', N'French Polynesia')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (76, N'TF', N'French Southern Territories')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (77, N'GA', N'Gabon')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (78, N'GM', N'Gambia')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (79, N'GE', N'Georgia')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (80, N'DE', N'Germany')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (81, N'GH', N'Ghana')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (82, N'GI', N'Gibraltar')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (83, N'GK', N'Guernsey')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (84, N'GR', N'Greece')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (85, N'GL', N'Greenland')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (86, N'GD', N'Grenada')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (87, N'GP', N'Guadeloupe')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (88, N'GU', N'Guam')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (89, N'GT', N'Guatemala')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (90, N'GN', N'Guinea')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (91, N'GW', N'Guinea-Bissau')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (92, N'GY', N'Guyana')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (93, N'HT', N'Haiti')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (94, N'HM', N'Heard and Mc Donald Islands')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (95, N'HN', N'Honduras')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (96, N'HK', N'Hong Kong')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (97, N'HU', N'Hungary')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (98, N'IS', N'Iceland')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (99, N'IN', N'India')
GO
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (100, N'IM', N'Isle of Man')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (101, N'ID', N'Indonesia')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (102, N'IR', N'Iran (Islamic Republic of)')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (103, N'IQ', N'Iraq')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (104, N'IE', N'Ireland')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (105, N'IL', N'Israel')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (106, N'IT', N'Italy')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (107, N'CI', N'Ivory Coast')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (108, N'JE', N'Jersey')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (109, N'JM', N'Jamaica')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (110, N'JP', N'Japan')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (111, N'JO', N'Jordan')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (112, N'KZ', N'Kazakhstan')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (113, N'KE', N'Kenya')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (114, N'KI', N'Kiribati')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (115, N'KP', N'Korea, Democratic People''s Republic of')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (116, N'KR', N'Korea, Republic of')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (117, N'XK', N'Kosovo')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (118, N'KW', N'Kuwait')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (119, N'KG', N'Kyrgyzstan')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (120, N'LA', N'Lao People''s Democratic Republic')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (121, N'LV', N'Latvia')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (122, N'LB', N'Lebanon')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (123, N'LS', N'Lesotho')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (124, N'LR', N'Liberia')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (125, N'LY', N'Libyan Arab Jamahiriya')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (126, N'LI', N'Liechtenstein')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (127, N'LT', N'Lithuania')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (128, N'LU', N'Luxembourg')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (129, N'MO', N'Macau')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (130, N'MK', N'Macedonia')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (131, N'MG', N'Madagascar')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (132, N'MW', N'Malawi')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (133, N'MY', N'Malaysia')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (134, N'MV', N'Maldives')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (135, N'ML', N'Mali')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (136, N'MT', N'Malta')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (137, N'MH', N'Marshall Islands')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (138, N'MQ', N'Martinique')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (139, N'MR', N'Mauritania')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (140, N'MU', N'Mauritius')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (141, N'TY', N'Mayotte')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (142, N'MX', N'Mexico')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (143, N'FM', N'Micronesia, Federated States of')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (144, N'MD', N'Moldova, Republic of')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (145, N'MC', N'Monaco')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (146, N'MN', N'Mongolia')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (147, N'ME', N'Montenegro')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (148, N'MS', N'Montserrat')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (149, N'MA', N'Morocco')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (150, N'MZ', N'Mozambique')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (151, N'MM', N'Myanmar')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (152, N'NA', N'Namibia')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (153, N'NR', N'Nauru')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (154, N'NP', N'Nepal')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (155, N'NL', N'Netherlands')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (156, N'AN', N'Netherlands Antilles')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (157, N'NC', N'New Caledonia')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (158, N'NZ', N'New Zealand')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (159, N'NI', N'Nicaragua')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (160, N'NE', N'Niger')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (161, N'NG', N'Nigeria')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (162, N'NU', N'Niue')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (163, N'NF', N'Norfolk Island')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (164, N'MP', N'Northern Mariana Islands')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (165, N'NO', N'Norway')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (166, N'OM', N'Oman')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (167, N'PK', N'Pakistan')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (168, N'PW', N'Palau')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (169, N'PS', N'Palestine')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (170, N'PA', N'Panama')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (171, N'PG', N'Papua New Guinea')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (172, N'PY', N'Paraguay')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (173, N'PE', N'Peru')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (174, N'PH', N'Philippines')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (175, N'PN', N'Pitcairn')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (176, N'PL', N'Poland')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (177, N'PT', N'Portugal')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (178, N'PR', N'Puerto Rico')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (179, N'QA', N'Qatar')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (180, N'RE', N'Reunion')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (181, N'RO', N'Romania')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (182, N'RU', N'Russian Federation')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (183, N'RW', N'Rwanda')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (184, N'KN', N'Saint Kitts and Nevis')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (185, N'LC', N'Saint Lucia')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (186, N'VC', N'Saint Vincent and the Grenadines')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (187, N'WS', N'Samoa')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (188, N'SM', N'San Marino')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (189, N'ST', N'Sao Tome and Principe')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (190, N'SA', N'Saudi Arabia')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (191, N'SN', N'Senegal')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (192, N'RS', N'Serbia')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (193, N'SC', N'Seychelles')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (194, N'SL', N'Sierra Leone')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (195, N'SG', N'Singapore')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (196, N'SK', N'Slovakia')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (197, N'SI', N'Slovenia')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (198, N'SB', N'Solomon Islands')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (199, N'SO', N'Somalia')
GO
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (200, N'ZA', N'South Africa')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (201, N'GS', N'South Georgia South Sandwich Islands')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (202, N'SS', N'South Sudan')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (203, N'ES', N'Spain')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (204, N'LK', N'Sri Lanka')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (205, N'SH', N'St. Helena')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (206, N'PM', N'St. Pierre and Miquelon')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (207, N'SD', N'Sudan')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (208, N'SR', N'Suriname')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (209, N'SJ', N'Svalbard and Jan Mayen Islands')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (210, N'SZ', N'Swaziland')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (211, N'SE', N'Sweden')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (212, N'CH', N'Switzerland')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (213, N'SY', N'Syrian Arab Republic')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (214, N'TW', N'Taiwan')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (215, N'TJ', N'Tajikistan')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (216, N'TZ', N'Tanzania, United Republic of')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (217, N'TH', N'Thailand')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (218, N'TG', N'Togo')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (219, N'TK', N'Tokelau')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (220, N'TO', N'Tonga')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (221, N'TT', N'Trinidad and Tobago')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (222, N'TN', N'Tunisia')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (223, N'TR', N'Turkey')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (224, N'TM', N'Turkmenistan')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (225, N'TC', N'Turks and Caicos Islands')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (226, N'TV', N'Tuvalu')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (227, N'UG', N'Uganda')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (228, N'UA', N'Ukraine')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (229, N'AE', N'United Arab Emirates')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (230, N'GB', N'United Kingdom')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (231, N'US', N'United States')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (232, N'UM', N'United States minor outlying islands')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (233, N'UY', N'Uruguay')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (234, N'UZ', N'Uzbekistan')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (235, N'VU', N'Vanuatu')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (236, N'VA', N'Vatican City State')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (237, N'VE', N'Venezuela')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (238, N'VN', N'Vietnam')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (239, N'VG', N'Virgin Islands (British)')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (240, N'VI', N'Virgin Islands (U.S.)')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (241, N'WF', N'Wallis and Futuna Islands')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (242, N'EH', N'Western Sahara')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (243, N'YE', N'Yemen')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (244, N'ZR', N'Zaire')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (245, N'ZM', N'Zambia')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Country_name]) VALUES (246, N'ZW', N'Zimbabwe')
SET IDENTITY_INSERT [dbo].[Countries] OFF
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([Id], [Name], [Brand], [Model_Year], [Price], [Img]) VALUES (1, N'kala1', N'brand1 ', NULL, 500000, N'e (1).jpg')
INSERT [dbo].[Product] ([Id], [Name], [Brand], [Model_Year], [Price], [Img]) VALUES (2, N'kala2', N'brand2', NULL, 250000, N'e (2).jpg')
INSERT [dbo].[Product] ([Id], [Name], [Brand], [Model_Year], [Price], [Img]) VALUES (3, N'kala3', N'brand3', NULL, 2000000, N'e (3).jpg')
INSERT [dbo].[Product] ([Id], [Name], [Brand], [Model_Year], [Price], [Img]) VALUES (4, N'kala4', N'brand4', NULL, 650000, N'e (4).jpg')
INSERT [dbo].[Product] ([Id], [Name], [Brand], [Model_Year], [Price], [Img]) VALUES (5, N'kala5', N'brand5', NULL, 650000, N'e (5).jpg')
INSERT [dbo].[Product] ([Id], [Name], [Brand], [Model_Year], [Price], [Img]) VALUES (6, N'kala6', N'brand6', NULL, 500000, N'e (6).jpg')
INSERT [dbo].[Product] ([Id], [Name], [Brand], [Model_Year], [Price], [Img]) VALUES (7, N'kala7', N'brand7', NULL, 250000, N'e (7).jpg')
INSERT [dbo].[Product] ([Id], [Name], [Brand], [Model_Year], [Price], [Img]) VALUES (8, N'kala8', N'brand8', NULL, 2000000, N'e (8).jpg')
INSERT [dbo].[Product] ([Id], [Name], [Brand], [Model_Year], [Price], [Img]) VALUES (9, N'kala9', N'brand9', NULL, 650000, N'e (9).jpg')
INSERT [dbo].[Product] ([Id], [Name], [Brand], [Model_Year], [Price], [Img]) VALUES (10, N'kala10', N'brand10', NULL, 650000, N'e (2).jpg')
INSERT [dbo].[Product] ([Id], [Name], [Brand], [Model_Year], [Price], [Img]) VALUES (11, N'kala11', N'brand1 ', NULL, 500000, N'e (3).jpg')
INSERT [dbo].[Product] ([Id], [Name], [Brand], [Model_Year], [Price], [Img]) VALUES (12, N'kala12', N'brand2', NULL, 250000, N'e (4).jpg')
INSERT [dbo].[Product] ([Id], [Name], [Brand], [Model_Year], [Price], [Img]) VALUES (13, N'kala13', N'brand3', NULL, 2000000, N'e (1).jpg')
INSERT [dbo].[Product] ([Id], [Name], [Brand], [Model_Year], [Price], [Img]) VALUES (14, N'kala14', N'brand4', NULL, 650000, N'e (6).jpg')
INSERT [dbo].[Product] ([Id], [Name], [Brand], [Model_Year], [Price], [Img]) VALUES (15, N'kala15', N'brand5', NULL, 650000, N'e (7).jpg')
INSERT [dbo].[Product] ([Id], [Name], [Brand], [Model_Year], [Price], [Img]) VALUES (16, N'kala16', N'brand6', NULL, 500000, N'e (8).jpg')
INSERT [dbo].[Product] ([Id], [Name], [Brand], [Model_Year], [Price], [Img]) VALUES (17, N'kala17', N'brand7', NULL, 250000, N'e (1).jpg')
INSERT [dbo].[Product] ([Id], [Name], [Brand], [Model_Year], [Price], [Img]) VALUES (18, N'kala18', N'brand8', NULL, 2000000, N'e (9).jpg')
INSERT [dbo].[Product] ([Id], [Name], [Brand], [Model_Year], [Price], [Img]) VALUES (19, N'kala19', N'brand9', NULL, 650000, N'e (8).jpg')
INSERT [dbo].[Product] ([Id], [Name], [Brand], [Model_Year], [Price], [Img]) VALUES (20, N'kala20', N'brand10', NULL, 650000, N'e (1).jpg')
SET IDENTITY_INSERT [dbo].[Product] OFF
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([Id], [Fname], [Lname], [Mobile], [Email], [Address], [Password], [VoucherBid], [RealBid], [LastLogin], [CountryId], [HideLocation], [Image]) VALUES (2, N'zahra', N'hvn11', N'09361997137    ', N'zahra11@gmail.com', N'tehran', N'123', 20, 11, NULL, 1, 0, NULL)
INSERT [dbo].[User] ([Id], [Fname], [Lname], [Mobile], [Email], [Address], [Password], [VoucherBid], [RealBid], [LastLogin], [CountryId], [HideLocation], [Image]) VALUES (3, N'ew', N'qwr', N'23232320930    ', N'mgn@gmail.com', N'b re e', N'3244185981728979115075721453575112', 0, 0, NULL, 2, 0, NULL)
INSERT [dbo].[User] ([Id], [Fname], [Lname], [Mobile], [Email], [Address], [Password], [VoucherBid], [RealBid], [LastLogin], [CountryId], [HideLocation], [Image]) VALUES (4, N'zahra', N'hvn', N'15451          ', N'zahra@gmail.com', N'defwej fkwjle', N'2539823711213321981162281981622129157160196', 0, 619, CAST(N'2019-05-12T10:20:00.000' AS DateTime), 1, 1, N'15.png')
INSERT [dbo].[User] ([Id], [Fname], [Lname], [Mobile], [Email], [Address], [Password], [VoucherBid], [RealBid], [LastLogin], [CountryId], [HideLocation], [Image]) VALUES (6, N'dfs', N'sdf', N'09305273237    ', N'sdfsdf', N'adwfwef', N'56581418015210272141575592534822911468', 0, 0, CAST(N'2019-05-14T12:12:46.253' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[User] ([Id], [Fname], [Lname], [Mobile], [Email], [Address], [Password], [VoucherBid], [RealBid], [LastLogin], [CountryId], [HideLocation], [Image]) VALUES (7, N'sdfsdfsdf', N'sdfsdf', N'23232320930    ', N'sdf', N'sdfsdfsdf', N'18915419056831262059510124183823858206161', 0, 0, CAST(N'2019-05-14T12:24:06.753' AS DateTime), NULL, 0, N'D:\Projects\newbidfinal-master\App_QuiBids\Content\img\avatar\10.png')
SET IDENTITY_INSERT [dbo].[User] OFF
ALTER TABLE [dbo].[Auction] ADD  CONSTRAINT [DF_Auction_StartStatus]  DEFAULT ((0)) FOR [StartStatus]
GO
ALTER TABLE [dbo].[Auction] ADD  CONSTRAINT [DF_Auction_IsClose]  DEFAULT ((0)) FOR [IsClose]
GO
ALTER TABLE [dbo].[Countries] ADD  DEFAULT ('') FOR [CountryCode]
GO
ALTER TABLE [dbo].[Countries] ADD  DEFAULT ('') FOR [Country_name]
GO
ALTER TABLE [dbo].[Auction]  WITH CHECK ADD  CONSTRAINT [FK_Auction_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([Id])
GO
ALTER TABLE [dbo].[Auction] CHECK CONSTRAINT [FK_Auction_Product]
GO
ALTER TABLE [dbo].[Auction]  WITH CHECK ADD  CONSTRAINT [FK_Auction_User] FOREIGN KEY([Current_UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Auction] CHECK CONSTRAINT [FK_Auction_User]
GO
ALTER TABLE [dbo].[AuctionLogs]  WITH CHECK ADD  CONSTRAINT [FK_LogsAuction_Auction] FOREIGN KEY([AuctionId])
REFERENCES [dbo].[Auction] ([Id])
GO
ALTER TABLE [dbo].[AuctionLogs] CHECK CONSTRAINT [FK_LogsAuction_Auction]
GO
ALTER TABLE [dbo].[AuctionLogs]  WITH CHECK ADD  CONSTRAINT [FK_LogsAuction_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[AuctionLogs] CHECK CONSTRAINT [FK_LogsAuction_User]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Countries] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Countries] ([Id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Countries]
GO
