﻿using ConsoleTables;
using Ejercicio01Practico8.Utilidades;
using Microsoft.VisualBasic;

namespace Ejercicio01Practica08.Consola
{
    internal class Program
    {
        const Double MIN_TEMPERATURA = -10;
        const Double MAX_TEMPERATURA = 24;
        enum Orden
        {
            Ascendente=1, 
            Descendente
        }

        static void Main(string[] args)
        {
            double[] temperaturas = new double[7];
            bool seguir = true;
            do
            {
               
                MostrarMenu();
                int opcionSeleccionada = IngresoDatos.PedirIntEnRango("Seleccione:", 1, 9);
                switch (opcionSeleccionada)
                {
                    case 1:
                        GenerarLasTemperaturas(temperaturas);
                        break;
                    case 2:
                        ModificarDatos(temperaturas);
                        break;
                    case 3:
                        ListarTemperaturas(temperaturas);
                        break;
                    case 4:
                        DatosEstadisticos(temperaturas);
                        break;
                    case 5:
                        MarcarMayoresPromedio(temperaturas);
                        break;
                    case 6:
                        MostrarInferioresAlPromedio(temperaturas);
                        break;
                    case 7:
                        Ordenar(temperaturas);
                        break;
                    case 8:
                        Ordenar(temperaturas,Orden.Descendente);
                        break;
                    case 9:
                        seguir = false;
                        break;
                }

            } while (seguir);
            Console.WriteLine("Fin de la aplicacion");

        }
        private static void Ordenar(double[] temperaturas, Orden orden = Orden.Ascendente)
        {
            var arrayCopia = new double[temperaturas.Length];
            temperaturas.CopyTo(arrayCopia, 0);
            var arrayOrdenado = OrdenarArray(arrayCopia, orden);
            Console.Clear();
            Console.WriteLine("Array Ordenado de Mayor a Menor");
            ListarTemperaturas(arrayOrdenado);
            TareaFinalizada("Array ordenado ...");
        }

        //private static void OrdenarDesc(double[] temperaturas)
        //{
        //    var arrayCopia = new double[temperaturas.Length];
        //    temperaturas.CopyTo(arrayCopia, 0);
        //    var arrayOrdenado = OrdenarArrayDesc(arrayCopia);
        //    Console.Clear();
        //    Console.WriteLine("Array Ordenado de Mayor a Menor");
        //    ListarTemperaturas(arrayOrdenado);
        //    TareaFinalizada("Array ordenado desc...");
        //}

        //private static void OrdenarAsc(double[] temperaturas)
        //{
        //    var arrayCopia = new double[temperaturas.Length];
        //    temperaturas.CopyTo(arrayCopia, 0);
        //    var arrayOrdenado = OrdenarArrayAsc(arrayCopia);
        //    Console.Clear();
        //    Console.WriteLine("Array Ordenado de Menor a Mayor");
        //    ListarTemperaturas(arrayOrdenado);
        //    TareaFinalizada("Array ordenado asc...");

        //}

        private static double[] OrdenarArray(double[] arrayCopia, Orden orden=Orden.Ascendente)
        {
            if (orden==Orden.Ascendente)
            {
                arrayCopia = arrayCopia.OrderBy(t=>t).ToArray();
            }
            else
            {
                arrayCopia = arrayCopia.OrderByDescending(t => t).ToArray();
            }
            return arrayCopia;
        }

        //private static double[] OrdenarArrayDesc(double[] arrayCopia)
        //{
        //    for (int i = 0; i < arrayCopia.Length - 1; i++)
        //    {
        //        for (int j = i + 1; j < arrayCopia.Length; j++)
        //        {
        //            if (arrayCopia[i] < arrayCopia[j])
        //            {
        //                var aux = arrayCopia[i];
        //                arrayCopia[i] = arrayCopia[j];
        //                arrayCopia[j] = aux;
        //            }
        //        }
        //    }
        //    return arrayCopia;
        //}


        //private static double[] OrdenarArrayAsc(double[] arrayCopia)
        //{
        //    for (int i = 0; i < arrayCopia.Length-1; i++)
        //    {
        //        for (int j = i+1; j < arrayCopia.Length; j++)
        //        {
        //            if (arrayCopia[i] > arrayCopia[j])
        //            {
        //                var aux = arrayCopia[i];
        //                arrayCopia[i] = arrayCopia[j];
        //                arrayCopia[j] = aux;
        //            }
        //        }
        //    }
        //    return arrayCopia;
        //}

        private static void MostrarInferioresAlPromedio(double[] temperaturas)
        {
            var promedio = temperaturas.Average();
            Console.Clear();
            Console.WriteLine("Mostrar Inferiores al Promedio");
            Console.WriteLine($"Promedio={promedio.ToString("N2")}");
            var tabla = new ConsoleTable("Celsius");
            var arrayMenores=temperaturas.Where(t=>t<promedio).ToArray();
            foreach (var tempEnArray in arrayMenores)
            {
                tabla.AddRow(tempEnArray);
            }
            Console.WriteLine(tabla.ToString());
            TareaFinalizada("Inferiores al promedio...");

        }

        private static void MarcarMayoresPromedio(double[] temperaturas)
        {
            var promedio = temperaturas.Average();
            Console.Clear();
            Console.WriteLine("Marcar Superiores al Promedio");
            Console.WriteLine($"Promedio={promedio.ToString("N2")}");
            var tabla = new ConsoleTable("Celsius", "Sup. al Prom?");
            foreach (var tempEnArray in temperaturas)
            {
                if (tempEnArray > promedio)
                {
                    tabla.AddRow(tempEnArray, "*");
                }
                else
                {
                    tabla.AddRow(tempEnArray, "");
                }
            }
            Console.WriteLine(tabla.ToString());
            TareaFinalizada("Superiores al promedio...");
        }
        private static void DatosEstadisticos(double[] temperaturas)
        {
            ListarTemperaturas(temperaturas);
            var maxTemp = temperaturas.Max();
            var minTemp = temperaturas.Min();
            var promTemp = temperaturas.Average();
            Console.WriteLine($"Mayor temperatura={maxTemp}");
            Console.WriteLine($"Menor temperatura={minTemp}");
            Console.WriteLine($"Promedio temperatura={promTemp.ToString("N2")}");
            Console.WriteLine();
            TareaFinalizada("Datos Estadísticos...");
        }

        private static void ModificarDatos(double[] temperaturas)
        {
            do
            {
                Console.Clear();
                Console.WriteLine("Modificacion de Datos");
                ListarTemperaturas(temperaturas);

                var index =
                     IngresoDatos.PedirIntEnRango("Ingrese un indice de elemento:", 1, temperaturas.Length);
                Console.WriteLine($"Valor anterior:{temperaturas[index - 1]}");
                double nuevaTemperatura;
                do
                {
                    nuevaTemperatura =
                        IngresoDatos.PedirDoubleEnRango("Ingrese nueba temperatura:",
                       MIN_TEMPERATURA, MAX_TEMPERATURA);
                    if (Existe(nuevaTemperatura, temperaturas))
                    {
                        Console.WriteLine("Temperatura existente!!!");
                    }
                    else
                    {
                        break;
                    }
                } while (true);
                temperaturas[index - 1] = nuevaTemperatura;
                var sigueModificando =
                     IngresoDatos.PedirCharEnRango("¿Desea modificar otro?(S/N)", 's', 'n');
                if (sigueModificando == "N")
                {
                    break;
                }
            } while (true);
            Console.WriteLine();
            TareaFinalizada("Modificacion Finalizada...");
        }

       

        private static void ListarTemperaturas(double[] temperaturas)
        {
            Console.Clear();
            Console.WriteLine("Listado de Temperaturas");
            var tabla = new ConsoleTable("Celcius", "Fahrenheit"); ;
            foreach (Double tempEnArray in temperaturas) 
            {
                var fahrenheit = ConvertToFah(tempEnArray);
                tabla.AddRow(tempEnArray, fahrenheit);
            }
            Console.WriteLine(tabla.ToString());
            TareaFinalizada("Listado Finalizado");
        }

        private static double ConvertToFah(double celcius) => 1.8 * celcius + 32;

        private static void GenerarLasTemperaturas(double[] temperaturas)
        {
            Console.Clear();
            Console.WriteLine("Ingreso de Temperaturas");
            for (int i = 0; i < temperaturas.Length; i++)
            {
                double tempIngresada;
                do
                {
                    tempIngresada =
                         IngresoDatos.PedirDoubleEnRango("Ingrese una temperatura:", MIN_TEMPERATURA, MAX_TEMPERATURA);
                    if (Existe(tempIngresada, temperaturas))
                    {
                        Console.WriteLine("Temperatura Existente!!!");
                    }
                    else
                    {
                        break;
                    }

                } while (true);
                temperaturas[i]=tempIngresada;

            }
            TareaFinalizada("Ingreso Finalizado");
        }

        private static bool Existe(double tempIngresada, double[] temperaturas)
        {
            //return Array.IndexOf(temperaturas, tempIngresada)>-1;
            return temperaturas.Contains(tempIngresada);
        }

        private static void TareaFinalizada(string mensaje)
        {
            Console.Write($"{mensaje}... ENTER para continuar");
            Console.ReadLine();
        }
         
        private static void MostrarMenu()
        {
            Console.Clear();
            Console.WriteLine("1-Ingresar Datos");
            Console.WriteLine("2-Modificar Datos");
            Console.WriteLine("3-Listar Temperaturas con Equivalentes");
            Console.WriteLine("4-Datos Estadisticos");
            Console.WriteLine("5-Ver Superiores al Promedio");
            Console.WriteLine("6-Mostrar Inferiores al Promedio");
            Console.WriteLine("7-Mostrar Ordenados ASC");
            Console.WriteLine("8-Mostrar Ordenados DESC");
            Console.WriteLine("9-Salida");
        }
        
    }
}