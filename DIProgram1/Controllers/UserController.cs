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
        IUserRepository userRepository = new UserRepository();

        public UserController(IConfiguration config)
        {

            _config = config;
        }

        public IActionResult GetUser()
        {
            var model = GetUsers();

            return View(model);
        }

        public IActionResult Delete(int? id)
        {
            ViewBag.Id = id;
            var model = DeleteUser(id);
            return View(model);
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
            var model = UpdateUser(id, names);
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        private ActionResult AddUsers(string names)
        {
            userRepository.AddUsers(names);
            return View();
        }

        [HttpGet]
        private List<User> GetUsers()
        {

            return userRepository.GetUsers();
        }

        [HttpDelete("{id}")]
        private ActionResult DeleteUser(int? id)
        {
            userRepository.DeleteUser(id);
            return View();
        }

        [HttpPut("{id}")]
        private ActionResult UpdateUser(int? id, string names)
        {
            userRepository.UpdateUser(names, id);
            return View();
        }
    }
}
