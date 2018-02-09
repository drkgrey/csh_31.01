CREATE TABLE [dbo].[EmployeeBase1] (
    [Id]           INT         IDENTITY (1, 1)   NOT NULL,
    [DepartmentId] INT           NOT NULL,
    [Name]         NVARCHAR (50) NULL,
    [Lastname]     NVARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
