using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace WebApplication1.Pages
{

    public class MakeBookingFormUserModel : PageModel
    {
    

        private const string ConnectionString = "server=bookingmysql;user=root;password=my-secret-pw;database=booking;";
        private string id, name, street, town, postcode, open, close;
        private PubList pubInfo_;

        [BindProperty]
        public string Day { get; set; }
        [BindProperty, DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [BindProperty]
        public string PubName { get; set; }
        public string Today { get; set; }
        public string Message { get; set; }
        public List<PubList> pubInfo = new List<PubList>();
        public string fsName { get; set; }
        
        public IActionResult OnPostBack()
        {
            return RedirectToPage("WelcomeFormUser");
        }
        public void OnPostSearchPubs()
        {
            Day = Date.ToString("dddd");
            Today = Date.ToString("yyyy-MM-dd");
            MySqlConnection connection = new MySqlConnection(ConnectionString);
            connection.Open();
            MySqlCommand command = new MySqlCommand("select pubs.id,pubs.name, pubs.street, pubs.town, pubs.postcode, day.open, day.close FROM day INNER JOIN pubs ON pubs.id = day.pubId WHERE day.day = @day AND pubs.town = @town;", connection);
            command.Parameters.AddWithValue("@day", Day);
            command.Parameters.AddWithValue("@town", PubName);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                id = reader["id"].ToString();
                name = reader["name"].ToString();
                street = reader["street"].ToString();
                town = reader["town"].ToString();
                postcode = reader["postcode"].ToString();
                open = reader["open"].ToString();
                close = reader["close"].ToString();
                pubInfo_ = new PubList(id, name, street, town, postcode, open, close);
                pubInfo.Add(pubInfo_);
                // Message = Message + reader["name"] + " " + reader["street"] + " " + reader["town"] + " " + reader["postcode"] + " ;";
            }
            connection.Close();
        }
        public void OnPostDayUpdate()
        {
            Day = Date.ToString("dddd");
            Today = Date.ToString("yyyy-MM-dd");
            MySqlConnection connection = new MySqlConnection(ConnectionString);
            connection.Open();
            MySqlCommand command = new MySqlCommand("select pubs.id,pubs.name, pubs.street, pubs.town, pubs.postcode, day.open, day.close FROM day INNER JOIN pubs ON pubs.id = day.pubId WHERE day.day = @day;", connection);
            command.Parameters.AddWithValue("@day", Day);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                id = reader["id"].ToString();
                name = reader["name"].ToString();
                street = reader["street"].ToString();
                town = reader["town"].ToString();
                postcode = reader["postcode"].ToString();
                open = reader["open"].ToString();
                close = reader["close"].ToString();
                pubInfo_ = new PubList(id, name, street, town, postcode, open, close);
                pubInfo.Add(pubInfo_);
                // Message = Message + reader["name"] + " " + reader["street"] + " " + reader["town"] + " " + reader["postcode"] + " ;";
            }
            connection.Close();
        }
        public IActionResult OnPostMakeBooking(string id, string open, string close, string name)
        {
            HttpContext.Session.SetString("bookingDate", Date.ToShortDateString());
            HttpContext.Session.SetString("pubId", id);
            HttpContext.Session.SetString("pubOpen", open);
            HttpContext.Session.SetString("pubClose", close);
            HttpContext.Session.SetString("pubName", name);
            return RedirectToPage("TableBookingFormUser");
        }
        public void OnGet()
        {
            fsName = HttpContext.Session.GetString("name");
            Today = DateTime.Now.ToString("yyyy-MM-dd");
            Day = DateTime.Now.ToString("dddd");
            MySqlConnection connection = new MySqlConnection(ConnectionString);
            connection.Open();
            MySqlCommand command = new MySqlCommand("select pubs.id,pubs.name, pubs.street, pubs.town, pubs.postcode, day.open, day.close FROM day INNER JOIN pubs ON pubs.id = day.pubId WHERE day.day = @day;", connection);
            command.Parameters.AddWithValue("@day", Day);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                id = reader["id"].ToString();
                name = reader["name"].ToString();
                street = reader["street"].ToString();
                town = reader["town"].ToString();
                postcode = reader["postcode"].ToString();
                open = reader["open"].ToString();
                close = reader["close"].ToString();
                pubInfo_ = new PubList(id, name, street, town, postcode, open, close);
                pubInfo.Add(pubInfo_);
                // Message = Message + reader["name"] + " " + reader["street"] + " " + reader["town"] + " " + reader["postcode"] + " ;";
            }
            connection.Close();
        }
    }
}


