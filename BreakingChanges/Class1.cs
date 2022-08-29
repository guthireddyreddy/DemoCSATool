using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace BreakingChanges
{
    public class AppDomain1
    {
        public void CreateAppDomain()
        {
            Console.WriteLine("Creating new AppDomain.");
            AppDomain domain = AppDomain.CreateDomain("MyDomain");

            Console.WriteLine("Host domain: " + AppDomain.CurrentDomain.FriendlyName);
            Console.WriteLine("child domain: " + domain.FriendlyName);
        }
    }

    public class CAS
    {
        public void LoadAssembly()
        {
            // Load another assembly
            Assembly asb = Assembly.LoadFrom(@"D:\Temp\test\test\bin\Debug\test.exe");

            Console.WriteLine("***** Evidence Viewer *****\n");

            if (asb != null)
            {
                DisplayEvidence(asb);
            }
            Console.ReadLine();
        }

        private static void DisplayEvidence(Assembly asm)
        {
            // Get evidence collection using enumerator.
            Evidence e = asm.Evidence;
            IEnumerator obj = e.GetHostEnumerator();
            // Now print out the evidence.
            while (obj.MoveNext())
            {
                Console.WriteLine(" **** Press Enter to continue ****");
                Console.ReadLine();
                Console.WriteLine(obj.Current);
            }
        }
    }

    public class RemotingServer
    {
        public RemotingServer()
        {
            //  
            // TODO: Add constructor logic here  
            //  
        }
    }
    //Service class  
    public class RemotingService : MarshalByRefObject
    {
        public void WriteMessage(int num1, int num2)
        {
            Console.WriteLine(Math.Max(num1, num2));
        }
    }


    public class Server
    {
        public void GetServer()
        {
            HttpChannel channel = new HttpChannel(8001); //Create a new channel  
            ChannelServices.RegisterChannel(channel); //Register channel  
            RemotingConfiguration.RegisterWellKnownServiceType(typeof (RemotingService), "Service", WellKnownObjectMode.Singleton);
            Console.WriteLine("Server ON at port number:8001");
            Console.WriteLine("Please press enter to stop the server.");
            Console.ReadLine();
        }
    }
}
