USE master;
GO

IF OBJECT_ID('CallRecordIntegrationTestsDb', 'U') IS NOT NULL
    BEGIN
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
VALUES 	('C5DA9724701EEBBA95CA2CC5617BA93E5', '441216000000', '448000000000', '2016-08-16T14:21:33', 43, 1, 2, 0),
		('E9F1E149C3BDA758C21F45F7AEDDAF60', '441234567890', '447890123456', '2016-08-17T08:45:21', 85, 0.4, 1, 0),
		('F8C2C2FAD89B14B248C79837A2023A7E', '441234567891', '447890123457', '2016-08-18T16:32:10', 120, 0.8, 2, 0),
		('A6C17CEBE51B52D3A877F547F9B9EE36', '441234567892', '447890123458', '2016-08-19T12:15:42', 60, 3, 1, 0),
		('E9F1E149C3BDA758C21F45F7AEDDAF61', '441234567890', '447890123456', '2016-08-18T08:45:21', 85, 0.8, 1, 0);
GO