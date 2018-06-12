use homemassage;

INSERT INTO Massagem (Nome,Preco,Duracao,Descricao,Imagem)
VALUES
('Relaxante',40,45,'',NULL),
('Profunda',60,60,'',NULL),
('Sueca',40,45,'',NULL),
('Desportivo',70,60,'',NULL),
('Pré-Natal',90,90,'',NULL);

INSERT INTO Funcionario (Username,Password,Nome,Email,Estado,Role)
VALUES
('admin','7260bedd7813b31b27c204450a065136','admin','homemassage@hotmail.com',1,'admin'),
('func002','168cef729f6c0bbb7c2f3bec98857975','func002','funcionario2@hotmail.com',1,'employee'),
('func003','b4fd4e310410cfa6c31661e66c425345','func003','funcionario3@hotmail.com',1,'employee'),
('func004','2698a3f9752cb7a4a438e658a479a569','func004','funcionario4@hotmail.com',0,'employee'),
('func005','f3ed85b558e1f3f1c72e68f52286de1a','func005','funcionario5@hotmail.com',1,'employee');

INSERT INTO Cliente (Username,Password,Nome,Email,Contacto,Numero_Contribuinte,Data_Nascimento,Role)
VALUES
('uti001','33e83cefe28373c7232b0eca7f3f9dcd','Utilizador 1','a77617@alunos.uminho.pt',926231675,'502304812','1997-10-09','user'),
('uti002','86b34ab0e5aeaae8a0545c05658e90ec','Utilizador 2','a78565@alunos.uminho.pt',966765650,'508033519','1998-04-25','user'),
('uti003','ae4e46dd21657419bac798850428fbcd','Utilizador 3','a77672@alunos.uminho.pt',917390750,'980414679','1993-08-29','user'),
('uti004','8f1093c26f5125b00bca664dd104e551','Utilizador 4','a78085@alunos.uminho.pt',915049712,'508451639','1994-12-25','user');

INSERT INTO Servico (Cliente,Funcionario,Massagem,Data,Cartao_Credito,Estado,Endereco,Codigo_Postal)
VALUES
(3,2,1,'2018-07-01 12:00:00','401200103714111123',0,'Rua Sao Domingos, Braga','4710-409'),
(1,5,2,'2018-07-23 16:30:00','507860187000012785',0,'Rua das Cavadas, Barrosas','4450-150'),
(1,3,2,'2018-05-01 10:30:00','761450103714111123',1,'Rua Martins Sarmento Sao Victor, Braga','4710-406'),
(4,5,4,'2018-05-09 11:15:00','3841001111222233334',1,'Acesso Entre As Ruas Bernardo Sequeira e Martins Sarmento Sao Victor,Braga','4710-406'),
(2,5,1,'2018-03-01 12:00:00','401200103714111123',1,'Rua Sao Domingos, Braga','4710-409'),
(4,3,2,'2018-10-25 09:30:00','507860187000012785',0,'Rua das Cavadas, Barrosas','4450-150'),
(3,3,2,'2018-02-01 13:30:00','761450103714111123',1,'Rua Martins Sarmento Sao Victor, Braga','4710-406'),
(2,5,4,'2018-06-15 08:15:00','3841001111222233334',0,'Acesso Entre As Ruas Bernardo Sequeira e Martins Sarmento Sao Victor,Braga','4710-406');