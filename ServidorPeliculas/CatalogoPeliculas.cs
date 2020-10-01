using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ServidorPeliculas
{
    public class CatalogoPeliculas
    {
        public ObservableCollection<Pelicula> ListaPeliculas { get; set; } = new ObservableCollection<Pelicula>();

        public void Agregar(Pelicula p)
        {
            ListaPeliculas.Add(p);
            Guardar();
        }
        public void Editar(Pelicula p)
        {
            var pelicula = ListaPeliculas.FirstOrDefault(x => x.Nombre == p.Nombre & x.Hora==p.Hora);

            if(pelicula!=null)
            {
                pelicula.Idioma = p.Idioma;
                pelicula.Sala = p.Sala;
                Guardar();
            }
        }
        public void Eliminar(Pelicula p)
        {
            var pelicula = ListaPeliculas.FirstOrDefault(x => x.Nombre == p.Nombre & x.Hora == p.Hora);
            if(pelicula!=null)
            {
                ListaPeliculas.Remove(pelicula);
            }
        }

        private void Guardar()
        {
            string datos = JsonConvert.SerializeObject(ListaPeliculas);
            File.WriteAllText("peliculas.json", datos);
        }
        private void Cargar()
        {
            if (File.Exists("peliculas.json"))
            {
                var datos = JsonConvert.DeserializeObject<ObservableCollection<Pelicula>>(File.ReadAllText("peliculas.json"));

                foreach (var item in datos)
                {
                    ListaPeliculas.Add(item);
                }
            }
        }
        public CatalogoPeliculas()
        {
            Cargar();
        }
    }
}
