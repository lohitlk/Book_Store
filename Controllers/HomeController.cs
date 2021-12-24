using Book_Store.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace Book_Store.Controllers
{
    public class HomeController : Controller
    {
        public IConfiguration Configuration { get; }
        public HomeController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(Register register)
        {
            var result = 1;
            string connectionString = Configuration["ConnectionStrings:MyConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Insert Into Register (name, email, password) Values ('{register.name}', '{register.email}','{register.password}')";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    result = 0;
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            
            ViewBag.Result = 1;
            ViewBag.message = "User Registered sucessfully";
            if(result == 0)
            {
                return View("Login");
            }
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Register register)
        {
            var num = 1;
            var userid = 0 ;
            var admin = false;
            List<Login> user = new List<Login>();
            string connectionString = Configuration["ConnectionStrings:MyConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Select id, email, password From Register where email = '{register.email}' and password = '{register.password}'";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Connection = connection;
                    connection.Open();
                    using (SqlDataReader sdr = command.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            userid = Convert.ToInt32(sdr["id"]);
                            HttpContext.Session.SetInt32("userid", userid);
                        }
                    }
                    var result = command.ExecuteScalar();
                    if(result!=null){
                        num = 0;
                        if(register.email == "admin")
                        {
                            admin = true;
                        }
                    }
                    HttpContext.Session.SetString("User", register.email);
                    connection.Close();
                }

            }
            if(admin == true)
            {
                return View("Admin");
            }
            if (num == 0)
            {
                return View("Index");
            }
            return View();
        }
        public IActionResult Admin()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Admin(Books book)
        {
            var result = 1;
            string connectionString = Configuration["ConnectionStrings:MyConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Insert Into Books (Book_Type, Name, Cost, Author, Discription) Values ('{book.Book_Type}', '{book.Name}','{book.Cost}','{book.Author}' ,'{book.Discription}')";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    command.ExecuteNonQuery();
                    result = 0;
                    connection.Close();
                }
            }
            if (result == 0)
            {
                ViewBag.message = "Row inserted sucessfully";
            }
            return View();
        }
        public IActionResult Activity_Book()
        {
            string connectionString = Configuration["ConnectionStrings:MyConnection"];
            List<Books> activity_book = new List<Books>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Books where Book_Type = 'Activity'";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            activity_book.Add(new Books
                            {
                                Book_Id = Convert.ToInt32(sdr["Book_id"]),
                                Book_Type = sdr["Book_Type"].ToString(),
                                Name = sdr["Name"].ToString(),
                                Cost= Convert.ToInt32(sdr["Cost"]),
                                Author = sdr["Author"].ToString(),
                                Discription = sdr["Discription"].ToString()
                            });
                        }
                    }
                    con.Close();
                }
            }

            return View(activity_book);
        }
        public IActionResult Story_Book()
        {
            string connectionString = Configuration["ConnectionStrings:MyConnection"];
            List<Books> activity_book = new List<Books>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Books where Book_Type = 'Story'";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            activity_book.Add(new Books
                            {
                                Book_Id = Convert.ToInt32(sdr["Book_id"]),
                                Book_Type = sdr["Book_Type"].ToString(),
                                Name = sdr["Name"].ToString(),
                                Cost = Convert.ToInt32(sdr["Cost"]),
                                Author = sdr["Author"].ToString(),
                                Discription = sdr["Discription"].ToString()
                            });
                        }
                    }
                    con.Close();
                }
            }

            return View(activity_book);
        }
        public IActionResult Study_Book()
        {
            string connectionString = Configuration["ConnectionStrings:MyConnection"];
            List<Books> activity_book = new List<Books>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Books where Book_Type = 'Study'";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            activity_book.Add(new Books
                            {
                                Book_Id = Convert.ToInt32(sdr["Book_id"]),
                                Book_Type = sdr["Book_Type"].ToString(),
                                Name = sdr["Name"].ToString(),
                                Cost = Convert.ToInt32(sdr["Cost"]),
                                Author = sdr["Author"].ToString(),
                                Discription = sdr["Discription"].ToString()
                            });
                        }
                    }
                    con.Close();
                }
            }

            return View(activity_book);
        }
        public IActionResult Cart()
        {
            var user = this.HttpContext.Session.GetString("User");
            string connectionString = Configuration["ConnectionStrings:MyConnection"];
            List<Books> cart = new List<Books>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "select * from Books b, Cart c where c.user_email = @user and b.Book_Id = c.Book_id;";
                
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = user;
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            cart.Add(new Books
                            {
                                Book_Id = Convert.ToInt32(sdr["Book_id"]),
                                Book_Type = sdr["Book_Type"].ToString(),
                                Name = sdr["Name"].ToString(),
                                Cost = Convert.ToInt32(sdr["Cost"]),
                                Author = sdr["Author"].ToString(),
                                Discription = sdr["Discription"].ToString()
                            });
                        }
                    }
                    con.Close();
                }
            }
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "select sum(b.Cost) as total from Books b, Cart c  where c.user_email = @user and b.Book_Id = c.Book_id;";

                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = user;
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            ViewBag.Total = Convert.ToInt32(sdr["total"]); 
                        }
                    }
                    con.Close();
                }
            }
            return View(cart);
        }
        public IActionResult AddCart(int id, string user, int price)
        {
            string connectionString = Configuration["ConnectionStrings:MyConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Insert Into Cart (cart_totalprice, Book_id, user_email) Values ('{price}', '{id}','{user}')";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return View("Index");
        }
        public IActionResult Checkout()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Checkout( UserDetails user)
        {
            var userid = HttpContext.Session.GetInt32("userid");
            string connectionString = Configuration["ConnectionStrings:MyConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Insert Into UserDetails (userdetails_name, userdetais_email, userdetails_address, userdetails_state, userdetails_pincode, id) Values ('{user.userdetails_name}', '{user.userdetais_email}','{user.userdetails_address}','{user.userdetails_state}', '{user.userdetails_pincode}', '{@userid}' )";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@userid", SqlDbType.VarChar).Value = userid;
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return View("Sucess");
        }
        public IActionResult Sucess()
        {
            return View();
        }
        public IActionResult ViewBooks()
        {
            string connectionString = Configuration["ConnectionStrings:MyConnection"];
            List<Books> book = new List<Books>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "select * from Books;";

                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            book.Add(new Books
                            {
                                Book_Id = Convert.ToInt32(sdr["Book_id"]),
                                Book_Type = sdr["Book_Type"].ToString(),
                                Name = sdr["Name"].ToString(),
                                Cost = Convert.ToInt32(sdr["Cost"]),
                                Author = sdr["Author"].ToString(),
                                Discription = sdr["Discription"].ToString()
                            });
                        }
                    }
                    con.Close();
                }
            }
            return View(book);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
