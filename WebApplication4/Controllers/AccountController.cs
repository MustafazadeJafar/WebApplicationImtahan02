using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication4.Contexts;
using WebApplication4.Extensions;
using WebApplication4.Models;
using WebApplication4.ViewModels;

namespace WebApplication4.Controllers;

public class AccountController : Controller
{
    Corona01DbContext _db { get; }
    UserManager<AppUser> _userManager { get; }
    SignInManager<AppUser> _signInManager { get; }
    RoleManager<IdentityRole> _roleManager { get; }

    public AccountController(Corona01DbContext db,
        SignInManager<AppUser> signInManager,
        UserManager<AppUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        this._db = db;
        this._signInManager = signInManager;
        this._userManager = userManager;
        this._roleManager = roleManager;
    }

    public ActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Register(RegisterVM vm)
    {
        if (vm.password != vm.password2) ModelState.AddModelError("", "not same passwords");
        if (ModelState.IsValid)
        {
            AppUser user = new AppUser
            {
                UserName = vm.username,
            };

            var result = await _userManager.CreateAsync(user, vm.password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                await this._userManager.AddToRoleAsync(user, AuthExtensions.Roles.user.ToString());
                return RedirectToAction("index", "Home");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            } 
        }
        return View(vm);
    }

    public async Task<ActionResult> Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Login(LoginVM vm, string? ReturnUrl)
    {
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(vm.username, vm.password, true, false);

            if (result.Succeeded)
            {
                ReturnUrl ??= Url.Action(action: "Index", controller: "Home");
                return Redirect(ReturnUrl);
            }

            ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
        }
        return View(vm);
    }

    public ActionResult Logout()
    {
        this._signInManager.SignOutAsync();
        return RedirectToAction(nameof(Login));
    }

    public async Task<ActionResult> CreateRoles()
    {
        foreach (var item in Enum.GetValues<AuthExtensions.Roles>())
        {
            if (await this._roleManager.RoleExistsAsync(item.ToString()) == false) 
            {
                await this._roleManager.CreateAsync(new IdentityRole
                {
                    Name = item.ToString(),
                });
            }
        }
        return Ok();
    }
}
