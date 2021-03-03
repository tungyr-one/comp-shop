using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace comp_shop
{
    class DB     

    {

        static private string filename = "db_file.txt";

        static public void addToDB(string item_entry)
        {
            StreamWriter sw = new StreamWriter(filename, false, System.Text.Encoding.GetEncoding(1251));
            sw.Write(item_entry);
            sw.Close();
        }
    }
}
