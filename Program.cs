using Cars.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars
{
    public class connect
    {
        public MySqlConnection Connection;
        private string Host;
        private string Database;
        private string Username;
        private string Password;
        private string ConnectionString;

        public connect()
        {
            Host = "localhost";
            Database = "cars";
            Username = "root";
            Password = "";
            
            ConnectionString = "SERVER=" + Host + ";DATABASE=" + Database + ";UID=" + Username + ";PASSWORD=" + Password + ";SslMode=None";

            Connection = new MySqlConnection(ConnectionString);
        }

    }
    
    public class Program
    {
        public static connect conn = new connect();
        public static List<Car> cars = new List<Car>();
        static void feltolt()
        {
            conn.Connection.Open();
            string sql = "SELECT * FROM `cars`";
            MySqlCommand cmd = new MySqlCommand(sql,conn.Connection);
            MySqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            do
            {
                Car car = new Car();
                car.ID = reader.GetInt32(0);
                car.Brand = reader.GetString(1);
                car.Type = reader.GetString(2);
                car.License = reader.GetString(3);
                car.Date = reader.GetInt32(4);
                cars.Add(car);
            } while (reader.Read());
            

            conn.Connection.Close();
        }
        static void Main(string[] args)
        {
            feltolt();
            foreach (var item in cars)
            {
                Console.WriteLine($"Autó gyártója: {item.Brand}, motorszáma: {item.License}");

            }
        }
    }
}
