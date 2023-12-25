using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WebApplication4.Areas.admin.ViewModels;
using WebApplication4.Contexts;
using WebApplication4.Extensions;
using WebApplication4.Models;
using WebApplication4.ViewModels;

namespace WebApplication4.Areas.admin.Controllers;

[Area("admin")]
[Authorize(Roles = "admin,superAdmin")]
public class BannerController : Controller
{
    Corona01DbContext _db { get; }

    public BannerController(Corona01DbContext db)
    {
        this._db = db;
    }

    // GET: BannerController
    [AllowAnonymous]
    public async Task<ActionResult> Index()
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

    // GET: BannerController/Create
    public ActionResult Create()
    {

        return Ok();
    }

    // GET: BannerController/Edit/5
    public async Task<ActionResult> Edit()
    {
        Banner banner = await this._db.Banners.FindAsync(1);

        return View(new BannerEditVM
        {
            Title = banner.Title,
            Describtion = banner.Describtion,
            FrontImagePath = banner.FrontImagePath,
            BackImagePath = banner.BackImagePath,
        });
    }

    // POST: BannerController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(BannerEditVM vm)
    {
        Banner banner = await this._db.Banners.FindAsync(1);

        if (vm.FrontImageFile != null) 
        {
            if (!vm.FrontImageFile.isCorrectType())
            {
                ModelState.AddModelError("FrontImageFile", "");
            }
            
            if (ModelState.IsValid) 
            {

                banner.FrontImagePath = await vm.FrontImageFile.SaveAsync(path: Path.Combine("blog", "img"), customName: "header-img.jpg");
            }
        }
        if (vm.BackImageFile != null)
        {
            if (!vm.BackImageFile.isCorrectType())
            {
                ModelState.AddModelError("BackImageFile", "");
            }

            if (ModelState.IsValid)
            {
                banner.BackImagePath = await vm.BackImageFile.SaveAsync(path: Path.Combine("blog", "img"), customName: "top-stories-header.jpg");
            }
        }
        if (!ModelState.IsValid)
        {
            return View(vm);
        }

        banner.Title = vm.Title;
        banner.Describtion = vm.Describtion;

        this._db.Banners.Update(banner);
        this._db.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
}
