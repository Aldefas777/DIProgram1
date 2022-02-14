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
    public class HomeController : Controller
    {
        private readonly IConfiguration _config;

        public HomeController(IConfiguration config)
        {

            _config = config;
        }


        ISqlBase sqlBase = new SqlBase();


        // GET: HomeController
        public IActionResult Index()
        {
            var model = GetUsers();

            return View(model);
        }


        public IActionResult Delete(int? id)
        {
            ViewBag.Id = id;
            sqlBase.DeleteUser(id);
            return View();
        }


        public IActionResult Privacy(string Names)
        {
            var model = AddUsers(Names);

            return View(model);
        }

        public IActionResult Update(string names, string id)
        {
            var model = UpdateUser(names, id);

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private List<User> GetUsers()
        {
            var result = sqlBase.GetUsers();
            return result;
        }

        [HttpPost]
        private ActionResult AddUsers(string names)
        {
            sqlBase.AddUsers(names);
            return View();
        }

        [HttpPost]
        private ActionResult UpdateUser(string names, string id)
        {
            sqlBase.UpdateUser(names, id);
            return View();
        }

    }
}