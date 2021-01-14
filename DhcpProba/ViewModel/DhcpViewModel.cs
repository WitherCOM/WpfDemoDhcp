using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace DhcpProba.ViewModel
{
    class DhcpViewModel : ViewModelBase
    {
        public event EventHandler<ClientPopupEventArg> OnPopUpOpen;
        
        private ObservableCollection<String> _leaseslist;

        public ObservableCollection<String> LeasesList
        {
            get
            {
                if(_leaseslist == null)
                {
                    _leaseslist = new ObservableCollection<String>();
                }
                return _leaseslist;
            }
        }
        
        public DhcpViewModel()
        {
            
        }

        public void OpenClientNamePopup()
        {
            OnPopUpOpen?.Invoke(this, new ClientPopupEventArg(""));
        }

        public void OnClientNameAdded(object sender, ClientPopupEventArg arg)
        {

        }
        
    }

}
