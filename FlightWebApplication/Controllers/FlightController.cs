using FlightWebApplication.Data;
using FlightWebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace FlightWebApplication.Controllers
{
    public class FlightController : Controller
    {
        private readonly IFlightDAO flightDao;
        ReferanceTableData GetTableData = new ReferanceTableData();
        public FlightController(IFlightDAO flightDao)
        {
            this.flightDao = flightDao;
        }
        

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Flight> model = flightDao.GetFlights();
            IEnumerable<ActiveFlight> mod = GetTableData.GetTable();

            foreach (var item in model)
            {                
                var replaceFlight_Num = mod.Where(i => item.Flight_Num.Equals(i.Id)).SingleOrDefault();
                if(replaceFlight_Num.Id == item.Flight_Num)
                {
                    item.Flight_Num = replaceFlight_Num.Flight_Number;
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            Flight model = flightDao.GetFlight(id);
            model.Flight_Num = GetTableData.GetTable().Where(i => model.Flight_Num.Equals(i.Id)).SingleOrDefault().Flight_Number;
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            IEnumerable<ActiveFlight> mod = GetTableData.GetTable();
            ViewBag.m = mod;
            //ViewBag.m = new SelectList(, "activeflight_Id", "flight_Number")
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] Flight flight)
        {
            if (ModelState.IsValid)
            {
                flightDao.AddFlight(flight);
                return RedirectToAction("Index");
            }

            return View(flight);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Flight model = flightDao.GetFlight(id);
            IEnumerable<ActiveFlight> mod = GetTableData.GetTable();
            ViewBag.m = mod;
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind] Flight flight)
        {
            if (id != flight.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                flightDao.UpdateFlight(flight);
                return RedirectToAction("Index");
            }
            return View(flightDao);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Flight model = flightDao.GetFlight(id);

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
                flightDao.DeleteFlight(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
