using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace WebApplication1.Pages.Shared
{
    public class CheckBookingFormPubModel : PageModel
    {
        private const string ConnectionString = "server=bookingmysql;user=root;password=my-secret-pw;database=booking;";
        private string bookingid, userid, tableid, pubid, bookingdate, bookingtime, name, tablename, contacts, maxnoseats;
        private BookingList bookingInfo_;
        private string sql;
        MySql.Data.MySqlClient.MySqlCommand cmd;
        public string Message { get; set; }
        public List<BookingList> bookingInfo = new List<BookingList>();
        public string FsName { get; set; }
        public string PubID { get; set; }
        public string BookingID { get; set; }

        public IActionResult OnPostBack()
        {
            return RedirectToPage("WelcomeFormPub");
        }
        public IActionResult OnPostDeleteBooking(string id)
        {
            BookingID = id;
            cmd = new MySql.Data.MySqlClient.MySqlCommand();
            MySqlConnection connection = new MySqlConnection(ConnectionString);
            connection.Open();
            sql = "DELETE FROM seatbookings WHERE id = @bookingid;";

            cmd.Connection = connection;
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@bookingid", BookingID);

            cmd.ExecuteNonQuery();
            connection.Close();

            return RedirectToPage("CheckBookingFormPub");
        }
        public void OnGet()
        {
            FsName = HttpContext.Session.GetString("name");
            PubID = HttpContext.Session.GetString("pubId");
            MySqlConnection connection = new MySqlConnection(ConnectionString);
            connection.Open();
            MySqlCommand command = new MySqlCommand("select seatbookings.id, seatbookings.userId, seatbookings.tableId, seating.pubId, seatbookings.bookingtime, seatbookings.bookingdate, seating.tablename, seating.maxNo, users.title, users.fname, users.sname, users.email, users.phoneno FROM seatbookings INNER JOIN seating ON seating.id = seatbookings.tableId INNER JOIN users ON users.id = seatbookings.userId WHERE seating.pubId = @pubid;", connection);
            command.Parameters.AddWithValue("@pubid", PubID);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                bookingid = reader["id"].ToString();
                userid = reader["userId"].ToString();
                tableid = reader["tableId"].ToString();
                pubid = reader["pubId"].ToString();
                bookingdate = reader["bookingdate"].ToString();
                bookingtime = reader["bookingtime"].ToString();
                name = reader["title"].ToString() + " " + reader["fname"].ToString() + " " + reader["sname"].ToString();
                tablename = reader["tablename"].ToString();
                contacts = reader["phoneno"].ToString() + " \n" + reader["email"].ToString();
                maxnoseats = reader["maxNo"].ToString();

                bookingInfo_ = new BookingList(bookingid, userid, tableid, pubid, bookingdate, bookingtime, name, tablename, contacts, maxnoseats);
                bookingInfo.Add(bookingInfo_);
                // Message = Message + reader["name"] + " " + reader["street"] + " " + reader["town"] + " " + reader["postcode"] + " ;";
            }
            connection.Close();
        }
    }
}



