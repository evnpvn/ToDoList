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
        //used for testing
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
                    strBuilder.Append("VALUES (@subT0, @subT1, @subT2, @subT3, @subT4, @subT5);");
                    sql = strBuilder.ToString();
                    
                    string sqlParam = "";
                    string paramprefix = "@subT";
                    connection.Open();  

                    foreach(List<string> subtasklist in tasks)
                    {
                        int sublistIndex = 0;
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            for(int i = 0; i < 7; i++)
                            {
                                sqlParam = string.Concat(paramprefix, sublistIndex);

                                if(subtasklist.Count > sublistIndex && subtasklist[sublistIndex] != null 
                                                                    && subtasklist[sublistIndex] != "")
                                {
                                    command.Parameters.AddWithValue(sqlParam, subtasklist[sublistIndex]);
                                }
                                else
                                {
                                    command.Parameters.AddWithValue(sqlParam, System.Data.SqlTypes.SqlString.Null);
                                }
                                sublistIndex++;
                            }

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
                                List<string> subList = new List<string>{};

                                if(!reader.IsDBNull(1))
                                {   subList.Add(reader.GetString(1));   }
                                
                                if(!reader.IsDBNull(2))
                                {   subList.Add(reader.GetString(2));   }
                                
                                if(!reader.IsDBNull(3))
                                {   subList.Add(reader.GetString(3));   }

                                if(!reader.IsDBNull(4))
                                {   subList.Add(reader.GetString(4));   }

                                if(!reader.IsDBNull(5))
                                {   subList.Add(reader.GetString(5));   }

                                if(!reader.IsDBNull(6))
                                {   subList.Add(reader.GetString(6));   }

                                tasks.Add(subList);
                            }
                            tasks.PrintAllTasks();
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