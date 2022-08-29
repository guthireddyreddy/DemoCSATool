using Aerospike.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AerospikeClientLibrary
{
    public class AerosClient
    {
        public List<string> GetData()
        {
            List<string> result = new List<string>(); 
            // Establish connection the server
            AerospikeClient client = new AerospikeClient("127.0.0.1", 3000);

            // Create key
            Key key = new Key("test", "myset", "mykey");

            // Create Bins
            Bin bin1 = new Bin("name", "John");
            Bin bin2 = new Bin("age", 25);

            // Write record
            client.Put(null, key, bin1, bin2);

            // Read record
            Record record = client.Get(null, key);

            // Close connection
            client.Close();

            if (record != null)
            {
                foreach (KeyValuePair<string, object> entry in record.bins)
                {
                    Console.WriteLine("Name=" + entry.Key + " Value=" + entry.Value);
                    result.Add("Name=" + entry.Key + " Value=" + entry.Value);
                }
            }

            return result;
        }
    }
}
