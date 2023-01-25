using Car_Rental_System.Data;
using Car_Rental_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Car_Rental_System.Controllers
{
    public class ReservationController : Controller
    {
        private readonly CarRentalDbContext _db;
        public ReservationController(CarRentalDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var data = _db.Reservations.Include(x => x.Car).Include(x=> x.Customer).ToList();
            return View(data);
        }
        public IActionResult Create()
        {
            ViewBag.Car = new SelectList(_db.Cars.ToList(), "Id", "Model");
            ViewBag.Customer = new SelectList(_db.Customers.ToList(),"Id","Name");
            return View();
        }
        [HttpPost]
        public IActionResult Create(Reservation obj)
        {
            var car = _db.Cars.Find(obj.CarId);

            if (car.IsBooked == true)
            {
                ModelState.AddModelError("", "The car is already booked, please select another car.");
                return View("Create");
            }
            ViewBag.Car = new SelectList(_db.Cars.ToList(), "Id", "Model");
            ViewBag.Customer = new SelectList(_db.Customers.ToList(), "Id", "Name");

            var days = (obj.EndDate - obj.StartDate).TotalDays;

            var totalprice = car.DailyHirePrice * (double)days;

            obj.Total_Price = totalprice;

            _db.Reservations.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            ViewBag.Car = new SelectList(_db.Cars.ToList(), "Id", "Model");
            ViewBag.Customer = new SelectList(_db.Customers.ToList(), "Id", "Name");
            var data = _db.Reservations.Where(x => x.Id == id).FirstOrDefault();
            return View(data);
        }
        [HttpPost]
        public IActionResult Edit(Reservation obj)
        {
            ViewBag.Car = new SelectList(_db.Cars.ToList(), "Id", "Model");
            ViewBag.Customer = new SelectList(_db.Customers.ToList(), "Id", "Name");
            if(obj == null)
            {
                return View(obj);
            }
           
                _db.Reservations.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            ViewBag.Car = new SelectList(_db.Cars.ToList(), "Id", "Model");
            ViewBag.Customer = new SelectList(_db.Customers.ToList(), "Id", "Name");
            var data = _db.Reservations.Where(x => x.Id == id).FirstOrDefault();
            return View(data);
        }
        [HttpPost]
        public IActionResult Delete(Reservation obj)
        {
            ViewBag.Car = new SelectList(_db.Cars.ToList(), "Id", "Model");
            ViewBag.Customer = new SelectList(_db.Customers.ToList(), "Id", "Name");
            if (obj != null)
            {
                _db.Reservations.Remove(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
    }
}
