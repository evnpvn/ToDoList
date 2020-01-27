using static System.Console;
using System.Text;
using System.Data.SqlClient;
using static ToDoList.TaskHelpers;
using System.Collections.Generic;

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
        //TODO: Delete when not needed
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

                    connection.Open();
                    
                    foreach(List<string> subtasklist in tasks)
                    {
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            //technically making this a foreach loop would prevent it from saving more than the
                            //correct number of records
                            command.Parameters.AddWithValue("@parentTask", subtasklist[0]);
                            command.Parameters.AddWithValue("@subT1", subtasklist[1]);
                            command.Parameters.AddWithValue("@subT2", subtasklist[2]);
                            command.Parameters.AddWithValue("@subT3", subtasklist[3]);
                            command.Parameters.AddWithValue("@subT4", subtasklist[4]);
                            command.Parameters.AddWithValue("@subT5", subtasklist[5]);
                            
                            int rowsAffected = command.ExecuteNonQuery();
                            WriteLine(rowsAffected + " row(s) inserted");
                        }
                    } 
                    
                }
            }
            catch(SqlException exception)
            {
                WriteLine(exception.ToString());
            }
        }

        public Tasklist dbRestoreTasks(Tasklist tasks)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    StringBuilder strBuilder = new StringBuilder();
                    strBuilder.Append("USE TodoDB; ");
                    strBuilder.Append("SELECT * FROM TodoList");
                    sql = strBuilder.ToString();
                    connection.Open();

                    using(SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while(reader.Read())
                            {
                                //FIXME: need to add validation to not try and grab null values
                                List<string> subList = new List<string>{};

                                subList.Add(reader.GetString(1));
                                subList.Add(reader.GetString(2));
                                subList.Add(reader.GetString(3));
                                subList.Add(reader.GetString(4));
                                subList.Add(reader.GetString(5));
                                subList.Add(reader.GetString(6));

                                tasks.Add(subList);
                            }
                            //Write something to the console for the number of tasks restored
                            return tasks;
                        }
                    }              
                }
            }
            catch(SqlException exception)
            {
                WriteLine(exception.ToString());
                return tasks;
            }
        }              
    }
}