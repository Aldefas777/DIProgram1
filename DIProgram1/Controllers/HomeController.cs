using Dapper;
using DIProgram1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace DIProgram1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _config;

        public HomeController(ILogger<HomeController> logger, IConfiguration config)
        {
            _logger = logger;

            _config = config;
        }


        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }

        // GET: HomeController
        public IActionResult Index()
        {
            var model = GetUsers();

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

        private List<User> GetUsers()
        {
            using (IDbConnection db = Connection)
            {
                var result = db.Query<User>("SELECT * FROM User").ToList();

                return result;
            }
        }

        public class User
        {
            public int id { get; set; }

            public string Name { get; set; }
        }
    }
}