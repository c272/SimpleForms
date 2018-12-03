using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleForms
{
    public class SFDB
    {
        //Creating new public properties.
        public List<string> tables = new List<string>();
        public string fileLoc;

        //Constructor for database.
        public SFDB(string location)
        {
            //Checking if database already exists at this location.
            if (!File.Exists(location+"\\sfdb.txt"))
            {
                //No database exists here, create new blank files.
                File.Create(location + "\\sfdb.txt");
                Directory.CreateDirectory(location + "\\sfdb");
                fileLoc = location;

                //Load blanks into strings.
                tables = new List<string>();
            } else {
                //Database exists, load table file.
                StreamReader sr = new StreamReader(location + "\\sfdb.txt");
                fileLoc = location;
                while (!sr.EndOfStream)
                {
                    tables.Add(sr.ReadLine());
                }
                sr.Close();
            }
        }

        //Methods to create a new table and add a dictionary.
        public void CreateTable(string tableName)
        {
            //Adding to tables.
            tables.Add(tableName);
            //Saving tables to table file.
            StreamWriter sw = new StreamWriter(fileLoc + "\\sfdb.txt");
            foreach (var i in tables)
            {
                sw.WriteLine(i);
            }
            sw.Close();

            //Creating file in sfdb directory.
            File.Create(fileLoc + "\\sfdb\\" + tableName+".txt");
        }

        public void UpdateTable(string table, Dictionary<string, string> d)
        {
            //Saving keys and values.
            StreamWriter sw = new StreamWriter(fileLoc + "\\sfdb\\" + table + ".txt");
            foreach (var i in d)
            {
                //Writing key and keyvalue pair separated by |.
                sw.WriteLine(i.Key + "|" + i.Value);
            }
            sw.Close();
        }

        //Method to get table.
        public Dictionary<string,string> GetTable(string table)
        {
            //Checking if table exists.
            if (!tables.Contains(table))
            {
                throw new Exception("Table does not exist in database.");
            }

            //Loading file.
            StreamReader sr = new StreamReader(fileLoc+"\\sfdb\\"+table+".txt");
            List<string> keyPair = new List<string>();
            while (!sr.EndOfStream)
            {
                keyPair.Add(sr.ReadLine());
            }
            sr.Close();

            //Splitting values out of keypairs.
            Dictionary<string, string> returnDict = new Dictionary<string, string>();
            foreach(var i in keyPair)
            {
                string[] split = i.Split('|');
                returnDict.Add(split[0], split[1]);
            }

            //Returning.
            return returnDict;
        }
    }
}
