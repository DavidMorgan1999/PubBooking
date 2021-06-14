using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication1.Pages
{
    public class WelcomeFormPubModel : PageModel
    {
        public string Name { get; set; }
        public void OnGet()
        {
            Name = HttpContext.Session.GetString("name");
        }
        public IActionResult OnPostMakeBooking()
        {
            return RedirectToPage("MakeBookingFormPub");
        }
        public IActionResult OnPostCheckBooking()
        {
            return RedirectToPage("CheckBookingFormPub");
        }
        public IActionResult OnPostLogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("Index");
        }

    }
}

