﻿using System.Security.Principal;
using System.Text.RegularExpressions;
using System.Transactions;

//inicializacion de las variables del menu principal
bool continuar = true;
int opcion = 0;
bool op = false;
// Inicializacion de las variables de los datos de la factura 
string email, nombre;
string Nonit;
int puntos = 0;
double subtotal = 0, total = 0;
int n = 0;
char metodoPago;
string codigo = "";
//Inicializacion de las variables para el reporte de facturación
int totalFacturas = 0;
int totalProductos = 0;
int totalPuntosGenerados = 0;
double totalVentas = 0;
double totalVentasCredito = 0;
double totalVentasContado = 0;
//Inicializacion de las variables para el reporte de facturacion, asi como los detalles de cada producto y la cantidad del mismo que se ha vendido en total.
double cantpan = 0;
double cantLib = 0;
double cantGall = 0;
double cantGran = 0;
double cantLitro = 0;

while (continuar)
{
    while (!op)
    {//Solicita al usuario que elija una opcion
        Console.WriteLine("-----------------------------------------------");
        Console.WriteLine("Bienvenido al sistema de facturacion PublicMart");
        Console.WriteLine("-----------------------------------------------");
        Console.WriteLine("Seleccione una opción:");
        Console.WriteLine("1. Facturación");
        Console.WriteLine("2. Reportes de facturación");
        Console.WriteLine("3. Cerrar programa");
        Console.Write("Opción: ");

        //Valida que la opcion sea correcta
        try
        {
            opcion = int.Parse(Console.ReadLine());

            if (opcion > 0 && opcion <= 3)
            {

                op = true;
            }
            else
            {
                Console.WriteLine("Ingrese una opcion valida");
                Console.ReadKey();
            }

        }
        catch (Exception)
        {
            Console.WriteLine("Ingrese una opcion valida"); 
            Console.ReadKey();
        }
        Console.Clear();

 
        switch (opcion)
        {
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            case 1:

                  bool fact = true;

                while (fact)
                {
                    //Cada vez que se ejecute la opcion, estas variables se igualan a cero, evitando la informacion incorrecta de los datos de la compra en la factura
                    subtotal = 0;
                    n = 0;
                    total = 0;


                    // Pedir datos del cliente

                    //Validacion del nit
                    while (true)
                    {
                        Console.WriteLine("Ingrese el No. NIT del cliente (8 digitos) : ");
                        Nonit = Console.ReadLine();


                        if (Nonit.Length == 8)
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Ingrese el Nit correctamente");
                            Console.ReadKey();
                            Console.Clear();
                        }

                    }
                    Console.Clear();

                   

                    //Ingreso y validacion del correo electronico del cliente
                    while (true)
                    {
                        // Imprimir lo ingresado para mejorar el aspecto de avance del menu
                        Console.WriteLine("Ingrese el No. NIT del cliente (8 digitos) : ");
                        Console.WriteLine(Nonit);
                       
                        Console.WriteLine("Ingrese el email del cliente: ");
                        email = Console.ReadLine();

                        bool formato = Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");

                        if (formato)
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Ingrese una direccion de correo valida");
                            Console.ReadKey();
                           
                        } 
                        Console.Clear();
                    }

                    //Ingreso del nombre del cliente
                    Console.WriteLine("Ingrese el nombre del cliente: ");
                    nombre = Console.ReadLine();


                    Console.Clear();
                    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


                    //Utiliza la clase Producto, para crear una "lista" de los productos, a partir del codigo del producto, asignarle el nombre y precio unitario correspondiente al producto seleccionado
                    bool agregarProducto = true;

                    List<Producto> _productos = new();

                    while (agregarProducto)
                    {

                        Producto nuevoProducto = new();
                        bool codigocorrecto = true;

                        while (codigocorrecto!)
                        {

                            Console.WriteLine("Ingrese el codígo del producto deseado (001-005): ");

                            double precio = 0;

                            codigo = Console.ReadLine();

                            switch (codigo)
                            {
                                case "001":
                                    precio = 1.10;
                                    nuevoProducto.PrecioUnitario = precio;
                                    nuevoProducto.NombreProducto = "Pan francés";
                                    codigocorrecto = false;

                                    break;
                                case "002":
                                    precio = 5.00;
                                    nuevoProducto.PrecioUnitario = precio;
                                    nuevoProducto.NombreProducto = "Libra de azúcar";
                                    codigocorrecto = false;

                                    break;
                                case "003":
                                    precio = 7.30;
                                    nuevoProducto.PrecioUnitario = precio;
                                    nuevoProducto.NombreProducto = "Caja de galletas";
                                    codigocorrecto = false;

                                    break;
                                case "004":
                                    precio = 32.50;
                                    nuevoProducto.PrecioUnitario = precio;
                                    nuevoProducto.NombreProducto = "Caja de granola";
                                    codigocorrecto = false;

                                    break;
                                case "005":
                                    precio = 17.95;
                                    nuevoProducto.PrecioUnitario = precio;
                                    nuevoProducto.NombreProducto = "Litro de jugo de naranja";
                                    codigocorrecto = false;

                                    break;
                                default:
                                    //Validación de la correcta escritura del codigo
                                    Console.WriteLine("Código del producto inválido");
                                    codigocorrecto = true;
                                    Console.ReadKey();
                                    Console.Clear();
                                    break;
                            }


                        }
                        Console.Clear();
                        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                        //Solicita al usuario la cantidad del producto
                        int cantidad = 0;

                        while (true)
                        {
                            Console.WriteLine("Ingrese el codígo del producto deseado (001-005): ");
                            Console.WriteLine(codigo);
                            try
                            {
                                Console.WriteLine("Ingrese la cantidad de unidades: ");
                                cantidad = int.Parse(Console.ReadLine());

                                if (cantidad < 0)
                                {       //Valida que la cantidad ingresada sea mayor que  0
                                    Console.WriteLine("Cantidad de unidades incorrectas.");
                                    Console.ReadKey();
                                }
                                else
                                {
                                    n = n + cantidad; //Variable que almacena la cantidad total de productos 
                                    nuevoProducto.Cantidad = cantidad; // le asigna la cantidad al elemento de la lista
                                    subtotal += nuevoProducto.Total(); //Variable acumulativa que guarda la suma de los precios de los productos sin impuestos
                                    break;
                                }
                            }
                            catch (Exception)
                            {
                                //Valida que se ingrese un numero
                                Console.WriteLine("Ingrese una cantidad valida");
                                Console.ReadKey();
                                Console.Clear();
                            }
                            Console.Clear();
                        }


                        ///////////////////////////////////////////////////////////////////////////


                        //Pregunta al usuario si desea ingresar otro producto 
                        while (true)
                        {
                            try
                            {
                                Console.WriteLine("¿Desea ingresar otro producto?  s=si/ n=no");
                                char respuesta = Convert.ToChar(Console.ReadLine());


                                if (respuesta == 's' || respuesta == 'S')
                                {
                                    agregarProducto = true;
                                    break;
                                }
                                else if (respuesta == 'n' || respuesta == 'N')
                                {
                                    agregarProducto = false;
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Ingrese una respuesta valida");
                                    Console.ReadKey();
                                    Console.Clear();
                                }



                            }
                            catch (Exception)

                            {
                                Console.WriteLine("Ingrese una respuesta valida");
                                Console.ReadKey();
                                Console.Clear();

                            }
                        }

                        Console.Clear();


                        _productos.Add(nuevoProducto); //Añade el nuevo elemento a la lista

                    }

                    //Solicita el metodo de pago, y se le adjunta los puntos de compra, segun el metodo de pago y el costo de la compra

                    string FormaPago = "";

                    while (true)
                    {
                        try
                        {
                            Console.WriteLine("Ingrese metodo de pago (Efectivo/Tarjeta) E=efectivo/ T=tarjeta");
                            metodoPago = Convert.ToChar(Console.ReadLine());
                            if (metodoPago == 'T')
                            {
                                FormaPago = "Tarjeta";

                                puntos = (int)(subtotal / 10);

                                if (subtotal >= 50 && subtotal < 150)
                                {
                                    puntos *= 2;
                                }
                                else if (subtotal >= 150)
                                {
                                    puntos = (int)((subtotal / 15) * 3);
                                }
                            }
                            else if (metodoPago == 'E')
                            {
                                FormaPago = "Efectivo";
                                puntos = 0;

                            }
                            break;
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Ingrese un metodo de pago valido");
                            Console.Clear();
                        }

                    }
                    Console.Clear();
                    //Genera el numero de factura

                    Random rnd = new Random();
                    int x = rnd.Next(1, 1000);

                    int numfactura = ((2 * x + puntos * puntos) + (2021 * n)) % 10000;


                    //Imprime la factura
                    Console.WriteLine();
                    Console.WriteLine("============================= FACTURA ============================");
                    Console.WriteLine();
                    Console.WriteLine("Supermercado PublicMart");
                    Console.WriteLine("No. Factura: " + numfactura);
                    Console.WriteLine("NIT: " + Nonit);
                    Console.WriteLine("Cliente: " + nombre);
                    Console.WriteLine("Email: " + email);
                    Console.WriteLine(DateTime.Now);
                    Console.WriteLine("Metodo de pago: " + FormaPago);
                    Console.WriteLine("Puntos acumulados: " + puntos);
                    Console.WriteLine("============================= PRODUCTOS ==========================");

                    //Recorre cada elemento de la lista creada y escoge un determinado elemento segun su codigo
                    foreach (Producto _producto in _productos)
                    {
                        codigo = "";
                        switch (_producto.NombreProducto)
                        {
                            case "Pan francés":
                                codigo = "001";
                                cantpan = cantpan + _producto.Cantidad;
                                break;
                            case "Libra de azúcar":
                                codigo = "002";
                                cantLib = cantLib + _producto.Cantidad;
                                break;
                            case "Caja de galletas":
                                codigo = "003";
                                cantGall = cantGall + _producto.Cantidad;
                                break;
                            case "Caja de granola":
                                codigo = "004";
                                cantGran = cantGran + _producto.Cantidad;
                                break;
                            case "Litro de jugo de naranja":
                                codigo = "005";
                                cantLitro = cantLitro + _producto.Cantidad;
                                break;
                        }

                        Console.WriteLine("Código " + codigo + "|" + "Producto " + _producto.NombreProducto + "|" + "Precio Q" + _producto.PrecioUnitario + "|" + "Cantidad " + _producto.Cantidad);

                    }
                    Console.WriteLine("-------------------------------------------------------------------");


                    //Variables para el calculo e impresion del subtotal, impuestos y total de la factura
                    double impuestoISR = subtotal * 0.05;
                    double impuestoIVA = subtotal * 0.12;
                    total = subtotal + impuestoISR + impuestoIVA;

                    Console.WriteLine("Subtotal: Q" + (subtotal.ToString("0.00")));
                    Console.WriteLine("Impuestos: 5% Impuesto de ISR y 12% Impuesto de IVA");
                    Console.WriteLine("Total: Q" + (total.ToString("0.00")));
                    Console.WriteLine("-------------------------------------------------------------------");
                    Console.WriteLine("Una copia de la factura se enviará al correo: " + email);


                    //Variables acumultavias para el reporte de facturacion
                    totalFacturas++;

                    if (metodoPago == 'E')
                    {
                        totalVentasContado = totalVentasContado + total;

                    }
                    else if (metodoPago == 'T')
                    {
                        totalVentasCredito = totalVentasContado + total;
                    }

                    totalPuntosGenerados = totalPuntosGenerados + puntos;

                    totalProductos = totalProductos + n;

                    totalVentas = totalVentas + total;



                    Console.ReadKey();
                    Console.Clear();


                    while (true)
                    {
                        try
                        {
                            //Solicita al usuario si desea continuar facturando o volver al menu principal
                            Console.WriteLine("1. Crear otra factura");
                            Console.WriteLine("2. Volver al menu principal");
                            Console.Write("Opción: ");
                            int opcion2 = Convert.ToInt32(Console.ReadLine());

                            switch (opcion2)
                            {
                                case 1:

                                    fact = true;
                                    break;

                                case 2:

                                    fact = false;
                                    break;

                            }
                            break;
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Ingrese una opcion valida");
                            Console.ReadKey();

                        }
                        Console.Clear();

                    }
                    Console.Clear();
                }
                
                //Vuelve al usuario al menu principal
                op = false;
                break;

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            case 2:

                // Imprime las variables acumulativas creadas anteriormente
                Console.WriteLine("---------------------------------------");
                Console.WriteLine("Reporte de facturacion");
                Console.WriteLine("---------------------------------------");
                Console.WriteLine("Total de facturas realizadas: " + totalFacturas);
                Console.WriteLine("Total de Productos: " + totalProductos);
                Console.WriteLine("\tPan Frances, Total vendido: " + cantpan);
                Console.WriteLine("\tLibra de azucar, Total vendido: " + cantLib);
                Console.WriteLine("\tCaja de galletas, Total vendido: " + cantGall);
                Console.WriteLine("\tCaja de granola, Total vendido: " + cantGran);
                Console.WriteLine("\tLitro de jugo de naranja, Total vendido: " + cantLitro);
                Console.WriteLine("Total de Puntos generados: " + totalPuntosGenerados);
                Console.WriteLine("Total de ventas a Credito: Q" + (totalVentasCredito.ToString("0.00")));
                Console.WriteLine("Total de ventas a Contado: Q" + (totalVentasContado.ToString("0.00")));
                Console.WriteLine("Total de ventas: Q" + (totalVentas.ToString("0.00")));


                //Espera a que el usuario pulse una tecla para devolverlo al menu principal
                Console.WriteLine();
                Console.WriteLine("Presione cualquier tecla para salir");
                Console.ReadKey();
                op = false;
                Console.Clear();


                break;

                
                
                //Cierre del programa
            case 3:
                Console.WriteLine("Cerrando programa...");
                continuar = false;
                break;

        }
        Console.WriteLine();
    }
}













