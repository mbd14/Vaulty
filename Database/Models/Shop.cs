using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Vaulty.Database.Models
{
    /// <summary>
    /// Represents the Shop object
    /// 
    /// </summary>
    public class Shop
    {
        public List<Item> Items { get; set; }

        public Shop()
        {
            Items = new List<Item>();
        }

        /// <summary>
        /// Read shop data base and get all records from DB
        /// </summary>
        public void GetShop()
        {
            DbCon dbcon = new DbCon();
            string query = "SELECT * FROM SHOP_ITEMS ORDER BY ItemType ASC, Price ASC;";
            dbcon.con.Open();
            using (SqlCommand cmd = new SqlCommand(query, dbcon.con))
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Items.Add(new Item
                    {
                        Id = reader.GetInt32(0),
                        Label = reader.GetString(1),
                        Description = reader.GetString(2),
                        Price = reader.GetInt32(3),
                        ItemType = reader.GetInt32(4),
                    });
                }
            }

        }
    }
}
