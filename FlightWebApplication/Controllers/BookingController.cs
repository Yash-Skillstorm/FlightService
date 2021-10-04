using FlightWebApplication.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightWebApplication.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingDAO bookingDao;

        public BookingController(IBookingDAO bookingDao)
        {
            this.bookingDao = bookingDao;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
