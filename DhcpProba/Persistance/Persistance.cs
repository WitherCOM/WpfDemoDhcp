using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DhcpProba.Persistance
{
    class Persistance
    {
        #region Events
        public event EventHandler<PersistanceEventArgs> OnLoad;
        #endregion

        public Persistance()
        {
            
        }

        public void Save(List<Model.Dhcp.leaseElement> lease, List<Model.Dhcp.resElement> res)
        {
            string storage = "";
            string storage_leases = "";
            foreach(Model.Dhcp.leaseElement e in lease)
            {
                storage_leases += e.MAC.ToString();
                storage_leases += ",";
                storage_leases += e.IP.GetString();
                storage_leases += ",";
                storage_leases += e.LejaratiIdo.ToString();
                storage_leases += ",";
            }
            if (storage_leases.Length != 0)
                storage_leases = storage_leases.Remove(storage_leases.Length-1);
            string storage_res = "";
            foreach (Model.Dhcp.resElement e in res)
            {
                storage_res += e.MAC.ToString();
                storage_res += ",";
                storage_res += e.IP.GetString();
                storage_res += ",";
            }
            if(storage_res.Length != 0)
                storage_res = storage_res.Remove(storage_res.Length - 1);
            storage = storage_leases + "|" + storage_res;
            StreamWriter file = new StreamWriter("dhcp.data");
            file.Write(storage);
            file.Close();
        }

        public void Load()
        {
            List<Model.Dhcp.leaseElement> lease = new List<Model.Dhcp.leaseElement>();
            List<Model.Dhcp.resElement> res = new List<Model.Dhcp.resElement>();
            string storage = "";
            StreamReader file = new StreamReader("dhcp.data");
            storage = file.ReadToEnd();
            file.Close();
            String[] lists = storage.Split('|');

            String[] leaselist = lists[0].Split(',');
            for(int i = 0;i < leaselist.Length/3;++i)
            {
                lease.Add(new Model.Dhcp.leaseElement(Int64.Parse(leaselist[i*3]), new Model.Dhcp.Ip(leaselist[i*3 + 1]), Int32.Parse(leaselist[i*3 + 2])));
            }

            String[] reslist = lists[1].Split(',');
            for (int i = 0; i < reslist.Length / 2; ++i)
            {
                res.Add(new Model.Dhcp.resElement(Int64.Parse(leaselist[i*2]), new Model.Dhcp.Ip(leaselist[i*2 + 1])));
            }
            OnLoad?.Invoke(this, new PersistanceEventArgs(lease, res));
        }
    }
}
