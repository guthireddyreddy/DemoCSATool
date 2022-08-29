using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using DBClientLibrary;
using AerospikeClientLibrary;
using SQLServerClientLibaray;
using SolaceVMRClientLibrary;
using RabbitMQClientLibrary;
using BreakingChanges;

namespace DemoWindowsService
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();

            InitializeData();
        }

        private void InitializeData()
        {
            SQLClient sqlClient = new SQLClient();
            sqlClient.GetData();
            sqlClient.ReadOrderData("");

            DB2Client dbClient = new DB2Client();
            dbClient.GetData();

            AerosClient aerosClient = new AerosClient();
            aerosClient.GetData();

            RabbitMQClient rabbitMQClient = new RabbitMQClient();
            rabbitMQClient.RabbitMQConsumer();

            M2MqttClient m2MqttClient = new M2MqttClient();
            m2MqttClient.MqttConnect("127.0.0.1", "clientid");

            AppDomain1 appDomain = new AppDomain1();
            appDomain.CreateAppDomain();

            CAS cas = new CAS();
            cas.LoadAssembly();

            RemotingService remotingServer = new RemotingService();
            remotingServer.WriteMessage(1, 3);

            Server server = new Server();
            server.GetServer();

            BreakingChangesAppSecurity.AppSecurity appSecurity = new BreakingChangesAppSecurity.AppSecurity();
            Console.WriteLine(appSecurity.UserName + "-" + appSecurity.Password);
            
        }

        protected override void OnStart(string[] args)
        {
            Console.WriteLine("Service OnStart");
        }

        protected override void OnStop()
        {
            Console.WriteLine("Service OnStop");
        }
    }
}
