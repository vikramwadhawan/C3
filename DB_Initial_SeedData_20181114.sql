SET IDENTITY_INSERT [dbo].[Product] ON 

GO
INSERT [dbo].[Product] ([ProductId], [Name], [Description], [IsActive], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn]) VALUES (100, N'RMO', N'Web Application', 1, 1, CAST(N'2018-05-31 11:13:32.920' AS DateTime), NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Product] OFF
GO

DECLARE @RC int
DECLARE @OrganizationId nvarchar(255)='1abcaee7-4b3a-415f-9884-4c6f3ebca539'
DECLARE @Name nvarchar(255) ='CGOTek'
DECLARE @RootUrl nvarchar(max)='https://cgotek.sharepoint.com'
DECLARE @RMOLicences int = 5

-- TODO: Set parameter values here.

EXECUTE @RC = [dbo].[UspCreateOrganization] 
   @OrganizationId
  ,@Name
  ,@RootUrl
  ,@RMOLicences
GO




SET IDENTITY_INSERT [dbo].[ErrorMessage] ON 

GO
INSERT [dbo].[ErrorMessage] ([ErrorMessageId], [ErrorKey], [Message], [Description], [IsActive], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn]) VALUES (100, N'UserNotRegistered', N'Your account is not registered to use this product.  Please contact your administrators for assistance.', N'', 1, 1, CAST(N'2018-05-29 18:23:31.327' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[ErrorMessage] ([ErrorMessageId], [ErrorKey], [Message], [Description], [IsActive], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn]) VALUES (101, N'InActiveUser', N'Your account is not activated for this product.  Please contact your administrators for assistance.', N'', 1, 1, CAST(N'2018-05-29 18:23:31.327' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[ErrorMessage] ([ErrorMessageId], [ErrorKey], [Message], [Description], [IsActive], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn]) VALUES (102, N'LoggedIn', N'Successfully Logged in', N'', 1, 1, CAST(N'2018-05-29 18:23:31.327' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[ErrorMessage] ([ErrorMessageId], [ErrorKey], [Message], [Description], [IsActive], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn]) VALUES (106, N'OrganizationNotRegistered', N'We are unable to recognize your organization for this product.  Please contact your administrators for assistance.', N'', 1, 1, CAST(N'2018-05-29 00:00:00.000' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[ErrorMessage] ([ErrorMessageId], [ErrorKey], [Message], [Description], [IsActive], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn]) VALUES (108, N'UserCreated', N'User Created Successfully.', N'', 1, 1, CAST(N'2018-05-29 00:00:00.000' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[ErrorMessage] ([ErrorMessageId], [ErrorKey], [Message], [Description], [IsActive], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn]) VALUES (110, N'LicenseFinished', N'Your organization has reached the number of licenses purchased for this product.  Please contact your administrators for assistance.', N'', 1, 1, CAST(N'2018-05-29 00:00:00.000' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[ErrorMessage] ([ErrorMessageId], [ErrorKey], [Message], [Description], [IsActive], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn]) VALUES (111, N'NoProductLicense', N'Your organization is not licensed to use this product.', N'Your organization is not licensed to use this product.', 1, 1, CAST(N'2018-09-18 16:58:34.530' AS DateTime), NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[ErrorMessage] OFF
GO
