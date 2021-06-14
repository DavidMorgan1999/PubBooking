using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication1.Pages
{
    public class TableBookingFormUserModel : PageModel
    {


        private const string ConnectionString = "server=bookingmysql;user=root;password=my-secret-pw;database=booking;";
        private string id, pubid, tablename, tabledescription;
        private int maxno;
        private bool valid = true;
        private SeatingList tablesInfo_;
        private TimeList filltimes_;
        private string sql;
        MySql.Data.MySqlClient.MySqlCommand cmd;
        public string Message { get; set; }
        public string pubName { get; set; }
        public string Open { get; set; }
        public string Close { get; set; }
        public List<SeatingList> tablesInfo = new List<SeatingList>();
        public List<TimeList> filltimes { get; } = new List<TimeList>();
        
       
        public string Name { get; set; }
        [BindProperty]
        public string PubID { get; set; }
        [BindProperty]
        public string UserID { get; set; }

        [BindProperty]
        public string Date { get; set; }
        [BindProperty]
        public string Time { get; set; }
        [BindProperty]
        public string TableID { get; set; }
 
        public void OnGet()
        {
            Name = HttpContext.Session.GetString("name");
            Message = HttpContext.Session.GetString("msg");
            HttpContext.Session.Remove("msg");
            PubID = HttpContext.Session.GetString("pubId");
            UserID = HttpContext.Session.GetString("userId");
            Open = HttpContext.Session.GetString("pubOpen");
            pubName = HttpContext.Session.GetString("pubName");
            Close = HttpContext.Session.GetString("pubClose");
            Date = HttpContext.Session.GetString("bookingDate");
            var resultopen = Convert.ToDateTime(Open);
            var resultclose = Convert.ToDateTime(Close);
            int result = DateTime.Compare(resultopen, resultclose);          
            DateTime currentTime = resultopen;

            while ((result) < 0)
            {
                filltimes_ = new TimeList(currentTime.ToString("HH:mm"));
                filltimes.Add(filltimes_);
                currentTime = currentTime.AddMinutes(60);
                result = DateTime.Compare(currentTime, resultclose);
            }
            MySqlConnection connection = new MySqlConnection(ConnectionString);
            connection.Open();
            MySqlCommand command = new MySqlCommand("select id, pubId, maxNo, tablename, description FROM seating WHERE pubId = @pubid;", connection);
            command.Parameters.AddWithValue("@pubid", PubID);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                id = reader["id"].ToString();
                pubid = reader["pubId"].ToString();
                maxno = Int32.Parse(reader["maxNo"].ToString());
                tablename = reader["tablename"].ToString();
                tabledescription = reader["description"].ToString();

                tablesInfo_ = new SeatingList(id, pubid, maxno, tablename, tabledescription);
                tablesInfo.Add(tablesInfo_);
            }
            connection.Close();
        }

        public IActionResult OnPostBack()
        {
            return RedirectToPage("MakeBookingFormUser");
        }
        public IActionResult OnPostMakeBooking(string id)
        {
            UserID = HttpContext.Session.GetString("userId");
            Date = HttpContext.Session.GetString("bookingDate");
            HttpContext.Session.SetString("TableId", id);
            TableID = id;
            MySqlConnection connection = new MySqlConnection(ConnectionString);
            connection.Open();
            MySqlCommand command = new MySqlCommand("select bookingtime, bookingdate from seatbookings WHERE tableId = @tableid", connection);
            command.Parameters.AddWithValue("@tableid", TableID);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                if(string.Equals(Date, reader["bookingdate"].ToString()))
                {
                    if (string.Equals(Time, reader["bookingtime"].ToString()))
                    {
                        valid = false;
                    } else
                    {
                        valid = true;
                    }
                } else
                {
                    valid = true;
                }
            }
            connection.Close();
            if (valid)
            {
                cmd = new MySql.Data.MySqlClient.MySqlCommand();
                connection.Open();
                sql = "INSERT IGNORE INTO seatbookings(userId, tableId, bookingtime, bookingdate) VALUES (@userid, @tableid, @time, @date);";

                cmd.Connection = connection;
                cmd.CommandText = sql; 
                cmd.Parameters.AddWithValue("@userid", UserID);
                cmd.Parameters.AddWithValue("@tableid", TableID);
                cmd.Parameters.AddWithValue("@time", Time);
                cmd.Parameters.AddWithValue("@date", Date);

                cmd.ExecuteNonQuery();
                connection.Close();
                return RedirectToPage("WelcomeFormUser");
            } else
            {
                HttpContext.Session.SetString("msg", "That table is already booked for that time."); 
                return RedirectToPage("TableBookingFormUser");
            }

        }
    }
}
