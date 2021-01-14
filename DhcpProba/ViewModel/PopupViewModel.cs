﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DhcpProba.ViewModel
{
    class PopupViewModel : ViewModelBase
    {
        public event EventHandler<ClientPopupEventArg> OnClientNameAdded;

        public PopupViewModel()
        {

        }

        public void AddClientName(string name)
        {
            OnClientNameAdded?.Invoke(this, new ClientPopupEventArg(name));
        }
    }
}