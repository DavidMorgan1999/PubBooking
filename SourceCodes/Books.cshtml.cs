using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace WebApplication1.Pages
{
    public class BooksModel : PageModel
    {
        private const string ConnectionString = "server=bookingmysql;user=root;password=my-secret-pw;database=booking;";

        public string Message { get; set; }
        
        public void OnGet()
        {
            MySqlConnection connection = new MySqlConnection(ConnectionString);
            connection.Open();
            MySqlCommand command = new MySqlCommand("select id, userId, tableId, bookingtime, bookingdate from seatbookings", connection);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                //Message = Message + reader["name"] + " " + reader["street"] + " " + reader["town"] + " " + reader["postcode"] + " " + reader["phoneno"] + " ;";
                Message = Message + reader["id"] + " " + reader["userId"] + " " + reader["tableId"] + " " + reader["bookingtime"] + " " + reader["bookingdate"] + " ;";
            }

            connection.Close();
           
        }
    }
}
