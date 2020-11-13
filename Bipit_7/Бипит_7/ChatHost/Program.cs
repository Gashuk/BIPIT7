using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace ChatHost
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var host = new ServiceHost(typeof(Бипит_7.Service1)))
            {
                host.Open();
                Console.WriteLine("Хост начал свою  работу.");
                Console.ReadLine();
            }
        }
    }
}
