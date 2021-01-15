using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;

namespace DhcpProba.Model
{
    class Dhcp
    {
        private int berletiido;

        public class Ip
        {
            public int a { get; set; }
            public int b { get; set; }
            public int c { get; set; }
            public int d { get; set; }

            public Ip(int _a = 0, int _b = 0, int _c = 0, int _d = 0)
            {
                a = _a;
                b = _b;
                c = _c;
                d = _d;
            }
            public Ip(string ip)
            {
                String[] i = ip.Split('.');
                a = Int32.Parse(i[0]);
                b = Int32.Parse(i[1]);
                c = Int32.Parse(i[2]);
                d = Int32.Parse(i[3]);
            }
        }
        public class leaseElement
        {
            private Int64 _mac;
            private Ip _ip;
            private int _lejaratido;

            public Int64 MAC
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

            public Ip IP
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
            public leaseElement(Int64 mac,Ip ip, int lejaratido)
            {
                _mac = mac;
                _ip = ip;
                _lejaratido = lejaratido;
            }
        }
        public class resElement
        {
            private Int64 _mac;
            private Ip _ip;

            public Int64 MAC
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

            public Ip IP
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
            public resElement(Int64 mac,Ip ip)
            {
                _mac = mac;
                _ip = ip;
            }
        }
        private List<resElement> _reservations;

        private List<leaseElement> _leases;

        public List<resElement> ReservationList
        {
            get
            {
                return _reservations;
            }
        }

        public List<leaseElement> LeasesList
        {
            get
            {
                return _leases;
            }
        }

        public void RequestNewIP(Int64 mac)
        {
            if (_leases.Count >= 245) return;
            foreach(resElement e in _reservations)
            {
                if(e.MAC == mac)
                {
                    bool canAdd = false;
                    foreach(leaseElement e2 in _leases)
                    {
                        if (e2.IP == e.IP)
                            canAdd = true;
                    }
                    if (!canAdd) break;
                    _leases.Add(new leaseElement(e.MAC,e.IP,berletiido));
                    break;
                }
            }
            for(int i = 10;i <=244;++i)
            {
                bool canAdd = true;
                foreach (leaseElement e2 in _leases)
                {
                    if (e2.IP == new Ip(21,1,7,i))
                    {
                        canAdd = false;
                    }
                }
                if(canAdd) _leases.Add(new leaseElement(mac, new Ip(21, 1, 7, i), berletiido));
            }
        }

        public Dhcp()
        {

        }
    }
}
