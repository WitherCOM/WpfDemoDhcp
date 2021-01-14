using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace DhcpProba
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        View.KliensUzenet _popupklient;


        public App()
        {
            Startup += App_Startup;
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            MainWindow mainwindow = new MainWindow();
            View.KliensUzenet _popupklient = new View.KliensUzenet();
            Model.Dhcp dhcpmodel = new Model.Dhcp();
            ViewModel.DhcpViewModel dhcpviewmodel = new ViewModel.DhcpViewModel();
            ViewModel.PopupViewModel popupviewmodel = new ViewModel.PopupViewModel();
            _popupklient.DataContext = popupviewmodel;
            mainwindow.DataContext = dhcpviewmodel;

            dhcpviewmodel.OnPopUpOpen += Dhcpviewmodel_OnPopUpOpen;
            popupviewmodel.OnClientNameAdded += dhcpviewmodel.OnClientNameAdded;

            mainwindow.Show();
        }

        private void Dhcpviewmodel_OnPopUpOpen(object sender, ViewModel.ClientPopupEventArg e)
        {
            _popupklient.Show();
        }
    }
}
