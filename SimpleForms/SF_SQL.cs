using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace SimpleForms
{
    class SF_SQL
    {
        // Defining private/public properties.
        public string connString;

        public bool connected = false;

        public SqlConnection connection;

        public SqlDataAdapter adapter;

        public SqlCommand command;

        public SqlCommandBuilder commandBuilder;

        public SF_SQL(string _connString)
        {
            connString = _connString;
            connection = new SqlConnection(connString);
        }

        //Connection/disconnection wrappers.
        public void Connect()
        {
            connection.Open();
            connected = true;
        }

        public void Disconnect()
        {
            connection.Close();
            connected = true;
        }

        //Pushes through a select command.
        public DataSet Select(string s_command)
        {
            checkConnected();
            adapter = new SqlDataAdapter(s_command, connection);
            DataSet dataSet = new DataSet();
            try
            {
                adapter.Fill(dataSet);
                return dataSet;
            }
            catch
            {
                return new DataSet();
            }
        }

        //Pushes through a non-query command.
        public void NonQuery(string n_command)
        {
            checkConnected();
            command = new SqlCommand(n_command);
            command.Connection = connection;
            command.ExecuteNonQuery();
        }

        //Checks whether the database is currently connected.
        private void checkConnected()
        {
            if (!connected)
            {
                Connect();
            }
        }
    }

}
}
