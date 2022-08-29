using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLServerClientLibaray
{
    public class SQLClient
    {
        public List<string> GetData()
        {
            string str = "Data Source=(local);Initial Catalog=Northwind;Integrated Security=SSPI";
            return ReadOrderData(str);
        }

        public List<string> ReadOrderData(string connectionString)
        {
            List<string> result = new List<string>();

            string queryString = "SELECT OrderID, CustomerID FROM dbo.Orders;";
            using (SqlConnection connection = new SqlConnection(
                       connectionString))
            {
                SqlCommand command = new SqlCommand(
                    queryString, connection);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(String.Format("{0}, {1}",
                            reader[0], reader[1]));

                        result.Add(reader[0].ToString());
                    }
                }
            }

            return result;
        }
    }
}
