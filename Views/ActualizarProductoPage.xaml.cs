using System;
using System.Threading.Tasks;
using Tarea_3._1.Models;
using Tarea_3._1.Services;


namespace Tarea_3._1.Views
{
    public partial class ActualizarProductoPage : ContentPage
    {
        private readonly FirebaseService _firebaseService;
        private Producto _producto;

        public ActualizarProductoPage(Producto producto)
        {
            InitializeComponent();
            _firebaseService = new FirebaseService();
            _producto = producto;

            // Inicializar campos con datos del producto
            nombreEntry.Text = _producto.Nombre;
            descripcionEntry.Text = _producto.Descripcion;
            precioEntry.Text = _producto.Precio.ToString();
        }

        private async void OnActualizarClicked(object sender, EventArgs e)
        {
            _producto.Nombre = nombreEntry.Text;
            _producto.Descripcion = descripcionEntry.Text;
            _producto.Precio = double.Parse(precioEntry.Text);

            await _firebaseService.ActualizarProductoAsync(_producto);
            await DisplayAlert("Éxito", "Producto actualizado exitosamente", "OK");
            await Navigation.PopAsync();
        }
    }
}
