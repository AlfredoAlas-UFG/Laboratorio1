using Laboratorio_1.Models;
using System.Collections.Generic;

namespace Laboratorio_1.Service
{
    /// <summary>
    /// Representa la lista de articulos disponibles
    /// </summary>
    public static class ArticloService
    {
        public static List<Articulo> listaArticulos = new List<Articulo>
        {
            new Articulo { nombreArticulo = "Camisa", precio = 15.00 },
            new Articulo { nombreArticulo = "Cinturón", precio = 8.00 },
            new Articulo { nombreArticulo = "Zapatos", precio = 40.00 },
            new Articulo { nombreArticulo = "Pantalón", precio = 25.00 },
            new Articulo { nombreArticulo = "Calcetines", precio = 2.50 },
            new Articulo { nombreArticulo = "Faldas", precio = 18.00 },
            new Articulo { nombreArticulo = "Gorras", precio = 9.00 },
            new Articulo { nombreArticulo = "Suéter", precio = 19.00 },
            new Articulo { nombreArticulo = "Corbatas", precio = 12.00 },
            new Articulo { nombreArticulo = "Chaquetas", precio = 35.00 },
        };

        
    }
}
