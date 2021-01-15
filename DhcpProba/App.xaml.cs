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

        ViewModel.DhcpViewModel dhcpviewmodel;
        public App()
        {
            Startup += App_Startup;
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            Model.Dhcp model = new Model.Dhcp();
            MainWindow mainwindow = new MainWindow();
            
            Model.Dhcp dhcpmodel = new Model.Dhcp();
            dhcpviewmodel = new ViewModel.DhcpViewModel(model);
            
            
            mainwindow.DataContext = dhcpviewmodel;

            dhcpviewmodel.OnPopUpOpen += Dhcpviewmodel_OnPopUpOpen;
            dhcpviewmodel.OnBeallitasokOpen += Dhcpviewmodel_OnBeallitasokOpen;

            


            mainwindow.Show();
        }

        private void Dhcpviewmodel_OnBeallitasokOpen(object sender, ViewModel.ClientPopupEventArg e)
        {
            View.BeallitasokPopup beallitasok = new View.BeallitasokPopup();
            ViewModel.BeallitasokViewModel beallviewmodel = new ViewModel.BeallitasokViewModel();
            beallitasok.DataContext = beallviewmodel;
            beallviewmodel.OnChangeBerletIdo += dhcpviewmodel.OnBerletIdoChanged;
            beallviewmodel.OnCloseWindow += beallitasok.KliensUzenet_OnCloseWindow;
            beallitasok.Show();
        }

        private void Dhcpviewmodel_OnPopUpOpen(object sender, ViewModel.ClientPopupEventArg e)
        {
            View.KliensUzenet popupklient = new View.KliensUzenet();
            ViewModel.PopupViewModel popupviewmodel = new ViewModel.PopupViewModel();
            popupklient.DataContext = popupviewmodel;
            popupviewmodel.OnClientNameAdded += dhcpviewmodel.OnClientNameAdded;
            popupviewmodel.OnCloseWindow += popupklient.KliensUzenet_OnCloseWindow;
            popupklient.Show();
        }
    }
}
