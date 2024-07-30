using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Tarea_3._1.Models;

namespace Tarea_3._1.Models
{
    public class Producto : INotifyPropertyChanged
    {
        private string _id;
        private string _nombre;
        private string _descripcion;
        private double _precio;  
        private string _foto;

        public string Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        public string Nombre
        {
            get => _nombre;
            set
            {
                _nombre = value;
                OnPropertyChanged();
            }
        }

        public string Descripcion
        {
            get => _descripcion;
            set
            {
                _descripcion = value;
                OnPropertyChanged();
            }
        }

        public double Precio
        {
            get => _precio;
            set
            {
                _precio = value;
                OnPropertyChanged();
            }
        }

        public string Foto
        {
            get => _foto;
            set
            {
                _foto = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
