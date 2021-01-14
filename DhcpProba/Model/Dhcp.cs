using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;

namespace DhcpProba.Model
{
    class Dhcp : INotifyPropertyChanged
    {

        
        #region INotifyPropertyChanged Members  

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
