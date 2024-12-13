using PaddleOCR_GUI.Views;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Windows;

namespace PaddleOCR_GUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Process_Method>();
            containerRegistry.RegisterForNavigation<Python_NET_Method>();
            containerRegistry.RegisterForNavigation<PaddleOCRSettings>();
        }
    }

}
