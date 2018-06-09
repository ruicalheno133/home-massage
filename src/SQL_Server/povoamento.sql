INSERT INTO Massagem (Nome,Preco,Duracao,Descricao,Imagem)
VALUES
('Relaxante',40,45,'',NULL),
('Profunda',60,60,'',NULL),
('Sueca',40,45,'',NULL),
('Desportivo',70,60,'',NULL),
('Pré-Natal',90,90,'',NULL);

INSERT INTO Funcionario (Username,Password,Nome,Email,Estado,Role)
VALUES
('admin','7260bedd7813b31b27c204450a065136','admin','homemassage@hotmail.com',1,'employee'),
('func002','168cef729f6c0bbb7c2f3bec98857975','func002','funcionario2@hotmail.com',1,'employee'),
('func003','b4fd4e310410cfa6c31661e66c425345','func003','funcionario3@hotmail.com',1,'employee'),
('func004','2698a3f9752cb7a4a438e658a479a569','func004','funcionario4@hotmail.com',0,'employee'),
('func005',' f3ed85b558e1f3f1c72e68f52286de1a','func005','funcionario5@hotmail.com',1,'employee');


INSERT INTO Cliente (Username,Password,Nome,Email,Contacto,Numero_Contribuinte,Data_Nascimento,Role)
VALUES
('andre62','a4ad2a25dcf6ab4220606dce6982fd63','André Salgueiro','andre@gmail.com',926231675,'250704293','1996-07-09','user'),
('hugo68','12cbab1ee557242c13b6396ef164701e','Hugo Oliveira','oliveirahugo.97@gmail.com',966765650,'250704293','1996-07-09','user'),
('paula72','f1370fb56962fddf6ff58d97dbaa4224','Paula Pereira','pspereira_@hotmail.com',917390750,'250704293','1996-07-09','user'),
('rui133','b84e1c3b002c87fa8955ccc4c5e3defc','Rui Calheno','ruic66@hotmail.com',915049712,'250704293','1997-06-06','user');

INSERT INTO Servico (Id_Servico,Cliente,Funcionario,Massagem,Data,Cartao_Credito,Estado,Endereco,Codigo_Postal)
VALUES
(1,2,2,1,'2018-07-01 12:00:00','401200103714111123',0,'Rua Nova de Santa Cruz, n81','4710-409');