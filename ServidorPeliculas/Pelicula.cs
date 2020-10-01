using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServidorPeliculas
{
    public enum Sala { A1 = 0, A2 = 1, B1 = 2, B2 = 3 };
    public enum Clasificacion { A = 0, B15 = 1, C = 2, R = 3 };
    public enum Idioma { Español = 0, Subtitulada = 1 };
    public class Pelicula : INotifyPropertyChanged
    {
        private string hora;
        private string nombre;
        private Sala sala;
        private Clasificacion clasificacion;
        private Idioma idioma;

        public string Hora
        {
            get => hora;
            set
            {
                hora = value;
                LanzarEvento("Hora");
            }
        }
        public string Nombre
        {
            get => nombre;
            set
            {
                nombre = value;
                LanzarEvento("Nombre");
            }
        }
        public Sala Sala
        {
            get => sala; set
            {
                sala = value;
                LanzarEvento("Sala");
            }
        }
        public Clasificacion Clasificacion
        {
            get => clasificacion;
            set
            {
                clasificacion = value;
                LanzarEvento("Clasificacion");
            }
        }
        public Idioma Idioma
        {
            get => idioma;
            set
            {
                idioma = value;
                LanzarEvento("Idioma");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void LanzarEvento(string propiedad)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("propiedad"));
        }
    }
}
