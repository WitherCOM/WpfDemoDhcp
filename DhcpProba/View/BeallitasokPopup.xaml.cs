using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DhcpProba.View
{
    /// <summary>
    /// Interaction logic for BeallitasokPopup.xaml
    /// </summary>
    public partial class BeallitasokPopup : Window
    {
        public BeallitasokPopup()
        {
            InitializeComponent();
        }
        public void KliensUzenet_OnCloseWindow(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
