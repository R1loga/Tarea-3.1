using Microsoft.Maui.Controls;
using Tarea_3._1.Views;


namespace Tarea_3._1
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new ProductosView());
        }
    }
}
