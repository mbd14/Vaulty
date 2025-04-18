using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Vaulty.Database.Models
{
    public class Job
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public int Tier { get; set; }
        public int SalaryMin { get; set; }
        public int SalaryMax { get; set; }
        public int WorkXp {  get; set; }


        public void GetJob(int id)
        {
            DbCon dbCon = new DbCon();
            using (dbCon)
            {
                dbCon.con.Open();
                string query = "SELECT * FROM JOBS WHERE Id=@Id";
                SqlCommand command = new SqlCommand(query, dbCon.con);
                command.Parameters.AddWithValue("@Id", Id);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    Id = Id;
                    Label = reader.GetString(reader.GetOrdinal("Label"));
                    Tier = reader.GetInt32(reader.GetOrdinal("Tier"));
                    SalaryMin = reader.GetInt32(reader.GetOrdinal("SalaryMin"));
                    SalaryMax = reader.GetInt32(reader.GetOrdinal("SalaryMax"));
                    WorkXp = reader.GetInt32(reader.GetOrdinal("WorkXp"));
                }

                dbCon.con.Close();
            }
        }

        public static List<Job> GetJobList()
        {
            List<Job> list = new List<Job>();

            DbCon dbCon = new DbCon();
            using (dbCon)
            {
                dbCon.con.Open();
                string query = "SELECT * FROM JOBS";
                SqlCommand command = new SqlCommand(query, dbCon.con);

                SqlDataReader reader =  command.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Job()
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        Label = reader.GetString(reader.GetOrdinal("Label")),
                        Tier = reader.GetInt32(reader.GetOrdinal("Tier")),
                        SalaryMin = reader.GetInt32(reader.GetOrdinal("SalaryMin")),
                        SalaryMax = reader.GetInt32(reader.GetOrdinal("SalaryMax")),
                        WorkXp = reader.GetInt32(reader.GetOrdinal("WorkXp")),
                    });
                }
            }

            return list;
        }
    }
}
