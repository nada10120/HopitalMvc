using Microsoft.AspNetCore.Mvc;
using HospitalMvc.Net.Data;
using HospitalMvc.Net.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Threading.Tasks;

namespace HospitalMvc.Net.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int page = 1)
        {
          var doctors = _context.Doctors.ToList();
            return View(doctors);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Create(Doctor doctor, IFormFile? Img)
        {
            if (doctor != null && Img != null && Img.Length > 0)
            {
                var fileName = Guid.NewGuid() + Path.GetFileName(Img.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await Img.CopyToAsync(stream);
                }
                doctor.Img = fileName;
            }
            else
            {
                doctor.Img = nameof(Doctor) + ".jpg"; // default image
            }

            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var doctor = _context.Doctors.Find(id);
            if (doctor == null)
            {
                return NotFound();
            }
            return View(doctor);
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormFile? Img, bool deletePhoto = false)
        {
            var existingDoctor = await _context.Doctors.FindAsync(id);
            if (existingDoctor == null)
            {
                return NotFound();
            }

            // Read form fields manually (or use a ViewModel)
            var name = Request.Form["Name"];
            var specialization = Request.Form["Specialization"];

            existingDoctor.Name = name;
            existingDoctor.Specialization = specialization;

            if (deletePhoto && !string.IsNullOrEmpty(existingDoctor.Img) && existingDoctor.Img != "Doctor.jpg")
            {
                var photoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", existingDoctor.Img);
                if (System.IO.File.Exists(photoPath))
                {
                    System.IO.File.Delete(photoPath);
                }
                existingDoctor.Img = "Doctor.jpg";
            }

            if (Img != null && Img.Length > 0)
            {
                var fileName = Guid.NewGuid() + Path.GetFileName(Img.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await Img.CopyToAsync(stream);
                }
                existingDoctor.Img = fileName;
            }

            _context.Doctors.Update(existingDoctor);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var doctor = _context.Doctors.Find(id);
            if (doctor == null)
            {
                return NotFound();
            }
            _context.Doctors.Remove(doctor);
            _context.SaveChanges();
            // Delete the photo file if it exists
            var photoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", doctor.Img);
            if (System.IO.File.Exists(photoPath))
            {
                System.IO.File.Delete(photoPath);
            }
            return RedirectToAction("Index");

        }
    }
 
    }