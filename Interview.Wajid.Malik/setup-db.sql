USE master
GO

CREATE DATABASE DB
GO

USE DB
GO

CREATE TABLE dbo.Transactions (
    AccountID VARCHAR(100),
    TransactionID VARCHAR(100),
    [TimeStamp] DATETIME,
    [Description] VARCHAR(500),
    Amount DECIMAL(12,2),
    Currency VARCHAR(3),
    TransactionType VARCHAR(100),
    TransactionCategory VARCHAR(100)
)
GO

CREATE PROCEDURE dbo.Transaction_Select
AS
    SELECT t.AccountID, t.TransactionID, t.[TimeStamp], t.[Description], t.Amount, t.Currency, t.TransactionType, t.TransactionCategory
    FROM dbo.Transactions t
GO

CREATE PROCEDURE dbo.Transaction_Delete
AS
    DELETE FROM dbo.Transactions
GO