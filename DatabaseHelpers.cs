using static System.Console;
using System.Text;
using System.Data.SqlClient;

namespace ToDoList
{
    public class DatabaseHelpers
    {
        private const string _connectString = "Server=localhost\\SQLEXPRESS;Database=master;Trusted_Connection=True;";

        private string _sql;
        public string sql { get => _sql; set => _sql = value; }

        public DatabaseHelpers()
        {  }

        public void dbServerConnect()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                        connection.Open();
                }
            }
            catch(SqlException exception)
            {
                WriteLine(exception.ToString());
            }
        }

        public void dbCreate()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    //Create database if it doesn't already exist
                    StringBuilder strBuilder = new StringBuilder();
                    strBuilder.Append("IF (db_id(N'TodoDB') IS NULL) ");
                    strBuilder.Append("BEGIN ");
                    strBuilder.Append("CREATE DATABASE [TodoDB] ");
                    strBuilder.Append("END;");
                    sql = strBuilder.ToString();

                    using(SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch(SqlException exception)
            {
                WriteLine(exception.ToString());
            }
        }

        public void dbTableCreate()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    //Create the ToDoList Table if it doesn't already exist
                    StringBuilder strBuilder = new StringBuilder();
                    strBuilder.Append("USE TodoDB; ");
                    strBuilder.Append("IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'TodoList')");
                    strBuilder.Append("BEGIN ");
                    strBuilder.Append("CREATE TABLE TodoList ( ");
                    strBuilder.Append(" Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY, ");
                    strBuilder.Append("ParentTask TEXT, ");
                    strBuilder.Append("SubTask1 TEXT, ");
                    strBuilder.Append("SubTask2 TEXT, ");
                    strBuilder.Append("SubTask3 TEXT, ");
                    strBuilder.Append("SubTask4 TEXT, ");
                    strBuilder.Append("SubTask5 TEXT ");
                    strBuilder.Append(");");
                    strBuilder.Append("END;");
                    sql = strBuilder.ToString();

                    using(SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch(SqlException exception)
            {
                WriteLine(exception.ToString());
            }
        }

        //Insert some test records into the table
        public void dbInsertTestRecs()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectString))
                { 
                    connection.Open();

                    StringBuilder strBuilder = new StringBuilder();
                    strBuilder.Append("USE TodoDB; ");
                    strBuilder.Append("INSERT INTO TodoList (ParentTask, SubTask1) VALUES ");
                    strBuilder.Append("(N'Task3', N'Sub3'), ");
                    strBuilder.Append("(N'Task4', N'Sub4') ");
                    sql = strBuilder.ToString();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        int rowsAffected = command.ExecuteNonQuery();
                        WriteLine("Done. Rows affected =" + rowsAffected);
                    }
                }
            }
            catch(SqlException exception)
            {
                WriteLine(exception.ToString());
            }
        }

        public void dbTableClear()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    StringBuilder strBuilder = new StringBuilder();
                    strBuilder.Append("USE TodoDB; ");
                    strBuilder.Append("DELETE FROM TodoList;");
                    sql = strBuilder.ToString();
                    connection.Open();
                    using(SqlCommand command = new SqlCommand(sql, connection))
                    {
                        int rowsAffected = command.ExecuteNonQuery();
                        WriteLine(rowsAffected + " Tasks deleted");
                    }
                }
            }
            catch(SqlException exception)
            {
                WriteLine(exception.ToString());
            }
        }

        public void dbSaveRecords(Tasklist tasks)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    StringBuilder strBuilder = new StringBuilder();
                    strBuilder.Append("USE TodoDB; ");
                    strBuilder.Append("INSERT INTO TodoList (ParentTask, SubTask1, SubTask2, SubTask3, SubTask4, SubTask5) ");
                    strBuilder.Append("VALUES (@parentTask, @subT1, @subT2, @subT3, @subT4, @subT5);");
                    sql = strBuilder.ToString();

                    //will probably need something to validate that a subtask is not null before trying to insert
                    //potentially make the index values variables as well
                        //that will allow it to loop through all tasks

                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@parentTask", tasks[0][0]);
                        command.Parameters.AddWithValue("@subT1", tasks[0][1]);
                        command.Parameters.AddWithValue("@subT2", tasks[0][2]);
                        command.Parameters.AddWithValue("@subT3", tasks[0][3]);
                        command.Parameters.AddWithValue("@subT4", tasks[0][4]);
                        command.Parameters.AddWithValue("@subT5", tasks[0][5]);

                        int rowsAffected = command.ExecuteNonQuery();
                        WriteLine(rowsAffected + " row(s) inserted");
                    }
                }
            }
            catch(SqlException exception)
            {
                WriteLine(exception.ToString());
            }
        }                
    }
}