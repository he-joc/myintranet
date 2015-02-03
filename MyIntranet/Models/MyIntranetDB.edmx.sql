
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 02/03/2015 09:26:22
-- Generated from EDMX file: C:\Users\joc.HARDING\Dropbox\DotNetProjects\MyIntranet\MyIntranet\Models\MyIntranetDB.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [MyIntranet];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_AppSectionConfig]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Configs] DROP CONSTRAINT [FK_AppSectionConfig];
GO
IF OBJECT_ID(N'[dbo].[FK_UserCommsUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserComms] DROP CONSTRAINT [FK_UserCommsUser];
GO
IF OBJECT_ID(N'[dbo].[FK_UserHrDependenciesUserHr]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserHrDependencies] DROP CONSTRAINT [FK_UserHrDependenciesUserHr];
GO
IF OBJECT_ID(N'[dbo].[FK_UserHrLeaveRequest]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LeaveRequests] DROP CONSTRAINT [FK_UserHrLeaveRequest];
GO
IF OBJECT_ID(N'[dbo].[FK_UserHrUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserHrs] DROP CONSTRAINT [FK_UserHrUser];
GO
IF OBJECT_ID(N'[dbo].[FK_usersDepartment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK_usersDepartment];
GO
IF OBJECT_ID(N'[dbo].[FK_UserSocialUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserSocials] DROP CONSTRAINT [FK_UserSocialUser];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[AppSections]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AppSections];
GO
IF OBJECT_ID(N'[dbo].[Configs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Configs];
GO
IF OBJECT_ID(N'[dbo].[Departments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Departments];
GO
IF OBJECT_ID(N'[dbo].[LeaveRequests]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LeaveRequests];
GO
IF OBJECT_ID(N'[dbo].[UserComms]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserComms];
GO
IF OBJECT_ID(N'[dbo].[UserHrDependencies]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserHrDependencies];
GO
IF OBJECT_ID(N'[dbo].[UserHrs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserHrs];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[UserSocials]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserSocials];
GO
IF OBJECT_ID(N'[MyIntranetDBStoreContainer].[OfficeClosures]', 'U') IS NOT NULL
    DROP TABLE [MyIntranetDBStoreContainer].[OfficeClosures];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Departments'
CREATE TABLE [dbo].[Departments] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [DepartmentId] int  NOT NULL,
    [Gender] nvarchar(max)  NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [DateOfBirth] datetime  NOT NULL,
    [UserName] nvarchar(max)  NOT NULL,
    [Active] bit  NOT NULL,
    [DateStarted] datetime  NOT NULL,
    [DateLeft] datetime  NULL,
    [DateLastLogin] datetime  NULL
);
GO

-- Creating table 'UserComms'
CREATE TABLE [dbo].[UserComms] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Extension] nvarchar(max)  NOT NULL,
    [DDI] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [User_Id] int  NOT NULL
);
GO

-- Creating table 'UserHrs'
CREATE TABLE [dbo].[UserHrs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ContractedHours] decimal(18,0)  NOT NULL,
    [Salary] decimal(18,0)  NOT NULL,
    [ShiftPattern] nvarchar(max)  NOT NULL,
    [LeaveThisYear] smallint  NOT NULL,
    [LeaveNextYear] smallint  NOT NULL,
    [LeaveSupervisor] int  NOT NULL,
    [User_Id] int  NOT NULL
);
GO

-- Creating table 'LeaveRequests'
CREATE TABLE [dbo].[LeaveRequests] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [StartDate] datetime  NOT NULL,
    [EndDate] datetime  NOT NULL,
    [StartHalfDay] nvarchar(max)  NULL,
    [EndHalfDay] nvarchar(max)  NULL,
    [HrApproved] bit  NULL,
    [SupervisorApproved] bit  NULL,
    [DateRequested] datetime  NOT NULL,
    [Days] decimal(18,0)  NOT NULL,
    [UserHrId] int  NOT NULL
);
GO

-- Creating table 'OfficeClosures'
CREATE TABLE [dbo].[OfficeClosures] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Date] datetime  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [BankHoliday] bit  NOT NULL
);
GO

-- Creating table 'UserSocials'
CREATE TABLE [dbo].[UserSocials] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Fact] nvarchar(max)  NULL,
    [User_Id] int  NOT NULL
);
GO

-- Creating table 'Configs'
CREATE TABLE [dbo].[Configs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Value] nvarchar(max)  NOT NULL,
    [AppSectionId] int  NOT NULL,
    [Description] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'AppSections'
CREATE TABLE [dbo].[AppSections] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'UserHrDependencies'
CREATE TABLE [dbo].[UserHrDependencies] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserHrId] int  NOT NULL,
    [UserHr_Id] int  NOT NULL
);
GO

-- Creating table 'Absences'
CREATE TABLE [dbo].[Absences] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserHrId] int  NOT NULL,
    [StartDate] datetime  NOT NULL,
    [EndDate] datetime  NOT NULL,
    [Days] decimal(18,0)  NOT NULL,
    [AbsenceReasonsId] int  NOT NULL
);
GO

-- Creating table 'AbsenceReasons'
CREATE TABLE [dbo].[AbsenceReasons] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Description] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'UserRoles'
CREATE TABLE [dbo].[UserRoles] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] int  NOT NULL,
    [Role_Id] int  NOT NULL
);
GO

-- Creating table 'Roles'
CREATE TABLE [dbo].[Roles] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [RoleName] nvarchar(max)  NOT NULL,
    [AppSectionId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Departments'
ALTER TABLE [dbo].[Departments]
ADD CONSTRAINT [PK_Departments]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserComms'
ALTER TABLE [dbo].[UserComms]
ADD CONSTRAINT [PK_UserComms]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserHrs'
ALTER TABLE [dbo].[UserHrs]
ADD CONSTRAINT [PK_UserHrs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'LeaveRequests'
ALTER TABLE [dbo].[LeaveRequests]
ADD CONSTRAINT [PK_LeaveRequests]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OfficeClosures'
ALTER TABLE [dbo].[OfficeClosures]
ADD CONSTRAINT [PK_OfficeClosures]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserSocials'
ALTER TABLE [dbo].[UserSocials]
ADD CONSTRAINT [PK_UserSocials]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Configs'
ALTER TABLE [dbo].[Configs]
ADD CONSTRAINT [PK_Configs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AppSections'
ALTER TABLE [dbo].[AppSections]
ADD CONSTRAINT [PK_AppSections]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserHrDependencies'
ALTER TABLE [dbo].[UserHrDependencies]
ADD CONSTRAINT [PK_UserHrDependencies]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Absences'
ALTER TABLE [dbo].[Absences]
ADD CONSTRAINT [PK_Absences]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AbsenceReasons'
ALTER TABLE [dbo].[AbsenceReasons]
ADD CONSTRAINT [PK_AbsenceReasons]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserRoles'
ALTER TABLE [dbo].[UserRoles]
ADD CONSTRAINT [PK_UserRoles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Roles'
ALTER TABLE [dbo].[Roles]
ADD CONSTRAINT [PK_Roles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [DepartmentId] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [FK_usersDepartment]
    FOREIGN KEY ([DepartmentId])
    REFERENCES [dbo].[Departments]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_usersDepartment'
CREATE INDEX [IX_FK_usersDepartment]
ON [dbo].[Users]
    ([DepartmentId]);
GO

-- Creating foreign key on [User_Id] in table 'UserHrs'
ALTER TABLE [dbo].[UserHrs]
ADD CONSTRAINT [FK_UserHrUser]
    FOREIGN KEY ([User_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserHrUser'
CREATE INDEX [IX_FK_UserHrUser]
ON [dbo].[UserHrs]
    ([User_Id]);
GO

-- Creating foreign key on [User_Id] in table 'UserSocials'
ALTER TABLE [dbo].[UserSocials]
ADD CONSTRAINT [FK_UserSocialUser]
    FOREIGN KEY ([User_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserSocialUser'
CREATE INDEX [IX_FK_UserSocialUser]
ON [dbo].[UserSocials]
    ([User_Id]);
GO

-- Creating foreign key on [User_Id] in table 'UserComms'
ALTER TABLE [dbo].[UserComms]
ADD CONSTRAINT [FK_UserCommsUser]
    FOREIGN KEY ([User_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserCommsUser'
CREATE INDEX [IX_FK_UserCommsUser]
ON [dbo].[UserComms]
    ([User_Id]);
GO

-- Creating foreign key on [UserHrId] in table 'LeaveRequests'
ALTER TABLE [dbo].[LeaveRequests]
ADD CONSTRAINT [FK_UserHrLeaveRequest]
    FOREIGN KEY ([UserHrId])
    REFERENCES [dbo].[UserHrs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserHrLeaveRequest'
CREATE INDEX [IX_FK_UserHrLeaveRequest]
ON [dbo].[LeaveRequests]
    ([UserHrId]);
GO

-- Creating foreign key on [AppSectionId] in table 'Configs'
ALTER TABLE [dbo].[Configs]
ADD CONSTRAINT [FK_AppSectionConfig]
    FOREIGN KEY ([AppSectionId])
    REFERENCES [dbo].[AppSections]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AppSectionConfig'
CREATE INDEX [IX_FK_AppSectionConfig]
ON [dbo].[Configs]
    ([AppSectionId]);
GO

-- Creating foreign key on [UserHr_Id] in table 'UserHrDependencies'
ALTER TABLE [dbo].[UserHrDependencies]
ADD CONSTRAINT [FK_UserHrDependenciesUserHr]
    FOREIGN KEY ([UserHr_Id])
    REFERENCES [dbo].[UserHrs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserHrDependenciesUserHr'
CREATE INDEX [IX_FK_UserHrDependenciesUserHr]
ON [dbo].[UserHrDependencies]
    ([UserHr_Id]);
GO

-- Creating foreign key on [UserHrId] in table 'Absences'
ALTER TABLE [dbo].[Absences]
ADD CONSTRAINT [FK_AbsenceUserHr]
    FOREIGN KEY ([UserHrId])
    REFERENCES [dbo].[UserHrs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AbsenceUserHr'
CREATE INDEX [IX_FK_AbsenceUserHr]
ON [dbo].[Absences]
    ([UserHrId]);
GO

-- Creating foreign key on [AbsenceReasonsId] in table 'Absences'
ALTER TABLE [dbo].[Absences]
ADD CONSTRAINT [FK_AbsenceReasonsAbsence]
    FOREIGN KEY ([AbsenceReasonsId])
    REFERENCES [dbo].[AbsenceReasons]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AbsenceReasonsAbsence'
CREATE INDEX [IX_FK_AbsenceReasonsAbsence]
ON [dbo].[Absences]
    ([AbsenceReasonsId]);
GO

-- Creating foreign key on [UserId] in table 'UserRoles'
ALTER TABLE [dbo].[UserRoles]
ADD CONSTRAINT [FK_UserRolesUser]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserRolesUser'
CREATE INDEX [IX_FK_UserRolesUser]
ON [dbo].[UserRoles]
    ([UserId]);
GO

-- Creating foreign key on [Role_Id] in table 'UserRoles'
ALTER TABLE [dbo].[UserRoles]
ADD CONSTRAINT [FK_UserRolesRoles]
    FOREIGN KEY ([Role_Id])
    REFERENCES [dbo].[Roles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserRolesRoles'
CREATE INDEX [IX_FK_UserRolesRoles]
ON [dbo].[UserRoles]
    ([Role_Id]);
GO

-- Creating foreign key on [AppSectionId] in table 'Roles'
ALTER TABLE [dbo].[Roles]
ADD CONSTRAINT [FK_RolesAppSection]
    FOREIGN KEY ([AppSectionId])
    REFERENCES [dbo].[AppSections]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RolesAppSection'
CREATE INDEX [IX_FK_RolesAppSection]
ON [dbo].[Roles]
    ([AppSectionId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------