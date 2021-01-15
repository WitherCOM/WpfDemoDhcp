using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using System.Windows.Threading;

namespace DhcpProba.Model
{
    class Dhcp
    {       

        #region Events
        public event EventHandler<EventArgs> OnLeasesFull;
        public event EventHandler<EventArgs> OnListUpdated;
        #endregion

        #region InnerClasses
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
            
            public string GetString()
            {
                return a.ToString() + "." + b.ToString() + "." + c.ToString() + "." + d.ToString();
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
        #endregion

        private List<resElement> _reservations;

        private List<leaseElement> _leases;

        private int berletiido;

        private Persistance.Persistance _persistance;

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
            if (_leases.Count >= 245)
            {
                RaiseOnLeasesFull();
                return;
            }
            bool isReserved = false;
            foreach(resElement e in _reservations)
            {
                if(e.MAC == mac)
                {
                    bool canAdd = true;
                    foreach(leaseElement e2 in _leases)
                    {
                        if (e2.IP.GetString() == e.IP.GetString())
                            canAdd = false;
                    }
                    if (!canAdd) break;
                    isReserved = true;
                    _leases.Add(new leaseElement(e.MAC,e.IP, DateTime.Now.Second + DateTime.Now.Minute * 60 + DateTime.Now.Hour * 3600 + berletiido));
                    break;
                }
            }
            if(!isReserved)
            for(int i = 10;i <=244;++i)
            {
                bool canAdd = true;
                foreach (leaseElement e2 in _leases)
                {
                    if (e2.IP.GetString() == (new Ip(21,1,7,i)).GetString())
                    {
                        canAdd = false;
                    }
                }
                if (canAdd)
                {
                    _leases.Add(new leaseElement(mac, new Ip(21, 1, 7, i), DateTime.Now.Second + DateTime.Now.Minute*60 + DateTime.Now.Hour*3600 + berletiido));
                    break;
                }
            }

            RaiseOnListUpdated();
        }

        public void AddReservation(int selection)
        {
            if (selection == -1) return;
            _reservations.Add(new resElement(_leases[selection].MAC, _leases[selection].IP));
            RaiseOnListUpdated();
        }

        public void ClearReservation()
        {
            _reservations.Clear();
            RaiseOnListUpdated();
        }

        public void ChangeBerletido(string ido)
        {
            berletiido = Int32.Parse(ido);
        }

        public void RemoveLeasesElement()
        {
            int now = DateTime.Now.Second + DateTime.Now.Minute * 60 + DateTime.Now.Hour * 3600;
            if (_leases.Count == 0) return;
            foreach (leaseElement l in _leases)
            {
                if (l.LejaratiIdo <= now)
                {
                    _leases.Remove(l);
                    RaiseOnListUpdated();
                    break;
                }
            }
            
        }

        public void Save()
        {
            _persistance.Save(_leases, _reservations);
        }

        public void Load()
        {
            _persistance.Load();
        }
        public Dhcp()
        {
            berletiido = 10;
            _persistance = new Persistance.Persistance();
            _persistance.OnLoad += _persistance_OnLoad;
            _leases = new List<leaseElement>();
            _reservations = new List<resElement>();          
        }

        private void _persistance_OnLoad(object sender, Persistance.PersistanceEventArgs e)
        {
            _reservations = e.Reserv;
            _leases = e.Leases;
            RaiseOnListUpdated();
        }

        private void RaiseOnLeasesFull()
        {
            OnLeasesFull?.Invoke(this, new EventArgs());
        }

        private void RaiseOnListUpdated()
        {
            OnListUpdated?.Invoke(this, new EventArgs());
        }
    }
}
