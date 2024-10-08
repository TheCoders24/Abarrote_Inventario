CREATE DATABASE AbarroteDB
USE AbarroteDB
-- Tabla Producto
CREATE TABLE Producto (
    ID_Producto INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(100) NOT NULL,
    Precio DECIMAL(10, 2) NOT NULL,
    Descripci�n VARCHAR(255)
);

-- Tabla Proveedor
CREATE TABLE Proveedor (
    ID_Proveedor INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(100) NOT NULL,
    Telefono VARCHAR(20),
    Direcci�n VARCHAR(255)
);

-- Tabla Cliente
CREATE TABLE Cliente (
    ID_Cliente INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(100) NOT NULL,
    Telefono VARCHAR(20),
    Direcci�n VARCHAR(255)
);

-- Tabla Venta
CREATE TABLE Venta (
    ID_Venta INT PRIMARY KEY IDENTITY(1,1),
    Fecha DATE NOT NULL,
    Importe DECIMAL(10, 2) NOT NULL,
    Iva DECIMAL(10, 2) NOT NULL,
    Total DECIMAL(10, 2) NOT NULL,
    M�todo_Pago VARCHAR(50),
    ID_Cliente INT,
    CONSTRAINT fk_cliente_venta FOREIGN KEY (ID_Cliente) REFERENCES Cliente(ID_Cliente)
);

-- Tabla Detalle de Venta
CREATE TABLE DetalleVenta (
    ID_Venta INT,
    ID_Producto INT,
    Cantidad INT NOT NULL,
    Precio_Unitario DECIMAL(10, 2) NOT NULL,
    Subtotal DECIMAL(10, 2) NOT NULL,
    PRIMARY KEY (ID_Venta, ID_Producto),
    CONSTRAINT fk_venta_detalle FOREIGN KEY (ID_Venta) REFERENCES Venta(ID_Venta),
    CONSTRAINT fk_producto_detalle FOREIGN KEY (ID_Producto) REFERENCES Producto(ID_Producto)
);

-- Tabla Inventario
CREATE TABLE Inventario (
    ID_Inventario INT PRIMARY KEY IDENTITY(1,1),
    Fecha_Registro DATE NOT NULL,
    Observaciones VARCHAR(255),
    Importe DECIMAL(10, 2) NOT NULL,
    IVA DECIMAL(10, 2) NOT NULL,
    Total DECIMAL(10, 2) NOT NULL,
    ID_Proveedor INT,
    CONSTRAINT fk_proveedor_inventario FOREIGN KEY (ID_Proveedor) REFERENCES Proveedor(ID_Proveedor)
);

-- Tabla Detalle de Inventario
CREATE TABLE DetalleInventario (
    ID_Detalle_Inventario INT PRIMARY KEY IDENTITY(1,1),
    ID_Inventario INT,
    ID_Producto INT,
    Cantidad_Entrante INT NOT NULL,
    Costo_Unitario DECIMAL(10, 2) NOT NULL,
    Subtotal DECIMAL(10, 2) NOT NULL,
    CONSTRAINT fk_inventario_detalle FOREIGN KEY (ID_Inventario) REFERENCES Inventario(ID_Inventario),
    CONSTRAINT fk_producto_inventario_detalle FOREIGN KEY (ID_Producto) REFERENCES Producto(ID_Producto)
);

-- Tabla Saldos
CREATE TABLE Saldos (
    ID_Saldo INT PRIMARY KEY IDENTITY(1,1),
    ID_Producto INT,
    Cantidad_Entrante INT NOT NULL,
    Cantidad_Salida INT NOT NULL,
    Cantidad_Disponible AS (Cantidad_Entrante - Cantidad_Salida),
    CONSTRAINT fk_producto_saldos FOREIGN KEY (ID_Producto) REFERENCES Producto(ID_Producto)
);

-- Relaci�n Producto - Proveedor (Un proveedor puede suministrar muchos productos; un producto es suministrado por un proveedor)
ALTER TABLE Producto
ADD ID_Proveedor INT;

ALTER TABLE Producto
ADD CONSTRAINT fk_producto_proveedor FOREIGN KEY (ID_Proveedor) REFERENCES Proveedor(ID_Proveedor);


-- Relaci�n Producto - Venta (Un producto puede ser vendido en muchas ventas; una venta puede incluir muchos productos) ya est� establecida en DetalleVenta

-- Relaci�n Cliente - Venta (Un cliente puede realizar muchas compras; una venta est� asociada a un solo cliente) ya est� establecida en Venta

-- Relaci�n Producto - Inventario (Un producto tiene varios registros de inventario en diferentes fechas) ya est� establecida en DetalleInventario

-- Relaci�n Inventario - Proveedor (El inventario se actualiza cada vez que un proveedor entrega productos) ya est� establecida en Inventario

-- Relaci�n Producto - Saldos (Cada producto tiene un saldo que refleja sus existencias actuales, actualizadas conforme a las entradas y salidas) ya est� establecida en Saldos
