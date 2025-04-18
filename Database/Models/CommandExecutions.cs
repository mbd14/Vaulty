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
        public int DailyStreak { get; set; }


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
                    Id = reader.GetString(reader.GetOrdinal("UserId"));
                    LastDaily = reader.GetString(reader.GetOrdinal("LastExecDaily"));
                    LastWeekly = reader.GetString(reader.GetOrdinal("LastExecWeekly"));
                    LastWork = reader.GetString(reader.GetOrdinal("LastExecWork"));
                    DailyStreak = reader.GetInt32(reader.GetOrdinal("DailyStreak"));
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
                               "SET LastExecDaily = @LastExecDaily, LastExecWeekly = @LastExecWeekly, LastExecWork = @LastExecWork, DailyStreak = @DailyStreak " +
                               "WHERE UserId = @UserId";

                SqlCommand command = new SqlCommand(query, dbCon.con);

                // Add parameters to the SQL query to prevent SQL injection
                command.Parameters.AddWithValue("@LastExecDaily", LastDaily);
                command.Parameters.AddWithValue("@LastExecWeekly", LastWeekly);
                command.Parameters.AddWithValue("@LastExecWork", LastWork);
                command.Parameters.AddWithValue("@UserId", Id);
                command.Parameters.AddWithValue("@DailyStreak", DailyStreak);

                // Execute the query
                command.ExecuteNonQuery();

                dbCon.con.Close();
            }
        }

        public void InsertCommandExecution()
        {
            using (DbCon dbCon = new DbCon())
            {
                dbCon.con.Open();

                string query = "INSERT INTO COMMAND_EXECUTION (UserId, LastExecDaily, LastExecWeekly, LastExecWork, DailyStreak) " +
                               "VALUES (@UserId, @LastExecDaily, @LastExecWeekly, @LastExecWork, @DailyStreak)";

                SqlCommand command = new SqlCommand(query, dbCon.con);
                command.Parameters.AddWithValue("@UserId", Id);
                command.Parameters.AddWithValue("@LastExecDaily", "0");  // Default to current time
                command.Parameters.AddWithValue("@LastExecWeekly", "0");  // Default to current time
                command.Parameters.AddWithValue("@LastExecWork", "0");  // Default to current time
                command.Parameters.AddWithValue("@DailyStreak", 0);  // Default streak

                command.ExecuteNonQuery();

                LastDaily = "0";
                LastWeekly = "0";
                LastWork = "0";
                DailyStreak = 0;
            }
        }

    }
}
