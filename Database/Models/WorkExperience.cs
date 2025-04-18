using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Vaulty.Database.Models
{
    public class WorkExperience
    {
        public string Id { get; set; }
        public int WorkXp { get; set; } = 0;
        public int WorkLevel { get; set; } = 1;
        public int XpUntilNextLevel { get; set; } = 50;

        public WorkExperience(string id)
        {
            Id = id;
        }

        // Insert user into WorkExperience table
        public void InsertWorkExperience()
        {
            using (DbCon dbcon = new DbCon())
            {
                string query = "INSERT INTO WorkExperience (Id, WorkXp, WorkLevel, XpUntilNextLevel) VALUES (@Id, @Xp, @Level, @XpNext)";
                SqlCommand cmd = new SqlCommand(query, dbcon.con);
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@Xp", WorkXp);
                cmd.Parameters.AddWithValue("@Level", WorkLevel);
                cmd.Parameters.AddWithValue("@XpNext", XpUntilNextLevel);

                dbcon.con.Open();
                cmd.ExecuteNonQuery();
                dbcon.con.Close();
            }
        }

        // Get work experience from DB
        public void GetWorkExperience()
        {
            using (DbCon dbcon = new DbCon())
            {
                string query = "SELECT * FROM WorkExperience WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(query, dbcon.con);
                cmd.Parameters.AddWithValue("@Id", Id);

                dbcon.con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    WorkXp = reader.GetInt32(reader.GetOrdinal("WorkXp"));
                    WorkLevel = reader.GetInt32(reader.GetOrdinal("WorkLevel"));
                    XpUntilNextLevel = reader.GetInt32(reader.GetOrdinal("XpUntilNextLevel"));
                    dbcon.con.Close();
                }
                else
                {
                    dbcon.con.Close();
                    InsertWorkExperience();
                }
            }
        }

        // Update the user's work experience (used after gaining XP or leveling up)
        public void UpdateWorkExperience()
        {
            using (DbCon dbcon = new DbCon())
            {
                string query = "UPDATE WorkExperience SET WorkXp=@Xp, WorkLevel=@Level, XpUntilNextLevel=@XpNext WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(query, dbcon.con);
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@Xp", WorkXp);
                cmd.Parameters.AddWithValue("@Level", WorkLevel);
                cmd.Parameters.AddWithValue("@XpNext", XpUntilNextLevel);

                dbcon.con.Open();
                cmd.ExecuteNonQuery();
                dbcon.con.Close();
            }
        }

        // Static method to calculate required XP
        public static int CalculateXpForNextLevel(int level)
        {
            return (int)(50 * level * 1.5);
        }
    }
}
