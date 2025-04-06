using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Vaulty.Database.Models
{
    public class Shop
    {
        public List<Item> Items { get; set; }

        public Shop()
        {
            Items = new List<Item>();
        }


        public void GetShop()
        {
            DbCon dbcon = new DbCon();
            string query = "SELECT Label, Description, Price FROM SHOP_ITEMS";
            dbcon.con.Open();
            using (SqlCommand cmd = new SqlCommand(query, dbcon.con))
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Items.Add(new Item
                    {
                        Label = reader.GetString(0),
                        Description = reader.GetString(1),
                        Price = reader.GetInt32(2)
                    });
                }
            }

        }
    }
}
