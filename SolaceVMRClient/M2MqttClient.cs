using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace SolaceVMRClientLibrary
{
    public class M2MqttClient
    {
        public bool MqttConnect(string serveur, string clientid, string username = null, string password = null, bool willRetain = false, byte willQosLevel = 1, bool willFlag = false, string willTopic = null, string willMessage = null, bool cleanSession = true, ushort keepAlivePeriod = 60)
        {
            // create client instance 
            var client = new MqttClient(serveur);
            client.MqttMsgSubscribed += Client_MqttMsgSubscribed;
            client.MqttMsgUnsubscribed += Client_MqttMsgUnsubscribed;
            // register to message received 
            client.MqttMsgPublishReceived += Client_MqttMsgPublishReceived;
            //string clientId = Guid.NewGuid().ToString();
            if (username != null)
            {
                if (willFlag)
                    client.Connect(clientid, username, password, willRetain, willQosLevel, willFlag, willTopic, willMessage, cleanSession, keepAlivePeriod);
                else
                    client.Connect(clientid, username, password);
            }
            else
                client.Connect(clientid);
            //client.Unsubscribe(new string[] { "/#" });


            return client.IsConnected;
        }

        private void Client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            
        }

        private void Client_MqttMsgUnsubscribed(object sender, MqttMsgUnsubscribedEventArgs e)
        {
            
        }

        private void Client_MqttMsgSubscribed(object sender, MqttMsgSubscribedEventArgs e)
        {
            
        }
    }
}
