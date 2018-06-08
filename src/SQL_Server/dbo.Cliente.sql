CREATE TABLE [dbo].[Cliente] (
    [Id_Cliente]          INT          IDENTITY (1, 1) NOT NULL,
    [Username]            VARCHAR (10) NOT NULL,
    [Password]            VARCHAR (50) NOT NULL,
    [Nome]                VARCHAR (50) NOT NULL,
    [Email]               VARCHAR (50) NOT NULL,
    [Contacto]            INT          NOT NULL,
    [Numero_Contribuinte] VARCHAR (9)  NOT NULL,
    [Role]                VARCHAR (60) DEFAULT ('user') NULL,
    CONSTRAINT [PK_Client] PRIMARY KEY CLUSTERED ([Id_Cliente] ASC)
);

