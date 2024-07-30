using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using Tarea_3._1.Models;
using Tarea_3._1.Services;

namespace Tarea_3._1.ViewModels
{
    public class ProductoViewModel : INotifyPropertyChanged
    {
        private readonly FirebaseService _firebaseService;
        private ObservableCollection<Producto> _productos;
        private Producto _productoSeleccionado;

        public ProductoViewModel()
        {
            _firebaseService = new FirebaseService();
            Productos = new ObservableCollection<Producto>();
            
            ProductoSeleccionado = new Producto(); // Inicializa el producto seleccionado
        }

        public ObservableCollection<Producto> Productos
        {
            get => _productos;
            set
            {
                _productos = value;
                OnPropertyChanged();
            }
        }

        public Producto ProductoSeleccionado
        {
            get => _productoSeleccionado;
            set
            {
                _productoSeleccionado = value;
                OnPropertyChanged();
            }
        }

        public async Task CrearProductoAsync()
        {
            ProductoSeleccionado.Id = Guid.NewGuid().ToString();
            await _firebaseService.CrearProductoAsync(ProductoSeleccionado);
            Productos.Add(ProductoSeleccionado);
            ProductoSeleccionado = new Producto(); // Reinicia el producto seleccionado después de crear
        }

        public async Task CargarProductosAsync()
        {
            var productos = await _firebaseService.LeerProductosAsync();
            Productos.Clear();
            foreach (var producto in productos)
            {
                Productos.Add(producto);
            }
        }

        public async Task ActualizarProductoAsync()
        {
            if (ProductoSeleccionado != null)
            {
                await _firebaseService.ActualizarProductoAsync(ProductoSeleccionado);
                var index = Productos.IndexOf(Productos.FirstOrDefault(p => p.Id == ProductoSeleccionado.Id));
                Productos[index] = ProductoSeleccionado;
                ProductoSeleccionado = new Producto(); // Reinicia el producto seleccionado después de actualizar
            }
        }

        public async Task EliminarProductoAsync()
        {
            if (ProductoSeleccionado != null)
            {
                await _firebaseService.EliminarProductoAsync(ProductoSeleccionado.Id);
                Productos.Remove(ProductoSeleccionado);
                ProductoSeleccionado = new Producto(); // Reinicia el producto seleccionado después de eliminar
            }
        }

        public ICommand CrearProductoCommand => new Command(async () => await CrearProductoAsync());
        public ICommand ActualizarProductoCommand => new Command(async () => await ActualizarProductoAsync());
        public ICommand EliminarProductoCommand => new Command(async () => await EliminarProductoAsync());

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}



