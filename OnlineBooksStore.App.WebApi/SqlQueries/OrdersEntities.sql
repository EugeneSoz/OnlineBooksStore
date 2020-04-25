CREATE PROCEDURE CreateBaseEntries AS
BEGIN
    CREATE TABLE dbo.Publishers
    (
        Id      BIGINT IDENTITY (1,1)
            CONSTRAINT PK_Publishers PRIMARY KEY,
        Name    NVARCHAR(100),
        Country NVARCHAR(100),
        Created DATETIME NOT NULL
            CONSTRAINT DFT_created DEFAULT (GETDATE()),
        Updated DATETIME NULL
    );

    CREATE TABLE dbo.Categories
    (
        Id              BIGINT IDENTITY (1, 1)
            CONSTRAINT PK_Categories PRIMARY KEY,
        Name            NVARCHAR(100),
        ParentId        BIGINT NULL
            CONSTRAINT FK_Categories_Categories_ParentId FOREIGN KEY
                REFERENCES Categories (Id),
        ParentAndChildName NVARCHAR(300),
        Created         DATETIME NOT NULL
            CONSTRAINT DFT_created DEFAULT (GETDATE()),
        Updated         DATETIME NULL
    );

    CREATE TABLE dbo.Books
    (
        Id            BIGINT IDENTITY (1,1)
            CONSTRAINT PK_Books PRIMARY KEY,
        Title         NVARCHAR(400),
        Authors       NVARCHAR(100),
        Year          INT           NOT NULL
            CONSTRAINT DFT_year DEFAULT (YEAR(GETDATE())),
        Language      NVARCHAR(50)
            CONSTRAINT DFT_language DEFAULT ('Russian'),
        PageCount     INT           NOT NULL,
        Description   NVARCHAR(MAX),
        PurchasePrice DECIMAL(8, 2) NOT NULL,
        RetailPrice   DECIMAL(8, 2) NOT NULL,
        BookCover     NVARCHAR(MAX),
        CategoryId    BIGINT
            CONSTRAINT FK_Books_Categories_CategoryId
                REFERENCES Categories (Id)
                ON DELETE SET NULL,
        PublisherId   BIGINT
            CONSTRAINT FK_Books_Publishers_PublisherId
                REFERENCES Publishers (Id)
                ON DELETE SET NULL,
        Created       DATETIME      NOT NULL
            CONSTRAINT DFT_created DEFAULT (GETDATE()),
        Updated       DATETIME      NULL
    )
END
GO
CREATE PROCEDURE BooksStoreIndexCreation AS
BEGIN

END
GO
CREATE PROCEDURE BooksStoreIndexAndConstraintsDeletion AS
BEGIN

END
GO



GO

CREATE INDEX IX_Categories_ParentCategoryID
    ON Categories (ParentId)
GO


GO

CREATE INDEX IX_Books_CategoryID
    ON Books (CategoryID)
GO

CREATE INDEX IX_Books_PublisherID
    ON Books (PublisherID)
GO

