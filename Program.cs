using System;
using System.Data.SqlClient;
using System.IO;

namespace LocalDBExample
{
    class Program
    {
        static void Main(string[] args)
        {

            // Filename of the MDF database name
            // Must be created before script is ran
            const string FileName = "DBSomething.mdf";

            var GCD = Directory.GetCurrentDirectory(); // full path
            var P = GCD?.Substring(0, GCD?.LastIndexOf("bin") ?? 0); // partitioned path
            var DB = P + FileName; // required path if the MDF file is in the same directory as main

            var S = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={DB};Integrated Security=True;Connect Timeout=30";
            var GetConnection = new SqlConnection(S);


            // Uncomment to execute below method
            // create(GetConnection);


            // Example of SQL table creation
            //GetConnection.Open();
            //var command = new SqlCommand("", GetConnection)
            //{
            //    CommandText = @"
    
            //    CREATE TABLE sometablename ( colOne varchar(50), colTwo varchar(20) )

            //"
            //};
            //command.ExecuteNonQuery(); 


        }

        private static string InsertValues(string where, string column, string values) => Insert(where, column) + Values(values);
        private static string Insert(string where, string column) => $"INSERT INTO {where} ({column}) ";
        private static string Values(string values) => $"VALUES ({values})";

        public static void create(SqlConnection connection)
        {
            
            connection.Open();                                                // Open connection to the db
            var command = new SqlCommand("", connection);              // New instance of command at connection
                                                                            
            Console.WriteLine("BD Starting...");                           // Some log
            Console.WriteLine("Executing try");                           // Some log
                                                                         
            var TableName = "someTable";                                // Name of the table
            var ColOneName = "first_name";                             // Name of the first column
            var ColTwoName = "last_name";                             // Name of the second column


            // For loop for creating 10 tables with col names but no content
            // nest another for loop to fill tables with some data
            for (var t = 0; t < 10; t++)
            {
                command.CommandText = $"CREATE TABLE {TableName}{t} ( {ColOneName} varchar(20), {ColTwoName} varchar(20) )";
                command.ExecuteNonQuery();

                // Uncomment this and comment the above to drop tables
                // DropTable(command, TableName, t);
            }

        }
        
        // Call this to drop tables that has been created, first uncomment the create table commands
        private static void DropTable(SqlCommand command, string tableName, int index)
        {

            command.CommandText = @$"DROP TABLE {tableName}{index}";
            command.ExecuteNonQuery();
        }
    }
}
