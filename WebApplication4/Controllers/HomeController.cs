using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication4.Contexts;
using WebApplication4.Models;
using WebApplication4.ViewModels;

namespace WebApplication4.Controllers
{
    public class HomeController : Controller
    {
        Corona01DbContext _db { get; }

        public HomeController(Corona01DbContext db)
        {
            this._db = db;
        }

        public async Task<IActionResult> Index()
        {
            Banner banner = await this._db.Banners.FindAsync(1);

            return View(new BannerVM
            {
                Title = banner.Title,
                Describtion = banner.Describtion,
                FrontImagePath = banner.FrontImagePath,
                BackImagePath = banner.BackImagePath,
            });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}