-- Lista los usuario del SQL
SELECT 
	NAME AS LoginName, 
	TYPE_DESC AS AccountType, 
	create_date, 
	modify_date,
	TYPE
FROM sys.server_principals
WHERE TYPE IN ('S', 'U', 'G');
GO
-- Crea un nuevo usuario para login
CREATE LOGIN testing WITH PASSWORD = 'testing';
GO
-- Crea nueva Base de Datos
CREATE DATABASE db_testing
GO
-- Te posiciona en la Base de Datos
USE db_testing
GO
-- Crea el usuario para la Base de Datos
CREATE USER testing FOR LOGIN testing;
GO
-- Asigna el rol de Owner del usuario a la Base de Datos
EXECUTE sp_addrolemember 'db_owner', 'testing';

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

CREATE TABLE Perfiles (
	Id INT PRIMARY KEY IDENTITY(1,1),
	Nombre VARCHAR(50) NOT NULL,
	UNIQUE(Nombre),
)
GO

CREATE TABLE Usuarios (
	Id VARCHAR(256) PRIMARY KEY,
	Email VARCHAR(100) NOT NULL,
	AuthHash VARBINARY(64) NOT NULL,
	AuthSalt VARBINARY(16) NOT NULL,
	SessionCode VARCHAR(256) ,
	Id_Perfil INT NOT NULL
	UNIQUE(Email),
	FOREIGN KEY (Id_Perfil) REFERENCES Perfiles(Id)
)
GO

DROP TABLE __EFMigrationsHistory
DROP TABLE CazadorNen
DROP TABLE Nen
DROP TABLE Cazadores
DROP TABLE Perfiles
DROP TABLE Usuarios

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

SET IDENTITY_INSERT Perfiles ON
GO
INSERT INTO Perfiles
	(Id, Nombre)
VALUES
	(1, 'Admin'),
	(2, 'Usuario')
SET IDENTITY_INSERT Perfiles OFF
GO

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

ALTER PROCEDURE Auth_Register
	@Id VARCHAR(256),
	@Email VARCHAR(100),
    @Clave VARCHAR(100),
	@ClaveConfirmar VARCHAR(100)
AS
BEGIN
	SET NOCOUNT ON
	
	IF @Clave <> @ClaveConfirmar
		BEGIN
			SELECT 400 AS StatusCode, 'Las Contraseñas NO Coinciden' AS StatusMessage
			RETURN
		END

	IF EXISTS (SELECT Id FROM Usuarios WHERE Email = @Email)
		BEGIN
			SELECT 400 AS StatusCode, 'El Usuario ya Existe' AS StatusMessage
			RETURN
		END

    DECLARE @Salt VARBINARY(16)
    SET @Salt = CRYPT_GEN_RANDOM(16)

    DECLARE @Hash VARBINARY(64)
    SET @Hash = HASHBYTES('SHA2_256', @Clave + CAST(@Salt AS NVARCHAR(32)))

	BEGIN TRY
		BEGIN TRANSACTION

		INSERT INTO Usuarios 
			(Id, Email, AuthHash, AuthSalt, Id_Perfil)
		VALUES 
			(@Id, @Email, @hash, @salt, 2)
			
		COMMIT TRANSACTION

		SELECT 201 AS StatusCode, 'Usuario Registrado Correctamente' AS StatusMessage
    END TRY
    BEGIN CATCH
		ROLLBACK TRANSACTION

		SELECT 500 AS StatusCode, 'Error al Guardado los Datos (Auth_Register)' AS StatusMessage 
    END CATCH
END
GO

ALTER PROCEDURE Auth_Login
    @Email NVARCHAR(100),
    @Clave NVARCHAR(100)
AS
BEGIN
	SET NOCOUNT ON

    DECLARE @Hash VARBINARY(64)
    DECLARE @Salt VARBINARY(16)
    DECLARE @HashProvidedPassword VARBINARY(64)

    SELECT 
		@Hash = AuthHash, @Salt = AuthSalt
    FROM Usuarios 
	WHERE 
		Email = @Email

    SET @hashProvidedPassword = HASHBYTES('SHA2_256', @Clave + CAST(@Salt AS NVARCHAR(32)))

    IF @Hash <> @hashProvidedPassword OR @Hash IS NULL OR @hashProvidedPassword IS NULL
		BEGIN
			SELECT 400 AS StatusCode, 'Usuario o Contraseña Incorrecta' AS StatusMessage
			RETURN
		END

	BEGIN TRY
		BEGIN TRANSACTION

		UPDATE Usuarios SET
			SessionCode = NEWID()
		WHERE 
			Email = @Email
		
		COMMIT TRANSACTION

		SELECT 201 AS StatusCode, 'Inicio de Sesión Exitoso.' AS StatusMessage
    END TRY
    BEGIN CATCH
		ROLLBACK TRANSACTION

		SELECT 500 AS StatusCode, 'Error al Iniciar Sesion (Auth_Login)' AS StatusMessage 
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
DROP PROCEDURE Auth_Register
DROP PROCEDURE Auth_Login

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

SELECT * FROM Perfiles
SELECT * FROM Usuarios

EXECUTE Auth_Register 'ABCD123', 'user@example.com', 'string', 'string' 
EXECUTE Auth_Login 'user@example.com', 'string' 

TRUNCATE TABLE Usuarios
SELECT NEWID()

-- --------------------------------------------------------------
-- --------------------------------------------------------------