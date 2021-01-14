using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;

namespace DhcpProba.Model
{
    class Dhcp
    {
        class listElement
        {
            private string _mac;
            private string _ip;
            private int _lejaratido;

            public string MAC
            {
                get
                {
                    return _mac;
                }
                set
                {
                    _mac = value;
                }
            }

            public string IP
            {
                get
                {
                    return _ip;
                }
                set
                {
                    _ip = value;
                }
            }

            public int LejaratiIdo
            {
                get
                {
                    return _lejaratido;
                }
                set
                {
                    _lejaratido = value;
                }
            }
            public listElement()
            {

            }
        }
        private List<listElement> _reservations;

        private List<listElement> _leases;
        public Dhcp()
        {

        }
    }
}
