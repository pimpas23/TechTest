USE master;
GO

IF DB_ID('CallRecordDb') IS NOT NULL
BEGIN
    ALTER DATABASE CallRecordDb SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE CallRecordDb;
END
GO



CREATE DATABASE CallRecordDb;
GO

USE CallRecordDb;
GO

IF OBJECT_ID('dbo.CallDetailRecords', 'U') IS NOT NULL
    DROP TABLE dbo.CallDetailRecords;
GO

CREATE TABLE [dbo].[CallDetailRecords] (
    [Id] NVARCHAR(40) NOT NULL,
    [CallerNumber] NVARCHAR(15) NULL,
    [RecipientNumber] NVARCHAR(15) NULL,
    [CallDateEndTime] DATETIME2(7) NOT NULL,
    [CallDuration] INT NOT NULL,
    [Cost] FLOAT NOT NULL,
    [TypeOfCall] INT NOT NULL,
    [Currency] INT NOT NULL,
    CONSTRAINT [PK_CallDetailRecords] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO