using FlightWebApplication.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightWebApplication.Controllers
{
    public class PassengerController : Controller
    {
        private readonly IPassengerDAO passengerDao;
        ReferanceTableData GetTableData = new ReferanceTableData();
        public PassengerController(IPassengerDAO passengerDao)
        {
            this.passengerDao = passengerDao;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Passenger> model = passengerDao.GetPassengers();
            return View(model);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            Passenger model = passengerDao.GetPassenger(id);
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] Passenger passenger)
        {
            if (ModelState.IsValid)
            {
                passengerDao.AddPassenger(passenger);
                return RedirectToAction("Index");
            }

            return View(passenger);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Passenger model = passengerDao.GetPassenger(id);

            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind] Passenger passenger)
        {
            if (id != passenger.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                passengerDao.UpdatePassenger(passenger);
                return RedirectToAction("Index");
            }
            return View(passengerDao);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.mp = "";
            Passenger model = passengerDao.GetPassenger(id);
            List<SeatCapacity> modSeat = GetTableData.CheckSeatCapacityTable(id, 0);
            if (modSeat.Count != 0)
            {
                ViewBag.mp = "The Passenger has Booking the Flight";
            }
            else
            {
                ViewBag.mp = "";
            }
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
                passengerDao.DeletePassenger(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
