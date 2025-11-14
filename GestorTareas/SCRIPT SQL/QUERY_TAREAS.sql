CREATE DATABASE TAREAS
GO
USE TAREAS
GO


CREATE TABLE Usuario (
    Id INT IDENTITY PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Email NVARCHAR(150) NULL
);
GO

INSERT INTO Usuario (Nombre, Email) VALUES (N'Juan Pérez', 'juan.perez@example.com');
INSERT INTO Usuario (Nombre, Email) VALUES (N'Ana María López', 'ana.lopez@example.com');
INSERT INTO Usuario (Nombre, Email) VALUES (N'Carlos Gómez', 'carlit@example.com');
INSERT INTO Usuario (Nombre, Email) VALUES (N'Lucía Fernández', 'lucia.fernandez@example.org');
INSERT INTO Usuario (Nombre, Email) VALUES (N'Marco Díaz', 'marco.diaz@example.net');
GO

CREATE TABLE Tarea (
    Id INT IDENTITY PRIMARY KEY,
    Titulo NVARCHAR(200) NOT NULL,
    Descripcion NVARCHAR(MAX) NULL,
    FechaCreacion DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    FechaVencimiento DATETIME2 NULL,
    Estado VARCHAR(20) NOT NULL DEFAULT 'Pendiente',
    UsuarioId INT NOT NULL,

    CONSTRAINT FK_Tarea_Usuario FOREIGN KEY (UsuarioId)
        REFERENCES Usuario(Id),

    CONSTRAINT CHK_Tarea_Estado CHECK (Estado IN ('Pendiente', 'En progreso', 'Completada'))
);
GO

select * from Tarea
  SELECT [u].[Id], [u].[Email], [u].[Nombre]
      FROM [Usuario] AS [u]