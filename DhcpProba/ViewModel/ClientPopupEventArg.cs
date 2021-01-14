using System;
using System.Collections.Generic;
using System.Text;

namespace DhcpProba.ViewModel
{
    class ClientPopupEventArg  : EventArgs
    {
        public enum CallType
        {
            Uzenetet,Foglalas,Torles
        }
        private string _message;
        private CallType _calltype;
        public string Message { get { return _message; } set { _message = value; } }
        public CallType Calltype { get { return _calltype; } set { _calltype = value; } }
        public ClientPopupEventArg(string msg)
        {
            Message = msg;
        }
    }
}
