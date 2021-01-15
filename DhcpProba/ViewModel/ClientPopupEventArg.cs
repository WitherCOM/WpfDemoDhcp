using System;
using System.Collections.Generic;
using System.Text;

namespace DhcpProba.ViewModel
{
    class ClientPopupEventArg  : EventArgs
    {
        private string _message;
        public string Message { get { return _message; } set { _message = value; } }
        public ClientPopupEventArg(string msg)
        {
            Message = msg;
        }
    }
}
