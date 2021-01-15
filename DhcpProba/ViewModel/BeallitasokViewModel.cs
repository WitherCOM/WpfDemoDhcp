using System;
using System.Collections.Generic;
using System.Text;

namespace DhcpProba.ViewModel
{
    class BeallitasokViewModel : ViewModelBase
    {
        #region DataBindings
        private string _berletidoedit;
        public string BerletidoEdit
        {
            get
            {
                return _berletidoedit;
            }
            set
            {
                _berletidoedit = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Events
        public event EventHandler<ClientPopupEventArg> OnChangeBerletIdo;
        public event EventHandler<EventArgs> OnCloseWindow;
        #endregion

        #region DelegateCommands
        public DelegateCommand Change { get; private set; }
        public DelegateCommand Cancel { get; private set; }
        #endregion

        public BeallitasokViewModel()
        {
            Change = new DelegateCommand((param) =>
            {
                RaiseOnChangeBerletIdo(_berletidoedit);
                RaiseOnCloseWindow();
            });

            Cancel = new DelegateCommand((param) =>
            {
                RaiseOnCloseWindow();
            });

        }

        private void RaiseOnChangeBerletIdo(string ido)
        {
            OnChangeBerletIdo?.Invoke(this, new ClientPopupEventArg(ido));
        }

        private void RaiseOnCloseWindow()
        {
            OnCloseWindow?.Invoke(this, new EventArgs());
        }
    }
}
