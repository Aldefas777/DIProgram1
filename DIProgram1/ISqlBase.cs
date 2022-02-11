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
    public interface ISqlBase
    {
       List<User> GetUsers();
        void AddUsers(string names);
        void UpdateUser(string names, string id);
        void DeleteUser(string id);
    }
}
