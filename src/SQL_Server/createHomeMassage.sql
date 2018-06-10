CREATE DATABASE homemassage;
USE homemassage;

CREATE TABLE Cliente (
	Id_Cliente			INT NOT NULL IDENTITY(1,1),
	Username			VARCHAR(50) NOT NULL UNIQUE,
	Password			VARCHAR(50) NOT NULL,
	Nome				VARCHAR(50) NOT NULL,
	Email				VARCHAR(50) NOT NULL UNIQUE,
	Contacto			INT NOT NULL,
	Numero_Contribuinte VARCHAR(9) NOT NULL,
	Data_Nascimento		DATE NOT NULL,
	Role                VARCHAR (60) DEFAULT 'user' NULL,
	CONSTRAINT PK_Client PRIMARY KEY (Id_Cliente)
);

CREATE TABLE Funcionario (
	Id_Funcionario	INT NOT NULL IDENTITY(1,1),
	Username		VARCHAR(50) NOT NULL UNIQUE,
	Password		VARCHAR(50) NOT NULL,
	Nome			VARCHAR(50) NOT NULL,
	Email			VARCHAR(50) NOT NULL UNIQUE,
	Estado			BIT NOT NULL,
	Role            VARCHAR (60) DEFAULT 'employee' NULL,
	CONSTRAINT PK_Funcionario PRIMARY KEY (Id_Funcionario)
);

CREATE TABLE Massagem (
	Id_Massagem		INT NOT NULL IDENTITY(1,1),
	Nome			VARCHAR(15) NOT NULL UNIQUE,
	Preco			MONEY NOT NULL,
	Duracao			INT NOT NULL,
	Descricao		VARCHAR(250) NOT NULL,
	Imagem			IMAGE,
	CONSTRAINT PK_Massagem PRIMARY KEY (Id_Massagem)
);

CREATE TABLE Servico (
	Id_Servico			INT NOT NULL IDENTITY(1,1),
	Cliente				INT NOT NULL,
	Funcionario			INT NOT NULL,
	Massagem			INT NOT NULL,
	Data				DATETIME NOT NULL,
	Cartao_Credito		VARCHAR(19) NOT NULL,
	Estado				BIT NOT NULL,
	Endereco			VARCHAR(100) NOT NULL,
	Codigo_Postal		VARCHAR(8) NOT NULL,
	Ocorrencias			VARCHAR(250) NULL,
	Classificacao		INT NULL,	
	CONSTRAINT PK_Servico		PRIMARY KEY (Id_Servico),
	CONSTRAINT FK_Cliente		FOREIGN KEY (Cliente)		REFERENCES Cliente(Id_Cliente) ON UPDATE CASCADE,
	CONSTRAINT FK_Funcionario	FOREIGN KEY (Funcionario)	REFERENCES Funcionario(Id_Funcionario) ON UPDATE CASCADE,
	CONSTRAINT FK_Massagem		FOREIGN KEY (Massagem)		REFERENCES Massagem(Id_Massagem) ON UPDATE CASCADE
);
