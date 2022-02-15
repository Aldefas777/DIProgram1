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
        private readonly IConfiguration _config;

        public UserRepository(IConfiguration config)
        {

            _config = config;
        }


        IUserRepository sqlBase = new SqlBase();
        public void AddUsers(string names)
        {
            sqlBase.AddUsers(names);
        }

        public void DeleteUser(int? id)
        {
            sqlBase.DeleteUser(id);
        }

        public void GetUserById(int? id)
        {
            sqlBase.GetUserById(id);
        }

        public List<User> GetUsers()
        {
            return sqlBase.GetUsers();
        }

        public void UpdateUser(string names, int? id)
        {
            sqlBase.UpdateUser(names, id);
        }
    }
}
