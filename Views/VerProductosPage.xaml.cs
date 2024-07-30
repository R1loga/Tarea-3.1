using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarea_3._1.Models;
using Tarea_3._1.Services;


namespace Tarea_3._1.Views
{
    public partial class VerProductosPage : ContentPage
    {
        private readonly FirebaseService _firebaseService;

        public VerProductosPage()
        {
            InitializeComponent();
            _firebaseService = new FirebaseService();
            CargarProductos();
        }

        private async void CargarProductos()
        {
            var productos = await _firebaseService.LeerProductosAsync();
            productosListView.ItemsSource = productos;
        }

        private async void OnActualizarClicked(object sender, EventArgs e)
        {
            var swipeItem = sender as SwipeItem;
            var producto = swipeItem?.BindingContext as Producto;

            if (producto != null)
            {
                // Navegar a una p�gina de actualizaci�n o mostrar un formulario para actualizar el producto
                await Navigation.PushAsync(new ActualizarProductoPage(producto));
            }
        }

        private async void OnEliminarClicked(object sender, EventArgs e)
        {
            var swipeItem = sender as SwipeItem;
            var producto = swipeItem?.BindingContext as Producto;

            if (producto != null)
            {
                var confirm = await DisplayAlert("Confirmar Eliminaci�n", "�Est�s seguro de que quieres eliminar este producto?", "S�", "No");
                if (confirm)
                {
                    await _firebaseService.EliminarProductoAsync(producto.Id);
                    await DisplayAlert("�xito", "Producto eliminado exitosamente", "OK");
                    CargarProductos();
                }
            }
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            // Manejar la selecci�n del producto si es necesario
        }
    }
}
