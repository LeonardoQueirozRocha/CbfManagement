IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Times] (
    [Id] uniqueidentifier NOT NULL,
    [Nome] varchar(200) NOT NULL,
    [Localidade] varchar(255) NOT NULL,
    [DataCadastro] datetime2 NOT NULL,
    CONSTRAINT [PK_Times] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Jogadores] (
    [Id] uniqueidentifier NOT NULL,
    [TimeId] uniqueidentifier NOT NULL,
    [Nome] varchar(200) NOT NULL,
    [DataNascimento] datetime NOT NULL,
    [Pais] varchar(200) NOT NULL,
    [DataCadastro] datetime2 NOT NULL,
    CONSTRAINT [PK_Jogadores] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Jogadores_Times_TimeId] FOREIGN KEY ([TimeId]) REFERENCES [Times] ([Id])
);
GO

CREATE TABLE [Transferencias] (
    [Id] uniqueidentifier NOT NULL,
    [TimeOrigemId] uniqueidentifier NOT NULL,
    [TimeDestinoId] uniqueidentifier NOT NULL,
    [JogadorId] uniqueidentifier NOT NULL,
    [DataCadastro] datetime2 NOT NULL,
    [Valor] decimal(10,5) NOT NULL,
    CONSTRAINT [PK_Transferencias] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Transferencias_Jogadores_JogadorId] FOREIGN KEY ([JogadorId]) REFERENCES [Jogadores] ([Id]),
    CONSTRAINT [FK_Transferencias_Times_TimeOrigemId] FOREIGN KEY ([TimeOrigemId]) REFERENCES [Times] ([Id])
);
GO

CREATE INDEX [IX_Jogadores_TimeId] ON [Jogadores] ([TimeId]);
GO

CREATE INDEX [IX_Transferencias_JogadorId] ON [Transferencias] ([JogadorId]);
GO

CREATE INDEX [IX_Transferencias_TimeOrigemId] ON [Transferencias] ([TimeOrigemId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220619224807_Initial', N'6.0.6');
GO

COMMIT;
GO

