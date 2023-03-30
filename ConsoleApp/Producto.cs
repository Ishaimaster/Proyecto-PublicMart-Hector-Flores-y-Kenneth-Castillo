using System.Text.RegularExpressions;

public class Producto
{
    public Producto() { }

    public Producto(string nombreProducto, int cantidad, double precioUnitario)
    {
        NombreProducto = nombreProducto;
        Cantidad = cantidad;
        PrecioUnitario = precioUnitario;
        
    }

    public string NombreProducto { get; set; }
    public int Cantidad { get; set; }
    public double PrecioUnitario { get; set; }

   



    public double Total()
    {
        return Cantidad * PrecioUnitario;
    }
}



