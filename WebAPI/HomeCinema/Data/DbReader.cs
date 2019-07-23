using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace HomeCinema.Data
{
    public class DbReader
    {
        public SqlConnection Connection;

        static DbReader instance;

        public static DbReader GetInstance()
        {
            if (instance == null)
                instance = new DbReader();

            return instance;
        }

        public DbReader()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder()
            {
                DataSource = @"localhost",
                InitialCatalog = "HomeCinema",
                UserID = "sa",
                Password = "sasa"
            };

            Connection = new SqlConnection(builder.ToString());
        }

        public int RunSimpleQuery(string commandString, List<SqlParameter> parameters = null)
        {
            try
            {
                if (Connection.State != ConnectionState.Open)
                    Connection.Open();

                SqlCommand command = new SqlCommand()
                {
                    CommandText = commandString,
                    Connection = Connection
                };

                if (parameters != null)
                {
                    foreach (SqlParameter param in parameters)
                    {
                        command.Parameters.Add(param);
                    }
                }

                return command.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                return 0;
            }
        }

        public List<T> RunReader<T>(string commandString, List<SqlParameter> parameters = null) where T : DataModel, new()
        {
            SqlDataReader reader = null;

            try
            {
                if (Connection.State != ConnectionState.Open)
                    Connection.Open();

                SqlCommand command = new SqlCommand()
                {
                    CommandText = commandString,
                    Connection = Connection
                };

                if (parameters != null)
                {
                    foreach (SqlParameter param in parameters)
                    {
                        command.Parameters.Add(param);
                    }
                }

                reader = command.ExecuteReader();

                List<T> items = new List<T>();

                while (reader.Read())
                {
                    T item = new T();
                    item.FillFromReader(reader);
                    items.Add(item);
                }

                reader.Close();

                return items;
            }
            catch (Exception ex)
            {
                if (reader != null)
                    reader.Close();
                return null;
            }
        }

        public T RunSingleReader<T>(string commandString, List<SqlParameter> parameters = null)
        {
            SqlDataReader reader = null;

            try
            {
                if (Connection.State != ConnectionState.Open)
                    Connection.Open();

                SqlCommand command = new SqlCommand()
                {
                    CommandText = commandString,
                    Connection = Connection
                };

                if (parameters != null)
                {
                    foreach (SqlParameter param in parameters)
                    {
                        command.Parameters.Add(param);
                    }
                }

                reader = command.ExecuteReader();

                List<T> items = new List<T>();

                if (reader.HasRows)
                {
                    reader.Read();

                    if(reader.FieldCount == 1)
                    {
                        T value = reader.GetFieldValue<T>(0);

                        reader.Close();

                        return value;
                    }
                    else
                    {
                        reader.Close();

                        return default(T);
                    }
                }
                else
                {
                    reader.Close();

                    return default(T);
                }
            }
            catch (Exception ex)
            {
                if(reader != null)
                    reader.Close();

                return default(T);
            }
        }
    }
}
