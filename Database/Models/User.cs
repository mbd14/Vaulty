using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Vaulty.Database.Models
{
    /// <summary>
    /// Data Model that represent a user
    /// </summary>
    public class User
    {
        public string Id { get; set; }  // Primary key
        public int VaultCoins { get; set; }
        public int Bank { get; set; }
        public int Vaultium {  get; set; }
        public bool HasBank { get; set; }
        public User() { }

        public void InsertUser()
        {
            DbCon dbcon = new DbCon();
            using (dbcon)
            {
                string query = "INSERT INTO [USER](Id, VaultCoins, Vaultium, Bank_Amount, Has_Bank) VALUES(@Id, @VaultCoins, @Vaultium, @Bank_Amount, @Has_Bank)";
                SqlCommand command = new SqlCommand(query, dbcon.con);
                command.Parameters.AddWithValue("@Id", Id);
                command.Parameters.AddWithValue("@VaultCoins", VaultCoins);
                command.Parameters.AddWithValue("@Vaultium", Vaultium);
                command.Parameters.AddWithValue("@Bank_Amount", Bank);
                command.Parameters.AddWithValue("@Has_Bank", HasBank);

                dbcon.con.Open();
                int result = command.ExecuteNonQuery();
                if (result > 0)
                    Console.WriteLine("User inserted successfully.");
                else
                    Console.WriteLine("User insertion failed.");

                dbcon.con.Close();
            }
        }

        public void ReadUser()
        {
            DbCon dbcon = new DbCon();
            using (dbcon)
            {
                string query = "SELECT Id, VaultCoins, Vaultium, Bank_Amount, Has_Bank FROM [USER] WHERE Id=@Id";
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
                }

                dbcon.con.Close();
            }
        }

        public void ModifyUser()
        {
            DbCon dbcon = new DbCon();
            using (dbcon)
            {
                string query = "UPDATE [USER] SET VaultCoins = @VaultCoins, Bank_Amount = @Bank_Amount WHERE Id = @Id";
                SqlCommand command = new SqlCommand(query, dbcon.con);
                command.Parameters.AddWithValue("@Id", Id);
                command.Parameters.AddWithValue("@Bank_Amount", Bank);
                command.Parameters.AddWithValue("@VaultCoins", VaultCoins);
                dbcon.con.Open();
                command.ExecuteNonQuery();
                dbcon.con.Close();
            }
        }
    }
}
