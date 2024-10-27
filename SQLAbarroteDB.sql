CREATE DATABASE AbarroteDB
USE AbarroteDB
-- Tabla Producto
CREATE TABLE Producto (
    ID_Producto INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(100) NOT NULL,
    Precio DECIMAL(10, 2) NOT NULL,
    Descripción VARCHAR(255)
);

-- Tabla Proveedor
CREATE TABLE Proveedor (
    ID_Proveedor INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(100) NOT NULL,
    Telefono VARCHAR(20),
    Dirección VARCHAR(255)
);

-- Tabla Cliente
CREATE TABLE Cliente (
    ID_Cliente INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(100) NOT NULL,
    Telefono VARCHAR(20),
    Dirección VARCHAR(255)
);

-- Tabla Venta
CREATE TABLE Venta (
    ID_Venta INT PRIMARY KEY IDENTITY(1,1),
    Fecha DATE NOT NULL,
    Importe DECIMAL(10, 2) NOT NULL,
    Iva DECIMAL(10, 2) NOT NULL,
    Total DECIMAL(10, 2) NOT NULL,
    Método_Pago VARCHAR(50),
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

-- Relación Producto - Proveedor (Un proveedor puede suministrar muchos productos; un producto es suministrado por un proveedor)
ALTER TABLE Producto
ADD ID_Proveedor INT;

ALTER TABLE Producto
ADD CONSTRAINT fk_producto_proveedor FOREIGN KEY (ID_Proveedor) REFERENCES Proveedor(ID_Proveedor);


-- Relación Producto - Venta (Un producto puede ser vendido en muchas ventas; una venta puede incluir muchos productos) ya está establecida en DetalleVenta

-- Relación Cliente - Venta (Un cliente puede realizar muchas compras; una venta está asociada a un solo cliente) ya está establecida en Venta

-- Relación Producto - Inventario (Un producto tiene varios registros de inventario en diferentes fechas) ya está establecida en DetalleInventario

-- Relación Inventario - Proveedor (El inventario se actualiza cada vez que un proveedor entrega productos) ya está establecida en Inventario

-- Relación Producto - Saldos (Cada producto tiene un saldo que refleja sus existencias actuales, actualizadas conforme a las entradas y salidas) ya está establecida en Saldos

-- Registrar venta
CREATE PROCEDURE usp_RegistrarVenta
    @Fecha DATE,
    @Importe DECIMAL(10, 2),
    @Iva DECIMAL(10, 2),
    @Total DECIMAL(10, 2),
    @Metodo_Pago VARCHAR(50),
    @ID_Cliente INT,
    @ID_Producto INT,
    @Cantidad INT,
    @Precio_Unitario DECIMAL(10, 2),
    @Resultado INT OUTPUT
AS
BEGIN
    BEGIN TRY
        -- Iniciar transacción
        BEGIN TRANSACTION;

        -- Insertar la venta en la tabla Venta
        INSERT INTO Venta (Fecha, Importe, Iva, Total, Metodo_Pago, ID_Cliente)
        VALUES (@Fecha, @Importe, @Iva, @Total, @Metodo_Pago, @ID_Cliente);

        -- Obtener el ID de la venta recién insertada
        DECLARE @ID_Venta INT;
        SET @ID_Venta = SCOPE_IDENTITY();

        -- Insertar los detalles de la venta en la tabla DetalleVenta
        INSERT INTO DetalleVenta (ID_Venta, ID_Producto, Cantidad, Precio_Unitario, Subtotal)
        VALUES 
            (@ID_Venta, @ID_Producto, @Cantidad, @Precio_Unitario, @Cantidad * @Precio_Unitario);

        -- Verificar si hay suficiente inventario antes de actualizar
        IF EXISTS (
            SELECT 1
            FROM Saldos
            WHERE ID_Producto = @ID_Producto
              AND (Cantidad_Entrante - Cantidad_Salida) >= @Cantidad
        )
        BEGIN
            -- Actualizar el inventario (tabla Saldos)
            UPDATE Saldos
            SET Cantidad_Salida = Cantidad_Salida + @Cantidad
            WHERE ID_Producto = @ID_Producto;

            -- Confirmar la transacción
            COMMIT;

            -- Devolver el ID de la venta registrada
            SET @Resultado = @ID_Venta;
        END
        ELSE
        BEGIN
            -- Si no hay suficiente inventario, hacer rollback y devolver un error
            ROLLBACK;
            SET @Resultado = -1; -- Indica error por falta de inventario
        END
    END TRY
    BEGIN CATCH
        -- Si ocurre un error, revertir la transacción
        ROLLBACK;

        -- Indicar error devolviendo 0
        SET @Resultado = 0;
    END CATCH
END
GO

