using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Vaulty.Database.Models;

namespace Vaulty.Database
{
    /// <summary>
    /// Represent a connection in the SQL SERVER database.
    /// Since localhost is used, the auth string can be visible.
    /// </summary>
    public class DbCon : DbContext
    {
        public readonly string connectionString = "Server=sqlserver,1433;Database=master;User Id=sa;Password=password1*;TrustServerCertificate=True;";
        public SqlConnection con;

        public DbCon()
        {
            con = new SqlConnection(connectionString);
        }
    }

}
