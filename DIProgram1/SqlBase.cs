using DIProgram1.Controllers;
using DIProgram1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using Dapper;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using DIProgram1;

namespace DIProgram1
{
    public class SqlBase : ISqlBase
    {
        public SqlConnection GetConnection()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            string connectionString = config.GetConnectionString("DefaultConnection");
            return new SqlConnection(connectionString);
        }

        [HttpPost]
        public void AddUsers(string names)
        {

            using (var db = GetConnection())
            {
                db.Open();
                SqlCommand insertCommand = new SqlCommand();
                insertCommand.Connection = db;


                if (names != null)
                {
                    insertCommand.CommandText = "INSERT INTO [User] VALUES (@User);";
                    insertCommand.Parameters.AddWithValue("@User", names);
                    insertCommand.ExecuteReader();
                }


                db.Close();
            }
        }


        [HttpDelete("{Id}")]
        public void DeleteUser(int? id)
        {
            using (var db = GetConnection())
            {
                db.Open();
                SqlCommand insertCommand = new SqlCommand();
                insertCommand.Connection = db;

                if (id != null)
                {
                    insertCommand.CommandText = "DELETE FROM [User] WHERE id = (@Id)";
                    insertCommand.Parameters.AddWithValue("@Id", id); ;
                    insertCommand.ExecuteReader();
                }


                db.Close();
            }
        }

        public List<User> GetUsers()
        {
            using (var db = GetConnection())
            {
                var result = db.Query<User>("SELECT * FROM [User]").ToList();

                return result;
            }
        }
        [HttpPost]
        public void UpdateUser(string names, string id)
        {
            using (var db = GetConnection())
            {
                db.Open();
                SqlCommand insertCommand = new SqlCommand();
                insertCommand.Connection = db;


                if (names != null)
                {
                    insertCommand.CommandText = "UPDATE [User] SET Name = (@User) WHERE id = (@Id)";
                    insertCommand.Parameters.AddWithValue("@Id", Convert.ToInt32(id));
                    insertCommand.Parameters.AddWithValue("@User", names);
                    insertCommand.ExecuteReader();
                }


                db.Close();
            }
        }
    }
}
