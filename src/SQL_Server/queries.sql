USE HomeMassage;

SELECT * FROM Cliente;
SELECT * FROM Massagem;
SELECT * FROM Funcionario;

DELETE FROM Funcionario
	WHERE Id_Funcionario<=3;