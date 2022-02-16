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
    public class UserRepository : IUserRepository
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
        public void AddUsers(string names)
        {
            Add(names);
        }

        public void DeleteUser(int? id)
        {
            Delete(id);
        }

        public void GetUserById(int? id)
        {
            UserById(id);
        }

        public List<User> GetUsers()
        {
            return Get();
        }

        public void UpdateUser(string names, int? id)
        {
            Update(names, id);
        }

        [HttpPost]
        public void Add(string names)
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
        public void Delete(int? id)
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

        [HttpGet]
        public List<User> Get()
        {
            using (var db = GetConnection())
            {
                var result = db.Query<User>("SELECT * FROM [User]").ToList();

                return result;
            }
        }
        [HttpPut("{Id}")]
        public void Update(string names, int? id)
        {
            using (var db = GetConnection())
            {
                db.Open();
                SqlCommand insertCommand = new SqlCommand();
                insertCommand.Connection = db;


                if (names != null)
                {
                    insertCommand.CommandText = "UPDATE [User] SET Name = (@User) WHERE id = (@Id)";
                    insertCommand.Parameters.AddWithValue("@Id", id);
                    insertCommand.Parameters.AddWithValue("@User", names);
                    insertCommand.ExecuteReader();
                }


                db.Close();
            }
        }

        public void UserById(int? id)
        {
            using (var db = GetConnection())
            {
                db.Open();
                SqlCommand insertCommand = new SqlCommand();
                insertCommand.Connection = db;
                if (id != null)
                {
                    insertCommand.CommandText = "SELECT * FROM [User] WHERE id = (@Id)";
                    insertCommand.Parameters.AddWithValue("@Id", id);
                    insertCommand.ExecuteReader();
                }
            }
        }
    }
}
