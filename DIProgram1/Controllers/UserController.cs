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
        public IUserRepository _userRepository;

        public UserController(IConfiguration config, IUserRepository userRepository)
        {

            _userRepository = userRepository;
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
            _userRepository.AddUsers(names);
            return View();
        }

        [HttpGet]
        private List<User> GetUsers()
        {

            return _userRepository.GetUsers();
        }

        [HttpDelete("{id}")]
        private ActionResult DeleteUser(int? id)
        {
            _userRepository.DeleteUser(id);
            return View();
        }

        [HttpPut("{id}")]
        private ActionResult UpdateUser(int? id, string names)
        {
            _userRepository.UpdateUser(names, id);
            return View();
        }
    }
}
