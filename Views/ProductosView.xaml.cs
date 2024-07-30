using Microsoft.Maui.Controls;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.IO;
using Tarea_3._1.ViewModels;
using Tarea_3._1.Models;  



namespace Tarea_3._1.Views
{
    public partial class ProductosView : ContentPage
    {
        private MediaFile _file;

        public ProductosView()
        {
            InitializeComponent();
        }

        private void OnProductoSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is Producto producto)
            {
                // Establece el producto seleccionado en el ViewModel
                ((ProductoViewModel)BindingContext).ProductoSeleccionado = producto;
            }
        }

        private async void OnTakePhotoClicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", "No camera available.", "OK");
                return;
            }

            _file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                Directory = "Sample",
                Name = "test.jpg"
            });

            if (_file == null)
                return;

            var filePath = Path.Combine(FileSystem.AppDataDirectory, _file.Path.Substring(_file.Path.LastIndexOf(Path.DirectorySeparatorChar) + 1));
            using (var stream = _file.GetStream())
            using (var newStream = File.OpenWrite(filePath))
            {
                await stream.CopyToAsync(newStream);
            }

            var viewModel = BindingContext as ProductoViewModel;
            if (viewModel != null)
            {
                viewModel.ProductoSeleccionado.Foto = filePath;
            }

            CapturedImage.Source = ImageSource.FromStream(() =>
            {
                var stream = _file.GetStream();
                return stream;
            });
        }

        private void OnSavePhotoClicked(object sender, EventArgs e)
        {
            var viewModel = BindingContext as ProductoViewModel;
            if (viewModel == null)
                return;

            if (_file == null)
            {
                DisplayAlert("Error", "No photo to save.", "OK");
                return;
            }

           
            DisplayAlert("Foto Guardada", $"Foto guardada en: {viewModel.ProductoSeleccionado.Foto}", "OK");

            _file.Dispose();
            _file = null;
        }
        private async void OnVerProductosClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new VerProductosPage());
        }
    }
}
