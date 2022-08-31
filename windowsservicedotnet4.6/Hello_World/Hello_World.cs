using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Hello_World
{
    public partial class Hello_World : ServiceBase
    {
        public Hello_World()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            using (StreamWriter writeText = new StreamWriter("c://Temp//output.txt"))
            {
                writeText.WriteLine("Hello World...");
            }
        }

        protected override void OnStop()
        {

        }
    }
}
