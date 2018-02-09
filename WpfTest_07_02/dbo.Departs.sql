CREATE TABLE [dbo].[DepartmentsBase]
(
	[Id] INT IDENTITY(1, 1) NOT NULL,
	[DepartmentName] NVARCHAR(100) not null,
	CONSTRAINT[PK_dbo.Departments] PRIMARY KEY CLUSTERED([Id] ASC)
);
