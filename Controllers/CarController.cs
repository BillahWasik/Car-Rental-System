using Car_Rental_System.Data;
using Car_Rental_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Car_Rental_System.Controllers
{
    public class CarController : Controller
    {
        private readonly CarRentalDbContext _db;
        private readonly IWebHostEnvironment _env;
        public CarController(CarRentalDbContext db , IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public IActionResult Index()
        {
           
                var cars = _db.Cars.ToList();
                var reservations = _db.Reservations.Where(r => r.StartDate <= DateTime.Now && r.EndDate >= DateTime.Now).ToList();
                foreach (var car in cars)
                {
                    var reservation = reservations.FirstOrDefault(r => r.CarId == car.Id);
                    if (reservation != null)
                    {
                        car.IsBooked = true;
                    }
                    else
                    {
                        car.IsBooked = false;
                    }
                    _db.Cars.Update(car);
                }
                _db.SaveChanges();
            
            var data = _db.Cars.ToList();
            return View(data);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Car obj)
        {
            if (obj.Image != null)
            {
                string path = "Image/Car_Image/";

                path += Guid.NewGuid().ToString() + "_" + obj.Image.FileName;

                string FullPath = Path.Combine(_env.WebRootPath, path);

                if (obj.ImageUrl != null)
                {
                    var OldPath = Path.Combine(_env.WebRootPath, obj.ImageUrl);

                    if (System.IO.File.Exists(OldPath))
                    {
                        System.IO.File.Delete(OldPath);
                    }
                }

                obj.Image.CopyTo(new FileStream(FullPath, FileMode.Create));

                obj.ImageUrl = path;
            }
                _db.Cars.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var data = _db.Cars.Where(x => x.Id == id).FirstOrDefault();
            return View(data);
        }
        [HttpPost]
        public IActionResult Edit(Car obj)
        {
            if (obj.Image != null)
            {
                string path = "Image/Car_Image/";

                path += Guid.NewGuid().ToString() + "_" + obj.Image.FileName;

                string FullPath = Path.Combine(_env.WebRootPath, path);

                if (obj.ImageUrl != null)
                {
                    var OldPath = Path.Combine(_env.WebRootPath, obj.ImageUrl);

                    if (System.IO.File.Exists(OldPath))
                    {
                        System.IO.File.Delete(OldPath);
                    }
                }

                obj.Image.CopyTo(new FileStream(FullPath, FileMode.Create));

                obj.ImageUrl = path;
            }
            _db.Cars.Update(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var data = _db.Cars.Where(x=> x.Id == id).FirstOrDefault();
            return View(data);
        }
        [HttpPost]
        public IActionResult Delete(Car obj)
        {
            if (obj.Image != null)
            {
                string path = "Image/Car_Image/";

                path += Guid.NewGuid().ToString() + "_" + obj.Image.FileName;

                string FullPath = Path.Combine(_env.WebRootPath, path);

                if (obj.ImageUrl != null)
                {
                    var OldPath = Path.Combine(_env.WebRootPath, obj.ImageUrl);

                    if (System.IO.File.Exists(OldPath))
                    {
                        System.IO.File.Delete(OldPath);
                    }
                }

                obj.Image.CopyTo(new FileStream(FullPath, FileMode.Create));

                obj.ImageUrl = path;
            }
            if (obj == null)
            {
                return View();
            }
            _db.Cars.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
