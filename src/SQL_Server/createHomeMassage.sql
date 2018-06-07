CREATE TABLE Cliente (
	Id_Cliente int NOT NULL IDENTITY(1,1),
	Username varchar(10) NOT NULL,
	Password varchar(50) NOT NULL,
	Nome varchar(50) NOT NULL,
	Email varchar(50) NOT NULL,
	Contacto int NOT NULL,
	Numero_Contribuinte varchar(9) NOT NULL,
	CONSTRAINT PK_Client PRIMARY KEY (Id_Cliente)
);

CREATE TABLE Funcionario (
	Id_Funcionario int NOT NULL IDENTITY(1,1),
	Username varchar(10) NOT NULL,
	Password varchar(50) NOT NULL,
	Nome varchar(50) NOT NULL,
	Email varchar(50) NOT NULL,
	Estado bit NOT NULL,
	CONSTRAINT PK_Funcionario PRIMARY KEY (Id_Funcionario)
);

CREATE TABLE Massagem (
	Id_Massagem int NOT NULL IDENTITY(1,1),
	Nome varchar(15) NOT NULL,
	Preco money NOT NULL,
	Duracao int NOT NULL,
	Descricao varchar(250) NOT NULL,
	Imagem image NOT NULL,
	CONSTRAINT PK_Massagem PRIMARY KEY (Id_Massagem)
);

CREATE TABLE Servico (
	Id_Servico int NOT NULL IDENTITY(1,1),
	Cliente int NOT NULL,
	Funcionario int NOT NULL,
	Massagem int NOT NULL,
	Data datetime NOT NULL,
	Cartao_Credito varchar(19) NOT NULL,
	Estado bit NOT NULL,
	Endereco varchar(50) NOT NULL,
	Codigo_Postal varchar(8) NOT NULL,
	CONSTRAINT PK_Servico PRIMARY KEY (Id_Servico,Cliente,Funcionario,Massagem),
	CONSTRAINT FK_Cliente FOREIGN KEY (Cliente) REFERENCES Cliente(Id_Cliente) ON UPDATE CASCADE,
	CONSTRAINT FK_Funcionario FOREIGN KEY (Funcionario) REFERENCES Funcionario(Id_Funcionario) ON UPDATE CASCADE,
	CONSTRAINT FK_Massagem FOREIGN KEY (Massagem) REFERENCES Massagem(Id_Massagem) ON UPDATE CASCADE
);

