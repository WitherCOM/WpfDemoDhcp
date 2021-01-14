using System;
using System.Collections.Generic;
using System.Text;

namespace DhcpProba.ViewModel
{
    class PopupViewModel : ViewModelBase
    {
        public event EventHandler<ClientPopupEventArg> OnClientNameAdded;
        public event EventHandler<EventArgs> OnCloseWindow;

        private string _edittext;
        public string EditText { get { return _edittext; } set { _edittext = value; OnPropertyChanged(); } }

        public DelegateCommand Confirm { get; private set; }
        public DelegateCommand Cancel{ get; private set; }
        public PopupViewModel()
        {
            Confirm = new DelegateCommand((param) =>
            {
                RaiseOnClientNameAdded(EditText);
                RaiseOnCloseWindow();
            });

            Cancel = new DelegateCommand((param) =>
            {
                RaiseOnCloseWindow();
            });
        }

        public void RaiseOnClientNameAdded(string name)
        {
            OnClientNameAdded?.Invoke(this, new ClientPopupEventArg(name));
        }
        public void RaiseOnCloseWindow()
        {
            OnCloseWindow?.Invoke(this, new EventArgs());
        }
    }
}
