
CREATE VIEW Vista_TotalProductosVendidos AS
SELECT 
    P.ID_Producto,
    P.Nombre AS NombreProducto,
    SUM(DV.Cantidad) AS TotalUnidadesVendidas,
    SUM(DV.Subtotal) AS IngresoTotalProducto
FROM 
    Producto P
INNER JOIN 
    DetalleVenta DV ON P.ID_Producto = DV.ID_Producto
GROUP BY 
    P.ID_Producto, P.Nombre;




CREATE VIEW Vista_VentasPorMes AS
SELECT 
    YEAR(V.Fecha) AS Año,
    MONTH(V.Fecha) AS Mes,
    SUM(V.Total) AS TotalVentasMes
FROM 
    Venta V
GROUP BY 
    YEAR(V.Fecha), MONTH(V.Fecha);



CREATE VIEW Vista_TotalVentasPorCliente AS
SELECT 
    C.ID_Cliente,
    C.Nombre AS NombreCliente,
    SUM(V.Total) AS TotalComprasCliente
FROM 
    Cliente C
INNER JOIN 
    Venta V ON C.ID_Cliente = V.ID_Cliente
GROUP BY 
    C.ID_Cliente, C.Nombre;





CREATE VIEW Vista_IngresoDiario AS
SELECT 
    V.Fecha AS FechaVenta,
    SUM(V.Total) AS IngresoTotalDiario
FROM 
    Venta V
GROUP BY 
    V.Fecha;

