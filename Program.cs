﻿using Cars.Model;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

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
            Database = "auto";
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
            MySqlCommand cmd = new MySqlCommand(sql, conn.Connection);
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
        public static void addNewCar()
        {
            conn.Connection.Open();

            string brand, type, license;
            int date;

            Console.Write("Kérem az autó márkáját: ");
            brand = Console.ReadLine();

            Console.Write("Kérem az autó típusát: ");
            type = Console.ReadLine();

            Console.Write("Kérem az autó motorszámát: ");
            license = Console.ReadLine();

            Console.Write("Kérem az autó gyártási évét: ");
            date = Convert.ToInt32(Console.ReadLine());
            string sql = $"INSERT INTO `cars`(`Brand`, `Type`, `License`, `Date`) VALUES('{brand}', '{type}', '{license}',{date})";
            MySqlCommand cmd = new MySqlCommand(sql, conn.Connection);
            cmd.ExecuteNonQuery();


            conn.Connection.Close();
        }
        public static void updateCar() 
        {
            conn.Connection.Open();

            int date, id;

            Console.Write("Kérem az autó azonosítóját: ");
            id = Convert.ToInt32(Console.ReadLine());

            Console.Write("Kérem az autó gyártási évét: ");
            date = Convert.ToInt32(Console.ReadLine());

            string sql = $"UPDATE `cars` SET `Date`='{date}' WHERE Id={id}";
            MySqlCommand cmd = new MySqlCommand(sql, conn.Connection);
            cmd.ExecuteNonQuery();


            conn.Connection.Close();
        }
        public static void deleteCar()
        {
            conn.Connection.Open();

            int id;

            Console.Write("Kérem az autó azonosítóját: ");
            id = Convert.ToInt32(Console.ReadLine());

            string sql = $"DELETE FROM `cars` WHERE `Id={id}`";
            MySqlCommand cmd = new MySqlCommand(sql, conn.Connection);
            cmd.ExecuteNonQuery();

            conn.Connection.Close();
        }
        public static void CarById() 
        {
            conn.Connection.Open();

            Console.Write("Kérem az autó azonosítóját: ");
            int id = int.Parse(Console.ReadLine());
            string sql = $"SELECT * FROM `cars` WHERE `Id` = {id}";
            MySqlCommand cmd = new MySqlCommand(sql, conn.Connection);
            MySqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            Console.WriteLine($"\nMárka: {reader.GetString(1)}\nTípus: {reader.GetString(2)}\nMotorszám: {reader.GetString(3)}\nGyártási év: {reader.GetInt32(4)}");

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
