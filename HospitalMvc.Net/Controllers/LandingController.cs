using HospitalMvc.Net.Data;
using HospitalMvc.Net.Models;
using Microsoft.AspNetCore.Mvc;

namespace HospitalMvc.Net.Controllers
{
    public class LandingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LandingController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var doctors = _context.Doctors.ToList();


            return View(doctors);
        }
        public IActionResult CheckApp(int page=1 , int pageSize=4 , string searchQuery="")
        {   var totalDoctors = _context.Doctors.Count();
            var totalPages = (int)Math.Ceiling((double)totalDoctors / pageSize);
            if (!string.IsNullOrEmpty(searchQuery))
            {
                var filteredDoctors = _context.Doctors
                    .Where(d => d.Name.Contains(searchQuery) || d.Specialization.Contains(searchQuery)).Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
                if (filteredDoctors.Count == 0)
                {
                    TempData["ErrorMessage"] = "No doctors found matching your search criteria.";
                }
               
                ViewBag.CurrentPage = page;
                ViewBag.TotalPages = totalPages;
                ViewBag.SearchQuery = searchQuery;
                return View(filteredDoctors);
            }

            
            var doctors = _context.Doctors
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.SearchQuery = searchQuery;
            return View(doctors);
        }
        [HttpGet("/Landing/GetAppoint/{id}")]
        public IActionResult GetAppoint(int id)
        {
            var doctor = _context.Doctors.Find(id);
            if (doctor == null)
                return NotFound();

            return View(doctor);
        }

        [HttpPost]
        public IActionResult GetAppoint(int doctorId, string PatientName, DateOnly AppointmentDate, TimeOnly AppointmentTime)
        {
            var appointment = new Appointments
            {
                DoctorId = doctorId,
                Name = PatientName,
                Date = AppointmentDate,
                Time = AppointmentTime
            };
            if (_context.Appointments.Any(a => a.DoctorId == doctorId && a.Date == AppointmentDate && a.Time == AppointmentTime))
            {
                TempData["ErrorMessage"] = "This appointment time is already taken. Please choose another one.";
                return RedirectToAction("GetAppoint", new { id = doctorId });
            }
            _context.Appointments.Add(appointment);
            _context.SaveChanges();
            TempData["SuccessMessage"] = "Your appointment has been made successfully wish you health .";


            return RedirectToAction("CheckApp"); 
        }
    }
}
