using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using wfc_chat;

namespace ChatHost
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(ServiceChat)))
            {
                host.Open();
                Console.WriteLine("Хост запущен.");
                Console.ReadLine();
            }
        }
    }
}
