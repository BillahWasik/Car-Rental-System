using Car_Rental_System.Data;
using Car_Rental_System.Models;
using Microsoft.AspNetCore.Mvc;

namespace Car_Rental_System.Controllers
{
    public class CustomerController : Controller
    {
        private readonly CarRentalDbContext _db;
        private readonly IWebHostEnvironment _env;
        public CustomerController(CarRentalDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public IActionResult Index()
        {
            var data = _db.Customers.ToList();
            return View(data);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Customer obj)
        {
            if (obj.Image != null)
            {
                string path = "Image/Customer_Image/";

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

            _db.Customers.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
           var data = _db.Customers.Where(x => x.Id == id).FirstOrDefault();
            return View(data);
        }
        [HttpPost]
        public IActionResult Edit(Customer obj)
        {
            if (obj.Image != null)
            {
                string path = "Image/Customer_Image/";

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
            _db.Customers.Update(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var data = _db.Customers.Where(x => x.Id == id).FirstOrDefault();
            return View(data);
        }
        [HttpPost]
        public IActionResult Delete(Customer obj)
        {
            if (obj.Image != null)
            {
                string path = "Image/Customer_Image/";

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
            _db.Customers.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
