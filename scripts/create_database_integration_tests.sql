USE master;
GO

IF OBJECT_ID('CallRecordIntegrationTestsDb', 'U') IS NOT NULL
    BEGIN
        ALTER DATABASE CallRecordIntegrationTestsDb SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
        DROP DATABASE CallRecordIntegrationTestsDb;
    END
GO

CREATE DATABASE CallRecordIntegrationTestsDb
ON
( NAME = 'CallRecordIntegrationTestsDb',
  FILENAME = '/var/opt/mssql/data/CallRecordIntegrationTestsDb.mdf',
  SIZE = 100MB,
  MAXSIZE = UNLIMITED,
  FILEGROWTH = 100MB
)
LOG ON
( NAME = 'CallRecordIntegrationTestsDb_log',
  FILENAME = '/var/opt/mssql/data/CallRecordIntegrationTestsDb.ldf',
  SIZE = 10MB,
  MAXSIZE = UNLIMITED,
  FILEGROWTH = 10MB
);
GO

USE CallRecordIntegrationTestsDb;
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

INSERT INTO [dbo].[CallDetailRecords] ([Id], [CallerNumber], [RecipientNumber], [CallDateEndTime], [CallDuration], [Cost], [TypeOfCall], [Currency])
VALUES ('C5DA9724701EEBBA95CA2CC5617BA93E4', '441216000000', '448000000000', '2016-08-16T14:21:33', 43, 0, 2, 0);
GO