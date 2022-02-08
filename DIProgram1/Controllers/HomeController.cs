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

        public IActionResult Delete(string id)
        {
            var model = DeleteUser(id);

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private List<User> GetUsers()
        {
            using (IDbConnection db = Connection)
            {
                var result = db.Query<User>("SELECT * FROM [User]").ToList();

                return result;
            }
        }

        [HttpPost]
        private ActionResult AddUsers(string names)
        {
            using(SqlConnection db = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                db.Open();
                SqlCommand insertCommand = new SqlCommand();
                insertCommand.Connection = db;

                
                if(names != null)
                {
                    insertCommand.CommandText = "INSERT INTO [User] VALUES (@User);";
                    insertCommand.Parameters.AddWithValue("@User", names);
                    insertCommand.ExecuteReader();
                }
               
                
                db.Close();
            }
            return View();
        }

        [HttpPost]
        private ActionResult UpdateUser(string names, string id)
        {
            using (SqlConnection db = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
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
            return View();
        }

        [HttpPost]
        private ActionResult DeleteUser(string id)
        {
            using (SqlConnection db = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                db.Open();
                SqlCommand insertCommand = new SqlCommand();
                insertCommand.Connection = db;


                if (id != null)
                {
                    insertCommand.CommandText = "DELETE FROM [User] WHERE id = (@Id)";
                    insertCommand.Parameters.AddWithValue("@Id", Convert.ToInt32(id));;
                    insertCommand.ExecuteReader();
                }


                db.Close();
            }
            return View();
        }

        public class User
        {
            public int id { get; set; }

            public string Name { get; set; }
        }
    }
}