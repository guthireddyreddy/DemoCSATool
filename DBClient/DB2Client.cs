using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBClientLibrary
{
    public class DB2Client
    {
        public List<string> GetData()
        {
            List<string> result = new List<string>(); 
            OdbcCommand command = null;
            // Use the dsn alias that you defined in db2dsdriver.cfg with the db2cli writecfg command in step 1.
            String connectionString = "database=alias;uid=userid;pwd=password;";
            OdbcConnection connection = new OdbcConnection(connectionString);
            connection.Open();
            command = (OdbcCommand)connection.CreateCommand();
            command.CommandText = "SELECT branch_code, city from GOSALES.BRANCH";
            Console.WriteLine(command.CommandText);

            DbDataReader dataReader = null;
            dataReader = command.ExecuteReader();
            Console.WriteLine("BRANCH\tCITY");
            Console.WriteLine("============================");
            while (dataReader.Read())
            {
                for (int i = 0; i <= 1; i++)
                {
                    try
                    {
                        if (dataReader.IsDBNull(i))
                        {
                            Console.Write("NULL");
                        }
                        else
                        {
                            Console.Write(dataReader.GetString(i));
                            result.Add(dataReader.GetString(i));
                        }
                    }
                    catch (Exception e)
                    {
                        Console.Write(e.ToString());
                    }
                    Console.Write("\t");

                }
                Console.WriteLine("");
            }
            dataReader.Close();
            command.Dispose();
            connection.Close();

            return result;
        }
    }
}
