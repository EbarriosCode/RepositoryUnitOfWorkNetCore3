using Entities.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.DataContext.Data
{
    public static class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var _context = new WebApiDbContext(serviceProvider.GetRequiredService<DbContextOptions<WebApiDbContext>>()))
            {
                // Agregando Artistas a la BD
                if (_context.Artistas.Any())
                {
                    return;
                }

                _context.Artistas.AddRange(
                    new Artista { Nombre = "Luis Miguel" },
                    new Artista { Nombre = "Ricardo Arjona" },
                    new Artista { Nombre = "Kalimba" }
                 );

                _context.SaveChanges();


                // Agregando Albumes a la BD
                if (_context.Albumes.Any())
                {
                    return;
                }

                _context.Albumes.AddRange(
                    new Album
                    {
                        ArtistaID = _context.Artistas.FirstOrDefault(a => a.Nombre.Equals("Kalimba")).ArtistaID,
                        Titulo = $"Mi Otro Yo",
                        Precio = 200,
                        Anio = 2008
                    },

                    new Album
                    {
                        ArtistaID = _context.Artistas.FirstOrDefault(a => a.Nombre.Equals("Kalimba")).ArtistaID,
                        Titulo = $"Aerosoul",
                        Precio = 275,
                        Anio = 2004
                    },

                    new Album
                    {
                        ArtistaID = _context.Artistas.FirstOrDefault(a => a.Nombre.Equals("Ricardo Arjona")).ArtistaID,
                        Titulo = $"Circo Soledad",
                        Precio = 180,
                        Anio = 2017
                    },

                    new Album
                    {
                        ArtistaID = _context.Artistas.FirstOrDefault(a => a.Nombre.Equals("Luis Miguel")).ArtistaID,
                        Titulo = $"Romance",
                        Precio = 290,
                        Anio = 1991
                    }
                );

                _context.SaveChanges();
            }
        }
    }
}
