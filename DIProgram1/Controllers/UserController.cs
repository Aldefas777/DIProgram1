using DIProgram1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using Dapper;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using DIProgram1;

namespace DIProgram1.Controllers
{
    public class UserController : Controller
    {
        private readonly IConfiguration _config;

        public UserController(IConfiguration config)
        {

            _config = config;
        }


        ISqlBase sqlBase = new SqlBase();

        public IActionResult GetUser()
        {
            var model = sqlBase.GetUsers();

            return View(model);
        }

        public IActionResult Delete(int? id)
        {
            ViewBag.Id = id;
            sqlBase.DeleteUser(id);
            return View();
        }


        public IActionResult Add(string Names)
        {
            var model = AddUsers(Names);
            return View(model);
        }

        public IActionResult Update(int? id, string names)
        {
            ViewBag.Id = id;
            ViewBag.Name = names;
            sqlBase.UpdateUser(names, id);
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        private ActionResult AddUsers(string names)
        {
            sqlBase.AddUsers(names);
            return View();
        }

        private List<User> GetUsers()
        {
            var result = sqlBase.GetUsers();
            return result;
        }
    }
}
