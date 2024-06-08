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

INSERT INTO CazadorNen
	(Id_Cazador, Id_Nen)
VALUES
	(1, 1),
	(2, 2),
	(3, 3),
	(3, 6),
	(4, 4)

-- Stored Procedure ---------------------------------------------
-- --------------------------------------------------------------
CREATE PROCEDURE Cazadores_GetAll 
AS
BEGIN
	SET NOCOUNT ON

	SELECT
		Id,
		Nombre,
		Edad
	FROM Cazadores
END
GO

CREATE PROCEDURE Cazadores_GetById
	@Id AS INT
AS
BEGIN
	SET NOCOUNT ON

	SELECT
		Id,
		Nombre,
		Edad
	FROM Cazadores
	WHERE
		Id = @Id
END
GO

CREATE PROCEDURE Cazadores_Insert
	@Nombre AS VARCHAR(50),
	@Edad AS INT
AS
BEGIN
	SET NOCOUNT ON

	BEGIN TRANSACTION

	BEGIN TRY
		INSERT INTO Cazadores
			(Nombre, Edad)
		VALUES
			(@Nombre, @Edad)		
				
		COMMIT TRANSACTION

		SELECT 201 AS StatusCode, 'Datos Guardados Correctamente' AS Msge, SCOPE_IDENTITY() AS Id
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION

		SELECT ERROR_STATE() AS StatusCode, ERROR_MESSAGE() AS Msge, 0 AS Id
	END CATCH
END
GO

CREATE PROCEDURE Cazadores_Update
	@Id AS INT,
	@Nombre AS VARCHAR(50),
	@Edad AS INT
AS
BEGIN
	SET NOCOUNT ON

	BEGIN TRANSACTION

	BEGIN TRY
		UPDATE Cazadores SET
			Nombre = @Nombre, 
			Edad = @Edad
		WHERE
			Id = @Id

		COMMIT TRANSACTION

		SELECT 202 AS StatusCode, 'Datos Modificados Correctamente' AS Msge, @Id AS Id
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION

		SELECT ERROR_STATE() AS StatusCode, ERROR_MESSAGE() AS Msge, 0 AS Id
	END CATCH
END
GO

CREATE PROCEDURE Cazadores_Delete
	@Id AS INT
AS
BEGIN
	SET NOCOUNT ON

    IF EXISTS (SELECT Id_Cazador FROM CazadorNen WHERE Id_Cazador = @Id)
		BEGIN
			SELECT 400 AS StatusCode, 'Existe Dependencia Con CazadorNen' AS Msge, 0 AS Id
			RETURN
		END

    IF NOT EXISTS (SELECT Id FROM Cazadores WHERE Id = @Id)
		BEGIN
			SELECT 404 AS StatusCode, 0 AS Id, 'El Id No Existe' AS Msge 
			RETURN
		END

	BEGIN TRANSACTION

	BEGIN TRY
		DELETE FROM Cazadores WHERE Id = @Id

		COMMIT TRANSACTION

		SELECT 202 AS StatusCode, 'Datos Eliminados Correctamente' AS Msge, 0 AS Id
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION

		SELECT ERROR_STATE() AS StatusCode, ERROR_MESSAGE() AS Msge, 0 AS Id
	END CATCH
END
GO

CREATE PROCEDURE Nen_GetAll 
AS
BEGIN
	SET NOCOUNT ON

	SELECT
		Id,
		Nombre,
		Descripcion
	FROM Nen
END
GO

CREATE PROCEDURE Nen_GetById
	@Id AS INT
AS
BEGIN
	SET NOCOUNT ON

	SELECT
		Id,
		Nombre,
		Descripcion
	FROM Nen
	WHERE
		Id = @Id
END
GO

CREATE PROCEDURE Nen_Insert
	@Nombre AS VARCHAR(50),
	@Descripcion AS VARCHAR(256)
AS
BEGIN
	SET NOCOUNT ON

	BEGIN TRANSACTION

	BEGIN TRY
		INSERT INTO Nen
			(Nombre, Descripcion)
		VALUES
			(@Nombre, @Descripcion)

		COMMIT TRANSACTION

		SELECT 201 AS StatusCode, 'Datos Guardados Correctamente' AS Msge, SCOPE_IDENTITY() AS Id
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION

		SELECT ERROR_STATE() AS StatusCode, ERROR_MESSAGE() AS Msge, 0 AS Id
	END CATCH
END
GO

CREATE PROCEDURE Nen_Update
	@Id AS INT,
	@Nombre AS VARCHAR(50),
	@Descripcion AS VARCHAR(256)
AS
BEGIN
	SET NOCOUNT ON

	BEGIN TRANSACTION

	BEGIN TRY
		UPDATE Nen SET
			Nombre = @Nombre, 
			Descripcion = @Descripcion
		WHERE
			Id = @Id

		COMMIT TRANSACTION

		SELECT 202 AS StatusCode, 'Datos Modificados Correctamente' AS Msge, @Id AS Id
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION

		SELECT ERROR_STATE() AS StatusCode, ERROR_MESSAGE() AS Msge, 0 AS Id
	END CATCH
END
GO

CREATE PROCEDURE Nen_Delete
	@Id AS INT
AS
BEGIN
	SET NOCOUNT ON


    IF EXISTS (SELECT Id_Nen FROM CazadorNen WHERE Id_Nen = @Id)
		BEGIN
			SELECT 400 AS StatusCode, 'Existe Dependencia Con CazadorNen' AS Msge, 0 AS Id
			RETURN
		END

    IF NOT EXISTS (SELECT Id FROM Nen WHERE Id = @Id)
		BEGIN
			SELECT 404 AS StatusCode, 0 AS Id, 'El Id No Existe' AS Msge 
			RETURN
		END

	BEGIN TRANSACTION

	BEGIN TRY
		DELETE FROM Nen WHERE Id = @Id

		COMMIT TRANSACTION

		SELECT 202 AS StatusCode, 'Datos Eliminados Correctamente' AS Msge, 0 AS Id
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION

		SELECT ERROR_STATE() AS StatusCode, ERROR_MESSAGE() AS Msge, 0 AS Id
	END CATCH
END
GO

CREATE PROCEDURE CazadorNen_GetAll 
AS
BEGIN
	SET NOCOUNT ON

	SELECT
		Id_Cazador,
		Id_Nen
	FROM CazadorNen
END
GO

CREATE PROCEDURE CazadorNen_Insert
	@Id_Cazador AS INT,
	@Id_Nen AS INT
AS
BEGIN
	SET NOCOUNT ON

    IF NOT EXISTS (SELECT Id FROM Cazadores WHERE Id = @Id_Cazador)
		BEGIN
			SELECT 404 AS StatusCode, 'El Id_Cazador No Existe' AS Msge, 0 AS Id
			RETURN
		END

    IF NOT EXISTS (SELECT Id FROM Nen WHERE Id = @Id_Nen)
		BEGIN
			SELECT 404 AS StatusCode, 'El Id_Nen No Existe' AS Msge, 0 AS Id
			RETURN
		END

	IF EXISTS (SELECT Id_Cazador FROM CazadorNen WHERE Id_Cazador = @Id_Cazador AND Id_Nen = @Id_Nen)
		BEGIN
			SELECT 400 AS StatusCode, 'El Elemento ya Existe' AS Msge, 0 AS Id
			RETURN
		END

	BEGIN TRANSACTION

	BEGIN TRY
		INSERT INTO CazadorNen
			(Id_Cazador, Id_Nen)
		VALUES
			(@Id_Cazador, @Id_Nen)

		COMMIT TRANSACTION

		SELECT 201 AS StatusCode, 'Datos Guardado Correctamente' AS Msge, 0 AS Id
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION

		SELECT ERROR_STATE() AS StatusCode, ERROR_MESSAGE() AS Msge, 0 AS Id
	END CATCH
END
GO

CREATE PROCEDURE CazadorNen_Delete
	@Id_Cazador AS INT,
	@Id_Nen AS INT
AS
BEGIN
	SET NOCOUNT ON

    IF NOT EXISTS (SELECT Id_Cazador FROM CazadorNen WHERE Id_Cazador = @Id_Cazador AND Id_Nen = @Id_Nen)
		BEGIN
			SELECT 404 AS StatusCode, 'El Elemento No Existe' AS Msge, 0 AS Id
			RETURN
		END

	BEGIN TRANSACTION

	BEGIN TRY
		DELETE FROM CazadorNen WHERE Id_Cazador = @Id_Cazador AND Id_Nen = @Id_Nen
	
		COMMIT TRANSACTION

		SELECT 202 AS StatusCode, 'Datos Eliminados Correctamente' AS Msge, 0 AS Id
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION

		SELECT ERROR_STATE() AS StatusCode, ERROR_MESSAGE() AS Msge, 0 AS Id
	END CATCH
END
GO

DROP PROCEDURE Cazadores_GetAll
DROP PROCEDURE Cazadores_GetById
DROP PROCEDURE Cazadores_Insert
DROP PROCEDURE Cazadores_Update
DROP PROCEDURE Cazadores_Delete
DROP PROCEDURE Nen_GetAll
DROP PROCEDURE Nen_GetById
DROP PROCEDURE Nen_Insert
DROP PROCEDURE Nen_Update
DROP PROCEDURE Nen_Delete
DROP PROCEDURE CazadorNen_GetAll
DROP PROCEDURE CazadorNen_Insert
DROP PROCEDURE CazadorNen_Delete

-- Query --------------------------------------------------------
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

-- --------------------------------------------------------------
-- --------------------------------------------------------------