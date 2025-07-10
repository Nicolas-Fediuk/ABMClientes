create database ABM_CLIENTES;

use ABM_CLIENTES;

CREATE TABLE CLIENTES
(
CLIENTE_ID INT IDENTITY(1,1) PRIMARY KEY,
CLIENTE_NOMBRES VARCHAR(50),
CLIENTE_APELLIDOS VARCHAR(80),
CLIENTE_FECNAC DATETIME,
CLIENTE_CUIT VARCHAR(12),
CLIENTE_DOMICILIO VARCHAR(100),
CLIENTE_TELEFONO VARCHAR(10),
CLIENTE_EMAIL VARCHAR(40)
)

CREATE TABLE LOG_ERRORES (
    LOG_ID INT IDENTITY(1,1) PRIMARY KEY,
    LOG_FECHA DATETIME,
    LOG_RUTA NVARCHAR(250),
    LOG_MSG NVARCHAR(MAX)
);

INSERT INTO Clientes (
    CLIENTE_NOMBRES,
    CLIENTE_APELLIDOS,
    CLIENTE_FECNAC,
    CLIENTE_CUIT,
    CLIENTE_DOMICILIO,
    CLIENTE_TELEFONO,
    CLIENTE_EMAIL
) VALUES
('Nicolás', 'Fediuk', '1998-11-23', '20123456789', 'Calle Falsa 123', '123456789', 'nf@gmail.com'),
('María', 'Pérez', '1992-07-12', '27345678901', 'Av. Siempreviva 742', '1167894561', 'maria.perez@gmail.com'),
('Lucas', 'González', '1987-04-15', '20333444556', 'Mitre 456', '1145556677', 'lucas.g@gmail.com'),
('Ana', 'Rodríguez', '1990-02-28', '27333222111', 'Belgrano 321', '1133224455', 'ana.r@gmail.com'),
('Julián', 'Fernández', '1985-08-09', '20222333444', 'Sarmiento 987', '1155667788', 'julianf@hotmail.com'),
('Carla', 'Díaz', '1995-12-01', '27345612300', 'Rivadavia 1001', '1199887766', 'carla.diaz@gmail.com'),
('Martín', 'López', '1993-03-20', '20321098765', 'San Martín 550', '1122334455', 'martin.l@hotmail.com'),
('Sofía', 'Ramírez', '1991-06-17', '27300112233', 'Córdoba 888', '1177886655', 'sofia.ramirez@gmail.com'),
('Diego', 'Sosa', '1989-11-11', '20229988776', 'La Rioja 234', '1166112288', 'diego.s@gmail.com'),
('Lucía', 'Morales', '1994-09-05', '27344556677', 'Catamarca 765', '1133557788', 'lucia.m@gmail.com');

