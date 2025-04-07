using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Vaulty.Database.Models
{
    /// <summary>
    /// Data Model used to represent a row in the COMMAND_EXECUTION table from the SQL DB.
    /// Contains methods to manipulate individual rows of the represented table.
    /// </summary>
    public class CommandExecutions
    {
        public string Id { get; set; }
        public string LastDaily {  get; set; }
        public string LastWeekly { get; set; }
        public string LastWork { get; set; }


        public CommandExecutions() { }


        public void GetExecution()
        {
            DbCon dbCon = new DbCon();
            using (dbCon)
            {
                dbCon.con.Open();
                string query = "SELECT * FROM COMMAND_EXECUTION WHERE UserId=@UserId";
                SqlCommand command = new SqlCommand(query, dbCon.con);
                command.Parameters.AddWithValue("@UserId", Id);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    Id = Id;
                    LastDaily = reader.GetString(reader.GetOrdinal("LastExecDaily"));
                    LastWeekly = reader.GetString(reader.GetOrdinal("LastExecWeekly"));
                    LastWork = reader.GetString(reader.GetOrdinal("LastExecWork"));

                }
                else
                {
                    dbCon.con.Close();
                    InsertCommandExecution();
                    return;
                }
                    dbCon.con.Close();
            }
        }

        public void ModifyExecution()
        {
            DbCon dbCon = new DbCon();
            using (dbCon)
            {
                dbCon.con.Open();

                // Construct the SQL query to update the command execution times
                string query = "UPDATE COMMAND_EXECUTION " +
                               "SET LastExecDaily = @LastExecDaily, LastExecWeekly = @LastExecWeekly, LastExecWork = @LastExecWork " +
                               "WHERE UserId = @UserId";

                SqlCommand command = new SqlCommand(query, dbCon.con);

                // Add parameters to the SQL query to prevent SQL injection
                command.Parameters.AddWithValue("@LastExecDaily", LastDaily);
                command.Parameters.AddWithValue("@LastExecWeekly", LastWeekly);
                command.Parameters.AddWithValue("@LastExecWork", LastWork);
                command.Parameters.AddWithValue("@UserId", Id);

                // Execute the query
                command.ExecuteNonQuery();

                dbCon.con.Close();
            }
        }

        public void InsertCommandExecution()
        {
            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            using (DbCon dbCon = new DbCon())
            {
                dbCon.con.Open();

                string query = "INSERT INTO COMMAND_EXECUTION (UserId, LastExecDaily, LastExecWeekly, LastExecWork) " +
                               "VALUES (@UserId, @LastExecDaily, @LastExecWeekly, @LastExecWork)";

                SqlCommand command = new SqlCommand(query, dbCon.con);
                command.Parameters.AddWithValue("@UserId", Id);
                command.Parameters.AddWithValue("@LastExecDaily", epoch);  // Default to current time
                command.Parameters.AddWithValue("@LastExecWeekly", epoch);  // Default to current time
                command.Parameters.AddWithValue("@LastExecWork", epoch);  // Default to current time

                command.ExecuteNonQuery();

                LastDaily = epoch.ToString();
                LastWeekly = epoch.ToString();
                LastWork = epoch.ToString();
            }
        }

    }
}
