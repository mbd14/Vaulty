using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Vaulty.Database.Models
{
    /// <summary>
    /// Data Model that represent a user in the USER table
    /// Contains methods to manipulate individual rows of the represented table or the whole table.
    /// DO NOT EDIT THIS CODE IF YOU DONT KNOW WHAT YOU ARE DOING
    /// </summary>
    public class User
    {
        public string Id { get; set; }  // Primary key
        public int VaultCoins { get; set; }
        public int Bank { get; set; }
        public int Vaultium {  get; set; }
        public bool HasBank { get; set; }
        public int Job { get; set; }
        public User() { }

        /// <summary>
        /// Insert a user in the data base
        /// </summary>
        public void InsertUser()
        {
            DbCon dbcon = new DbCon();
            using (dbcon)
            {
                string query = "INSERT INTO [USER](Id, VaultCoins, Vaultium, Bank_Amount, Has_Bank, Job) VALUES(@Id, @VaultCoins, @Vaultium, @Bank_Amount, @Has_Bank, @Job)";
                SqlCommand command = new SqlCommand(query, dbcon.con);
                command.Parameters.AddWithValue("@Id", Id);
                command.Parameters.AddWithValue("@VaultCoins", VaultCoins);
                command.Parameters.AddWithValue("@Vaultium", Vaultium);
                command.Parameters.AddWithValue("@Bank_Amount", Bank);
                command.Parameters.AddWithValue("@Has_Bank", HasBank);
                command.Parameters.AddWithValue("@Job", Job);

                dbcon.con.Open();
                int result = command.ExecuteNonQuery();
                if (result > 0)
                    Console.WriteLine("User inserted successfully.");
                else
                    Console.WriteLine("User insertion failed.");

                dbcon.con.Close();
            }
        }

        /// <summary>
        /// Fetch user from the data base
        /// </summary>
        public void ReadUser()
        {
            DbCon dbcon = new DbCon();
            using (dbcon)
            {
                string query = "SELECT * FROM [USER] WHERE Id=@Id";
                SqlCommand command = new SqlCommand(query, dbcon.con);
                command.Parameters.AddWithValue("@Id", Id);

                dbcon.con.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    Id = reader.GetString(reader.GetOrdinal("Id"));
                    VaultCoins = reader.GetInt32(reader.GetOrdinal("VaultCoins"));
                    Vaultium = reader.GetInt32(reader.GetOrdinal("Vaultium"));
                    Bank = reader.GetInt32(reader.GetOrdinal("Bank_Amount"));
                    HasBank = reader.GetBoolean(reader.GetOrdinal("Has_Bank"));
                    Job = reader.GetInt32(reader.GetOrdinal("Job"));
                }
                else
                {
                    dbcon.con.Close();
                    InsertUser();
                    return;
                }

                dbcon.con.Close();
            }
        }

        /// <summary>
        /// Update user in the data base
        /// </summary>
        public void ModifyUser()
        {
            DbCon dbcon = new DbCon();
            using (dbcon)
            {
                string query = "UPDATE [USER] SET VaultCoins = @VaultCoins, Vaultium=@Vaultium, Bank_Amount = @Bank_Amount, Job = @Job WHERE Id = @Id";
                SqlCommand command = new SqlCommand(query, dbcon.con);
                command.Parameters.AddWithValue("@Id", Id);
                command.Parameters.AddWithValue("@Bank_Amount", Bank);
                command.Parameters.AddWithValue("@VaultCoins", VaultCoins);
                command.Parameters.AddWithValue("@Vaultium", Vaultium);
                command.Parameters.AddWithValue("@Job", Job);
                dbcon.con.Open();
                command.ExecuteNonQuery();
                dbcon.con.Close();
            }
        }

        public static List<User> GetTopUsers()
        {
            List<User> topUsers = new List<User>();
            DbCon dbcon = new DbCon();

            using (dbcon)
            {
                string query = "SELECT TOP 10 * FROM [USER] ORDER BY VaultCoins DESC";
                SqlCommand command = new SqlCommand(query, dbcon.con);

                dbcon.con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    User u = new User
                    {
                        Id = reader.GetString(reader.GetOrdinal("Id")),
                        VaultCoins = reader.GetInt32(reader.GetOrdinal("VaultCoins")),
                        Vaultium = reader.GetInt32(reader.GetOrdinal("Vaultium")),
                        Bank = reader.GetInt32(reader.GetOrdinal("Bank_Amount")),
                        HasBank = reader.GetBoolean(reader.GetOrdinal("Has_Bank")),
                        Job = reader.GetInt32(reader.GetOrdinal("Job")),
                    };

                    topUsers.Add(u);
                }

                dbcon.con.Close();
            }

            return topUsers;
        }
    }
}
