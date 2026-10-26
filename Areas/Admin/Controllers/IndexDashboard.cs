using cinemasecondtask.Data;
using cinemasecondtask.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace cinemasecondtask.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class IndexDashboard : Controller
    {
        ApplicationDbContext _context ;

        public IndexDashboard(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var model = new DashboardVM
            {
                MoviesCount = _context.Movies.Count(),
                UsersCount = _context.Users.Count(),
                BookingsCount = _context.Bookin.Count(),
                Revenue = _context.Bookin.Sum(x => x.TotalPrice)
            };

            return View(model);
            
        }
    }
}
