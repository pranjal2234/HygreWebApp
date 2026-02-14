using Microsoft.AspNetCore.Mvc;
using HygreWebApp.Data;
using HygreWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace HygreWebApp.Controllers
{
    using HygreWebApp.Data;
    using HygreWebApp.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class TrainingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TrainingController(ApplicationDbContext context)
        {
            _context = context;
        }

        // SHOW AVAILABLE WEEKS
        public IActionResult Index()
        {
            var weeks = _context.TrainingWeeks
                .Include(w => w.Bookings)
                .OrderBy(w => w.WeekStartDate)
                .ToList();

            return View(weeks);
        }
        public IActionResult PaymentDetails()
        {
            var details = _context.PaymentDetails.FirstOrDefault();
            return View(details);
        }

        // BOOK SLOT
        [HttpPost]
        public IActionResult Book(int weekId, string name, string email)
        {
            var week = _context.TrainingWeeks
                .Include(w => w.Bookings)
                .FirstOrDefault(w => w.Id == weekId);

            if (week == null)
                return RedirectToAction("Index");

            if (week.Bookings.Count >= week.MaxSeats)
            {
                TempData["Error"] = "This week is fully booked.";
                return RedirectToAction("Index");
            }

            var booking = new TrainingBooking
            {
                StudentName = name,
                Email = email,
                TrainingWeekId = weekId
            };
            if (_context.TrainingBookings.Any(b =>
                b.Email == email && b.TrainingWeekId == weekId))
            {
                TempData["Error"] = "You already booked this week.";
                return RedirectToAction("Index");
            }
            else
            {
                _context.TrainingBookings.Add(booking);
                _context.SaveChanges();

                TempData["Success"] = "Your slot has been booked successfully!";
                return RedirectToAction("Index");
            }
        }
    }

}
