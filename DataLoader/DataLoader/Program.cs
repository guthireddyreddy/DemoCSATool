using System;
using System.Xml;
using Aerospike;
using Aerospike.Client;
using System.Collections.Generic;

namespace DataLoader
{
    class Program
    {


        static void Main(string[] args)

        {
            AerospikeClient client = new AerospikeClient("127.0.0.1", 3000);
            XmlTextReader xtr = new XmlTextReader("C:\\Users\\hemavenkata.hshah\\Desktop\\datareader.xml");
            string s1 = string.Empty;
            string s2= string.Empty;

           
            while (xtr.Read())
            {
                if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "name")
                {
                     s1 = xtr.ReadElementContentAsString();
                    
                }
                if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "class")
                {
                    s2 = xtr.ReadElementContentAsString();
                    

                }
            }
            Key key = new Key("test", "myset", "mykey");
            Bin bin1 = new Bin("name", s1);
            Bin bin2 = new Bin("name", s2);
            client.Put(null, key, bin1, bin2);
            Record record = client.Get(null, key);
            client.Close();


        }
    }
}

   

