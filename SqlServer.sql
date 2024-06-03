CREATE DATABASE bd_HxH
GO

USE bd_HxH
GO

-- Tablas -------------------------------------------------------
-- --------------------------------------------------------------
CREATE TABLE Cazadores (
	Id INT PRIMARY KEY IDENTITY(1,1),
	Nombre VARCHAR(100) NOT NULL,
	Edad INT NOT NULL,
)
GO

CREATE TABLE Nen (
	Id INT PRIMARY KEY IDENTITY(1,1),
	Nombre VARCHAR(100) NOT NULL,
	Descripcion VARCHAR(256) NOT NULL,
)
GO

CREATE TABLE CazadorNen (
	Id_Cazador INT NOT NULL,
	Id_Nen INT NOT NULL,
	PRIMARY KEY (Id_Cazador, Id_Nen),
	FOREIGN KEY (Id_Cazador) REFERENCES Cazadores(Id),
	FOREIGN KEY (Id_Nen) REFERENCES Nen(Id),
)
GO

DROP TABLE __EFMigrationsHistory
DROP TABLE CazadorNen
DROP TABLE Nen
DROP TABLE Cazadores

-- Data ---------------------------------------------------------
-- --------------------------------------------------------------
SET IDENTITY_INSERT Cazadores ON
GO
INSERT INTO Cazadores
	(Id, Nombre, Edad)
VALUES
	(1, 'Gon Freecss', 12),
	(2, 'Killua Zoldyck', 12),
	(3, 'Kurapika Kurta', 17),
	(4, 'Leorio Paladiknight', 19)
SET IDENTITY_INSERT Cazadores OFF
GO

SET IDENTITY_INSERT Nen ON
GO
INSERT INTO Nen
	(Id, Nombre, Descripcion)
VALUES
	(1, 'Intensificación', 'Si un estudiante aumenta la cantidad de agua en el vaso durante su prueba del agua, es de Intensificación'),
	(2, 'Transformación', 'Si un estudiante cambia el sabor del agua durante su prueba del agua es un Transformador'),
	(3, 'Materialización', 'Si un estudiante hace aparecer impurezas en el agua del vaso durante su prueba ellos son Materialización'),
	(4, 'Emisión', 'Si un estudiante cambia el color del agua en el vaso durante su prueba del agua, es un Emisor'),
	(5, 'Manipulación', 'Si un estudiante mueve la hoja flotando en el agua del vaso durante su prueba del agua, es un Manipulador'),
	(6, 'Especialización', 'Si un estudiante hace algún otro efecto durante su prueba del agua, son Especialistas')
SET IDENTITY_INSERT Nen OFF
GO

SET IDENTITY_INSERT CazadorNen ON
GO
INSERT INTO CazadorNen
	(Id, Nombre, Edad)
VALUES
	(1, 1),
	(2, 2),
	(3, 3),
	(3, 6),
	(4, 4)
SET IDENTITY_INSERT CazadorNen OFF
GO

-- Stored Procedure ---------------------------------------------
-- --------------------------------------------------------------
SELECT * FROM Cazadores
SELECT * FROM Nen
SELECT * FROM CazadorNen

SELECT
	a.Nombre,
	c.Nombre AS Nen,
	c.Descripcion
FROM Cazadores a
	INNER JOIN CazadorNen b ON a.Id = b.Id_Cazador
	INNER JOIN Nen c ON b.Id_Nen = c.Id

-- Query --------------------------------------------------------
-- --------------------------------------------------------------

-- --------------------------------------------------------------
-- --------------------------------------------------------------