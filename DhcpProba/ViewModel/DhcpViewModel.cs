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

        private Model.Dhcp _model;

        private ObservableCollection<String> _leaseslist;
        private ObservableCollection<String> _reservedlist;

        public DelegateCommand Kliensuzenet { get; private set; }

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
        public ObservableCollection<String> ReservedList
        {
            get
            {
                if (_reservedlist == null)
                {
                    _reservedlist = new ObservableCollection<String>();
                }
                return _reservedlist;
            }
        }

        public DhcpViewModel()
        {
            Kliensuzenet = new DelegateCommand((param) =>
            {
                RaiseOpenClientNamePopup();
            });
        }

        public void RaiseOpenClientNamePopup()
        {
            OnPopUpOpen?.Invoke(this, new ClientPopupEventArg(""));
        }

        public void OnClientNameAdded(object sender, ClientPopupEventArg arg)
        {

        }
        private void UpdateLists()
        {
            _leaseslist.Clear();
            _reservedlist.Clear();
            foreach(DhcpProba.Model.Dhcp.listElement e in _model.LeasesList)
            {
                _leaseslist.Add(e.MAC + " " + e.IP + " " + e.LejaratiIdo);
            }
            foreach (DhcpProba.Model.Dhcp.listElement e in _model.ReservationList)
            {
                _reservedlist.Add(e.MAC + " " + e.IP + " " + e.LejaratiIdo);
            }
        }
        
    }

}
