using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Models;

namespace WebApplication4.Contexts;

public class Corona01DbContext : IdentityDbContext<AppUser>
{
    public Corona01DbContext(DbContextOptions<Corona01DbContext> options) : base(options) { }

    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<Banner> Banners { get; set; }
}
