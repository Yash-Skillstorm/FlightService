using FlightWebApplication.Data;
using FlightWebApplication.Models;
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
        ReferanceTableData GetTableData = new ReferanceTableData();
        public BookingController(IBookingDAO bookingDao)
        {
            this.bookingDao = bookingDao;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Booking> model = bookingDao.GetBookings();
            return View(model);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            Booking model = bookingDao.GetBooking(id);
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            IEnumerable<Passenger> modPass = GetTableData.GetPassengerTable();
            BookingViewModel model = new BookingViewModel();
            IEnumerable<Flight> modFlight = GetTableData.GetFlightsTable();
            model.flightData = modFlight;            
            ViewBag.mp = modPass;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] BookingViewModel booking)
        {
            ViewBag.m = "";
            if (ModelState.IsValid)
            {
                Booking newBooking = new Booking();
                newBooking.Flight_Id = booking.SelectedItem;
                newBooking.Passenger_id = booking.Passenger_id;
                int number = bookingDao.AddBooking(newBooking);
                if (number == 1)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.m = "Seat Capacity is full. You cannot add more members to this flight.";
                }
            }
            return Create();
            //return View(booking);         
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Booking model = bookingDao.GetBooking(id);
            IEnumerable<ActiveFlight> mod = GetTableData.GetTable();
            IEnumerable<Passenger> modPass = GetTableData.GetPassengerTable();
            ViewBag.m = mod;
            ViewBag.mp = modPass;
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind] Booking booking)
        {
            if (id != booking.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                bookingDao.UpdateBooking(booking);
                return RedirectToAction("Index");
            }
            return View(booking);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Booking model = bookingDao.GetBooking(id);            
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                bookingDao.DeleteBooking(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
