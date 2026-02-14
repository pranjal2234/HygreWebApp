using HygreWebApp.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HygreWebApp.Controllers
{
    //[Authorize(Roles = "Admin")] // enable later
    public class AdminTrainingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminTrainingController(ApplicationDbContext context)
        {
            _context = context;
        }

        // VIEW ALL BOOKINGS (WEEK-WISE)
        public IActionResult Index()
        {
            var weeks = _context.TrainingWeeks
                .Include(w => w.Bookings)
                .OrderBy(w => w.WeekStartDate)
                .ToList();

            return View(weeks);
        }

        [HttpPost]
        public IActionResult CancelBooking(int bookingId)
        {
            var booking = _context.TrainingBookings
                .FirstOrDefault(b => b.Id == bookingId);

            if (booking == null)
                return RedirectToAction("Index");

            _context.TrainingBookings.Remove(booking);
            _context.SaveChanges();

            TempData["Success"] = "Booking cancelled successfully.";

            return RedirectToAction("Index");
        }

    }
}
