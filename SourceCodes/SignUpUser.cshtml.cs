using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
using System.Net.Mail;


namespace WebApplication1.Pages
{

    public class SignUpUserModel : PageModel
    {
        private const string ConnectionString = "server=bookingmysql;user=root;password=my-secret-pw;database=booking;";
        private string sql;
        private bool valid = true;
        MySql.Data.MySqlClient.MySqlCommand cmd;

        public string Message { get; set; }
        [BindProperty]
        public string Password { get; set; }
        [BindProperty]
        public string ConfirmPassword { get; set; }
        [BindProperty]
        public string Username { get; set; }
        [BindProperty]
        public string Fname { get; set; }

        [BindProperty]
        public string Sname { get; set; }
        [BindProperty]
        public string Title { get; set; }

        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string PhoneNo { get; set; }

        public void OnGet()
        {
            Message = HttpContext.Session.GetString("msg");
            HttpContext.Session.Remove("msg");
        }
        public bool IsValidEmail(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        public IActionResult OnPost()
        {
            if (Username == null || Password == null || Fname == null || Sname == null || Email == null || PhoneNo == null)
            {
                valid = false;
                Message += "Please fill out all details" + "\n";
            }
            else
            {
                MySqlConnection connection = new MySqlConnection(ConnectionString);
                connection.Open();
                MySqlCommand command = new MySqlCommand("select username from users", connection);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    

                    if (Username.Equals((string)reader["username"]))
                    {
                     
                        valid = false;
                        Message += "Username is already taken" + "\n";
                        
                    }
    
                }
                connection.Close();
                connection.Open();
                command = new MySqlCommand("select username from pubs", connection);
                reader = command.ExecuteReader();
                while (reader.Read())
                {


                    if (Username.Equals((string)reader["username"]))
                    {

                        valid = false;
                        Message += "Username is already taken" + "\n";

                    }

                }
                connection.Close();
                if (ConfirmPassword != Password)
                {
                    valid = false;
                    Message += "Confirm password did not match password" + "\n";
                }
                if (Username.Length >= 100)
                {
                    valid = false;
                    Message += "Username is too long" + "\n";
                }
                if (Password.Length >= 100)
                {
                    valid = false;
                    Message += "Password is too long" + "\n";
                }
                if (Fname.Length >= 100)
                {
                    valid = false;
                    Message += "First name is too long" + "\n";
                }
                if (Sname.Length >= 100)
                {
                    valid = false;
                    Message += "Surname is too long" + "\n";
                }
                if (Email.Length >= 255)
                {
                    valid = false;
                    Message += "Email is too long" + "\n";
                }
                if (PhoneNo.Length >= 20)
                {
                    valid = false;
                    Message += "Phone Number is invalid" + "\n";
                }
                if (!Regex.Match(Fname, "^[A-Z][a-zA-Z]*$").Success)
                {
                    valid = false;
                    Message += "Invalid First name" + "\n"; 
                }
                if (!Regex.Match(Sname, "^[A-Z][a-zA-Z]*$").Success)
                {
                    valid = false;
                    Message += "Invalid Surname" + "\n"; 
                }
                if (!IsValidEmail(Email))
                {
                    valid = false;
                    Message += "Invalid Email" + "\n";
                }
                if (!Regex.Match(PhoneNo, @"^(((\+44\s?\d{4}|\(?0\d{4}\)?)\s?\d{3}\s?\d{3})|((\+44\s?\d{3}|\(?0\d{3}\)?)\s?\d{3}\s?\d{4})|((\+44\s?\d{2}|\(?0\d{2}\)?)\s?\d{4}\s?\d{4}))(\s?\#(\d{4}|\d{3}))?$").Success)
                {
                    valid = false;
                    Message += "Invalid Phone Number" + "\n";
                }
            }
            if (valid)
            {
                cmd = new MySql.Data.MySqlClient.MySqlCommand();
                MySqlConnection connection = new MySqlConnection(ConnectionString);
                connection.Open();
                sql = "INSERT IGNORE INTO users(username, password, title, fname, sname, email, phoneno) VALUES (@username, @password, @title, @fname, @sname, @email, @phoneno);";

                cmd.Connection = connection;
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@username", Username);
                cmd.Parameters.AddWithValue("@password", Password);
                cmd.Parameters.AddWithValue("@title", Title);
                cmd.Parameters.AddWithValue("@fname", Fname);
                cmd.Parameters.AddWithValue("@sname", Sname);
                cmd.Parameters.AddWithValue("@email", Email);
                cmd.Parameters.AddWithValue("@phoneno", PhoneNo);

                cmd.ExecuteNonQuery();
                connection.Close();
                return RedirectToPage("Index");
            }
            else
            {
                HttpContext.Session.SetString("msg", Message);
                return RedirectToPage("SignUpUser");
            }

        }
    }
}