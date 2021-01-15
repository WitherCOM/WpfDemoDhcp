using System;
using System.Collections.Generic;
using System.Text;

namespace DhcpProba.Persistance
{
    class PersistanceEventArgs : EventArgs
    {
        private List<Model.Dhcp.leaseElement> _leases;
        private List<Model.Dhcp.resElement> _res;

        public List<Model.Dhcp.leaseElement> Leases
        {
            get
            {
                return _leases;
            }
            set
            {
                _leases = value;
            }
        }
        public List<Model.Dhcp.resElement> Reserv
        {
            get
            {
                return _res;
            }
            set
            {
                _res = value;
            }
        }
        public PersistanceEventArgs(List<Model.Dhcp.leaseElement> leases, List<Model.Dhcp.resElement> res)
        {
            Leases = leases;
            Reserv = res;
        }
    }
}
