using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace DhcpProba.ViewModel
{
    class DhcpViewModel : ViewModelBase
    {
        #region Events
        public event EventHandler<ClientPopupEventArg> OnPopUpOpen;
        public event EventHandler<ClientPopupEventArg> OnBeallitasokOpen;
        #endregion
        private Model.Dhcp _model;
        private DispatcherTimer _timer;
        #region DataBindings
        private int _leasesselected;
        public int LeasesSelected
        {
            get
            {
                return _leasesselected;
            }
            set
            {
                _leasesselected = value;
                OnPropertyChanged();
            }
        }
        

        private ObservableCollection<String> _leaseslist;
        private ObservableCollection<String> _reservedlist;
        public ObservableCollection<String> LeasesList
        {
            get
            {
                if (_leaseslist == null)
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

        #endregion
        #region DelegateCommands
        public DelegateCommand Kliensuzenet { get; private set; }
        public DelegateCommand Foglalas { get; private set; }
        public DelegateCommand FoglalasTorles { get; private set; }
        public DelegateCommand Beallitas { get; private set; }
        public DelegateCommand Load { get; private set; }
        public DelegateCommand Save { get; private set; }
        #endregion



        public DhcpViewModel(Model.Dhcp model)
        {
            _model = model;
            Kliensuzenet = new DelegateCommand((param) =>
            {
                RaiseOpenClientNamePopup();
                
            });
            Foglalas = new DelegateCommand((param) =>
            {
                model.AddReservation(_leasesselected);
               
            });
            FoglalasTorles = new DelegateCommand((param) =>
            {
                model.ClearReservation();

            });
            Beallitas= new DelegateCommand((param) =>
            {
                RaiseOpenBeallitasokPopup();
            });
            Save = new DelegateCommand((param) =>
            {
                _model.Save();
            });
            Load = new DelegateCommand((param) =>
            {
                _model.Load();
            });
            model.OnLeasesFull += Model_OnLeasesFull;
            model.OnListUpdated += UpdateLists;

            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0,0,1);
            _timer.Tick += _timer_Tick;
            _timer.Start();
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            _model.RemoveLeasesElement();
        }

        private void Model_OnLeasesFull(object sender, EventArgs e)
        {
            MessageBox.Show("Leases List is Full");
        }

        public void RaiseOpenClientNamePopup()
        {
            OnPopUpOpen?.Invoke(this, new ClientPopupEventArg(""));
        }
        public void RaiseOpenBeallitasokPopup()
        {
            OnBeallitasokOpen?.Invoke(this, new ClientPopupEventArg(""));
        }

        public void OnClientNameAdded(object sender, ClientPopupEventArg arg)
        {
            _model.RequestNewIP(Int64.Parse(arg.Message, System.Globalization.NumberStyles.HexNumber));
        }

        public void OnBerletIdoChanged(object sender, ClientPopupEventArg arg)
        {
            _model.ChangeBerletido(arg.Message);
        }
        private void UpdateLists(object sender,EventArgs args)
        {
            
            LeasesList.Clear();
            ReservedList.Clear();
            foreach(DhcpProba.Model.Dhcp.leaseElement e in _model.LeasesList)
            {
                LeasesList.Add("MAC:" +Convert.ToString(e.MAC,16) + " Ip:" + e.IP.GetString() + " Lejarat:" +TimeSpan.FromSeconds(e.LejaratiIdo).ToString());
            }
            foreach (DhcpProba.Model.Dhcp.resElement e in _model.ReservationList)
            {
                ReservedList.Add("MAC:" + Convert.ToString(e.MAC,16) + " Ip:" + e.IP.GetString());
            }
        }
        
    }

}
