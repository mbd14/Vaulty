using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Vaulty.Database.Models
{


    public class Inventory
    {

        public List<Inventoryitem> inventoryitems;
        public Inventory() { inventoryitems = new List<Inventoryitem>(); }


        public void GetUserInventory(string id)
        {
            DbCon dbcon = new DbCon();
            string query = "SELECT * FROM INVENTORY WHERE UserId=@Id";
            dbcon.con.Open();
            SqlCommand cmd = new SqlCommand(query, dbcon.con);
            cmd.Parameters.AddWithValue("@Id", id);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    inventoryitems.Add(new Inventoryitem
                    {
                        UserId = reader.GetString(0),
                        ItemId = reader.GetInt32(1),
                        ResellPrice = reader.GetInt32(2),
                        Quantity= reader.GetInt32(3),
                        ItemLabel = reader.GetString(4)
                        
                    });
                }
            }

        }

        public void AddItemToInventory(string userid, Item i)
        {
            DbCon dbcon = new DbCon();
            const string query = @"
            INSERT INTO INVENTORY (UserId, ItemId, ResellPrice, Quantity, ItemLabel)
            VALUES (@UserId, @ItemId, @ResellPrice, @Quantity, @ItemLabel)";

            using SqlCommand command = new SqlCommand(query, dbcon.con);

            command.Parameters.AddWithValue("@UserId", userid);
            command.Parameters.AddWithValue("@ItemId", i.Id);
            command.Parameters.AddWithValue("@ResellPrice", i.Price);
            command.Parameters.AddWithValue("@Quantity", 1);
            command.Parameters.AddWithValue("@ItemLabel", i.Label);

            dbcon.con.Open();
            int result = command.ExecuteNonQuery();
            if (result > 0)
                Console.WriteLine("User inserted successfully.");
            else
                Console.WriteLine("User insertion failed.");

        }

        public void AddItemToInventoryIncrement(string userid, Item i)
        {
            DbCon dbcon = new DbCon();
            const string query = @"
            UPDATE INVENTORY
            SET Quantity = Quantity + 1
            WHERE UserId = @UserId AND ItemId = @ItemId";

            using SqlCommand command = new SqlCommand(query, dbcon.con);
            command.Parameters.Add("@UserId", SqlDbType.VarChar, 50).Value = userid;
            command.Parameters.Add("@ItemId", SqlDbType.Int).Value = i.Id;

            dbcon.con.Open();
            command.ExecuteNonQuery();
        }

        public class Inventoryitem()
        {
            public string UserId { get; set; }
            public int ItemId { get; set; }
            public int ResellPrice { get; set; }
            public int Quantity { get; set; }
            public string ItemLabel { get; set; }
        }
    }
}
