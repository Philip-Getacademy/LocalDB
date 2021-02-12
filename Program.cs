using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace LocalDBExample 
{
    class Program
    {

        private static bool DELETEDB = true;

        static void Main(string[] args) {
            
            // STEP ONE :
            // Filename of the MDF DB file and Log file 
            const string Name = "DBSomething";
            var DBName = $"{Name}.mdf";
            var LogName = $"{Name}log.ldf";

            // Building the path to THIS main
            var GCD = Directory.GetCurrentDirectory();
            var Path = GCD?.Substring(0, GCD?.LastIndexOf("bin") ?? 0);

            // Contating path with file name
            var DB = Path + DBName;
            var LOG = Path + LogName;

            // Connection setup
            var S = "Data Source=(LocalDB)\\MSSQLLocalDB;Integrated Security=True;Connect Timeout=30";
            var GetConnection = new SqlConnection(S);
            var Command = new SqlCommand("", GetConnection);


            // STEP TWO :
            // Actual DB query
            Command.CommandText = DELETEDB

                ? @$" DROP DATABASE {Name} " 
                
                : @$" CREATE DATABASE {Name} ON PRIMARY
                (
                    NAME = {Name}_Data, 
                    FILENAME = '{DB}', 
                    SIZE = 2MB, MAXSIZE = 10MB, FILEGROWTH = 10%
                )

                LOG ON 
                (
                    NAME = {Name}_Log,
                    FILENAME = '{LOG}',
                    SIZE = 1MB, MAXSIZE = 5MB, FILEGROWTH = 10%
                )";


            // STEP THREE :             
            // Actual programmatic execution
            try
            {
                GetConnection.Open();
                Command.ExecuteNonQuery();

                var commandPromt = DELETEDB
                    ? "DataBase is Deleted Successfully" 
                    : "DataBase is Created Successfully";
                
                Console.WriteLine(commandPromt);
            }

            // Catch all exeptions ( Should be more specific cathces )
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
            
            // Guardclause if no expiration time is set in connection ( Only required if no timeout )
            finally { if (GetConnection.State == ConnectionState.Open) GetConnection.Close(); }
        }
    }
}
