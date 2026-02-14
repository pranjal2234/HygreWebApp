using HygreWebApp.Data;
using HygreWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace HygreWebApp.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class AdminPaymentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminPaymentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // EDIT / CREATE
        public IActionResult Index()
        {
            var details = _context.PaymentDetails.FirstOrDefault();

            if (details == null)
            {
                details = new PaymentDetail();
                //_context.PaymentDetails.Add(details);
                //_context.SaveChanges();
            }

            return View(details);
        }

        [HttpPost]
        public IActionResult Index(PaymentDetail model)
        {
            var details = _context.PaymentDetails.FirstOrDefault();

            if (details == null)
            {
                // CREATE new record
                details = new PaymentDetail();
                _context.PaymentDetails.Add(details);
            }

            // UPDATE values
            details.AccountName = model.AccountName;
            details.BankName = model.BankName;
            details.AccountNumber = model.AccountNumber;
            details.IFSC = model.IFSC;
            details.Branch = model.Branch;
            details.UpiId = model.UpiId;
            details.AcceptedApps = model.AcceptedApps;
            details.UpdatedOn = DateTime.Now;

            _context.SaveChanges();

            TempData["Success"] = "Payment details saved successfully.";
            return RedirectToAction("Index");
        }
    }
}
