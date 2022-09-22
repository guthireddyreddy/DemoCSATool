using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using Aerospike;
using Aerospike.Client;
using MySql.Data.MySqlClient.Memcached;

namespace aerospikedataloader
{
    public class program
    {
        static void Main(string[] args)

        {
            AerospikeClient client = new AerospikeClient("127.0.0.1", 3000);
            XmlTextReader xtr = new XmlTextReader("Replace with XML File Location Path ");
            string s1 = string.Empty;
            string s2 = string.Empty;


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
            Record record = client.Get(null, key);
            client.Close();
        }

    }
}