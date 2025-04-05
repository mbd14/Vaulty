using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Vaulty.Database.Models
{
    public class User
    {
        public string Id { get; set; }  // Primary key
        public int Coins { get; set; }

        public User() { }

        public static void InsertUser(User u, DbCon dbcon)
        {

            using (dbcon)
            {
                string query = "INSERT INTO [USER](Id, Coins) VALUES(@Id, @Coins)";
                SqlCommand command = new SqlCommand(query, dbcon.con);
                command.Parameters.AddWithValue("@Id", u.Id);
                command.Parameters.AddWithValue("@Coins", u.Coins);

                dbcon.con.Open();
                int result = command.ExecuteNonQuery();
                if (result > 0)
                    Console.WriteLine("User inserted successfully.");
                else
                    Console.WriteLine("User insertion failed.");
            }
        }

        public void ReadUser(DbCon dbcon)
        {
            using (dbcon)
            {
                string query = "SELECT Id, Coins FROM [USER] WHERE Id=@Id";
                SqlCommand command = new SqlCommand(query, dbcon.con);
                command.Parameters.AddWithValue("@Id", 460806719682117632);

                dbcon.con.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    Id = reader.GetString(reader.GetOrdinal("Id"));
                    Coins = reader.GetInt32(reader.GetOrdinal("Coins"));
                }
            }
        }
    }
}
