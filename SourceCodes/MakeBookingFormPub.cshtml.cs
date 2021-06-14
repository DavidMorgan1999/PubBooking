using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace WebApplication1.Pages
{
    public class MakeBookingFormPubModel : PageModel
    {
        private const string ConnectionString = "server=bookingmysql;user=root;password=my-secret-pw;database=booking;";
        private static readonly Random random = new Random();
        private string id, name, street, town, postcode, open, close, username, password;
        private string sql;
        private bool valid = true;
        MySql.Data.MySqlClient.MySqlCommand cmd;

        public string Username { get; set; }
        public string Password { get; set; }
        [BindProperty]
        public string Day { get; set; }
        [BindProperty, DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public string Today { get; set; }
        public string Message { get; set; }
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

        public static string GenerateRandomUsernamePassword()
        {
            string RandomValue = "";

            char[] lowers = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'j', 'k', 'm', 'n', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
            char[] uppers = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            char[] numbers = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

            int l = lowers.Length;
            int u = uppers.Length;
            int n = numbers.Length;

            RandomValue += lowers[random.Next(0, l)].ToString();
            RandomValue += lowers[random.Next(0, l)].ToString();
            RandomValue += lowers[random.Next(0, l)].ToString();

            RandomValue += uppers[random.Next(0, u)].ToString();
            RandomValue += uppers[random.Next(0, u)].ToString();
            RandomValue += uppers[random.Next(0, u)].ToString();

            RandomValue += numbers[random.Next(0, n)].ToString();
            RandomValue += numbers[random.Next(0, n)].ToString();
            RandomValue += numbers[random.Next(0, n)].ToString();

            return RandomValue;
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
            Day = Date.ToString("dddd");
            HttpContext.Session.SetString("bookingDate", Date.ToShortDateString());

            MySqlConnection connection = new MySqlConnection(ConnectionString);
            connection.Open();
            MySqlCommand command = new MySqlCommand("select day.open, day.close FROM day INNER JOIN pubs ON pubs.id = day.pubId WHERE day.day = @day AND pubs.id = @pubid;", connection);
            command.Parameters.AddWithValue("@day", Day);
            command.Parameters.AddWithValue("@pubid", HttpContext.Session.GetString("pubId"));
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                open = reader["open"].ToString();
                close = reader["close"].ToString();
            }
            HttpContext.Session.SetString("pubOpen", open);
            HttpContext.Session.SetString("pubClose", close);
            connection.Close();
            Username = GenerateRandomUsernamePassword();
            Password = GenerateRandomUsernamePassword();
            if (Fname == null || Sname == null || Email == null || PhoneNo == null)
            {
                valid = false;
                Message += "Please fill out all details" + "\n";
            }
            else
            {
                connection = new MySqlConnection(ConnectionString);
                connection.Open();
                command = new MySqlCommand("select username from users", connection);
                reader = command.ExecuteReader();
                while (reader.Read())
                {


                    while (Username.Equals((string)reader["username"]))
                    {

                        Username = GenerateRandomUsernamePassword();

                    }

                }
                connection.Close();
                connection.Open();
                command = new MySqlCommand("select username from pubs", connection);
                reader = command.ExecuteReader();
                while (reader.Read())
                {


                    while (Username.Equals((string)reader["username"]))
                    {

                        Username = GenerateRandomUsernamePassword();

                    }

                }
                connection.Close();
                             
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
                connection = new MySqlConnection(ConnectionString);
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

                connection.Open();
                cmd = new MySqlCommand("select id, username, password from users", connection);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    username = (string)reader["username"];
                    password = (string)reader["password"];
                    id = reader["id"].ToString();

                    if (Username.Equals(username) && Password.Equals(password))
                    {
                        HttpContext.Session.SetString("userId", id);
                        break;
                    }
                }
                connection.Close();
                HttpContext.Session.SetString("msg", "");
                return RedirectToPage("TableBookingFormPub");
            }
            else
            {
                HttpContext.Session.SetString("msg", Message);
                return RedirectToPage("MakeBookingFormPub");
            }

        }

        public IActionResult OnPostBack()
        {
            return RedirectToPage("WelcomeFormPub");
        }
        public void OnGet()
        {
            Today = DateTime.Now.ToString("yyyy-MM-dd");
            Message = HttpContext.Session.GetString("msg");
        }
    }
}


