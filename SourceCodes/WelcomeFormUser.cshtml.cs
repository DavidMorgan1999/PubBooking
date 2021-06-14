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
    public class WelcomeFormUserModel : PageModel
    {

        public string Name { get; set; }
        public void OnGet()
        {
            Name = HttpContext.Session.GetString("name");
        }
        public IActionResult OnPostMakeBooking()
        {
            return RedirectToPage("MakeBookingFormUser");
        }
        public IActionResult OnPostCheckBooking()
        {
            return RedirectToPage("CheckBookingFormUser");
        }
        public IActionResult OnPostLogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("Index");
        }
        
    }
}

