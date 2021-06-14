using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;

namespace WebApplication1.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        private const string ConnectionString = "server=bookingmysql;user=root;password=my-secret-pw;database=booking;";
        private bool Invalid = true;
        private string username = " ", password = " ", id = " ";
        private string puboruser;
        public string Message { get; set; }
        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public string Username { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
               MySqlConnection connection = new MySqlConnection(ConnectionString);
                connection.Open();
                MySqlCommand command = new MySqlCommand("select id, username, password, fname, sname from users", connection);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                username = (string)reader["username"];
                password = (string)reader["password"];
                id = reader["id"].ToString();
                
                    if (Username.Equals(username) && Password.Equals(password))
                    {
                    puboruser = "WelcomeFormUser";
                        HttpContext.Session.SetString("name", (reader["fname"] + " " + reader["sname"]));
                        HttpContext.Session.SetString("userId", id);
                    Invalid = false;
                    break;
                    }
                    else
                    {
                    Message = "Incorrect Login";
                    }
                }
            connection.Close();
            connection.Open();
            command = new MySqlCommand("select id, username, password, name from pubs", connection);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                username = (string)reader["username"];
                password = (string)reader["password"];
                id = reader["id"].ToString();

                if (Username.Equals(username) && Password.Equals(password))
                {
                    puboruser = "WelcomeFormPub";
                    HttpContext.Session.SetString("name", (reader["name"].ToString()));
                    HttpContext.Session.SetString("pubId", id);
                    Invalid = false;
                    break;
                }
                else
                {
                    Message = "Incorrect Login";
                }
            }
            if (Invalid == false)
            {
                return RedirectToPage(puboruser);
            }
            else
            {
                return Page();
            }
                  
        }
        }
    }


