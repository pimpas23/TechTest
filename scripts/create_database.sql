USE master;
GO

IF OBJECT_ID('CallRecordDb', 'U') IS NULL
    CREATE DATABASE CallRecordDb;
GO

USE CallRecordDb;
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