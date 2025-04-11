using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vaulty.Database.Models
{
    /// <summary>
    /// Data Model used to represent a row in the SHOP_ITEMS table from the SQL DB.
    /// </summary>
    public class Item
    {
        public int Id { get; set; }
        public string Label { get; set; }         // Unique identifier or name
        public string Description { get; set; }   // Description of the item
        public int Price { get; set; }        // Price of the item
        public int ItemType { get; set; }

    }
}
