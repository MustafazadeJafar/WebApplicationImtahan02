using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication4.Models;

public class AppUser : IdentityUser
{
    [NotMapped]
    public override string Email { get => base.Email; set => base.Email = value; }
    [NotMapped]
    public override string PhoneNumber { get => base.PhoneNumber; set => base.PhoneNumber = value; }
}
