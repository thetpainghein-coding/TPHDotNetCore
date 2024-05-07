using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TPHDotNetCore.Shared
{
    public class AdoDotNetService
    {

        private readonly string _connectionString;

        public AdoDotNetService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<T> Query<T> (string query, params AdoDotNetParameter[]? parameters)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open ();

            SqlCommand cmd = new SqlCommand (query, connection);

            if (parameters is not null && parameters.Length > 0) 
            {

                //foreach (var item in parameters)
                //{
                //    cmd.Parameters.AddWithValue(item.Name, item.Value);

                //}

                var parameterArray = parameters.Select(item => new SqlParameter(item.Name, item.Value)).ToArray();// same as above foreach 
                cmd.Parameters.AddRange (parameterArray);

                //cmd.Parameters.AddRange(parameters.Select(item => new SqlParameter(item.Name, item.Value)).ToArray()); // combination of above 2 lines
            }



            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);

            connection.Close ();

            string json = JsonConvert.SerializeObject(dt);
            List<T> lst = JsonConvert.DeserializeObject<List<T>>(json)!;

            return lst;
        }

        public T QueryFirstOrDefault<T>(string query, params AdoDotNetParameter[]? parameters)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);

            if (parameters is not null && parameters.Length > 0)
            {

                //foreach (var item in parameters)
                //{
                //    cmd.Parameters.AddWithValue(item.Name, item.Value);

                //}

                var parameterArray = parameters.Select(item => new SqlParameter(item.Name, item.Value)).ToArray();// same as above foreach 
                cmd.Parameters.AddRange(parameterArray);

                //cmd.Parameters.AddRange(parameters.Select(item => new SqlParameter(item.Name, item.Value)).ToArray()); // combination of above 2 lines
            }



            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);

            connection.Close();

            string json = JsonConvert.SerializeObject(dt);
            List<T> lst = JsonConvert.DeserializeObject<List<T>>(json)!;

            return lst[0];
        }

        public  int Execute(string query, params AdoDotNetParameter[]? parameters)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);

            if (parameters is not null && parameters.Length > 0)
            {

                //foreach (var item in parameters)
                //{
                //    cmd.Parameters.AddWithValue(item.Name, item.Value);

                //}

                var parameterArray = parameters.Select(item => new SqlParameter(item.Name, item.Value)).ToArray();// same as above foreach 
                cmd.Parameters.AddRange(parameterArray);

                //cmd.Parameters.AddRange(parameters.Select(item => new SqlParameter(item.Name, item.Value)).ToArray()); // combination of above 2 lines
            }

            var result = cmd.ExecuteNonQuery();

            connection.Close();
            return result;
        }
    }


    public class AdoDotNetParameter
    {

        public AdoDotNetParameter() { }

        public AdoDotNetParameter(string name, object value)
        {
            Name = name;
            Value = value;
        }
        public string Name { get; set; } // look at AddWithValue
        public object Value { get; set; } // look at AddWithValue
    }
}  




     
