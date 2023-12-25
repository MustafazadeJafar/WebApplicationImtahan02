namespace WebApplication4.Areas.admin.ViewModels;

public class BannerEditVM
{
    public string Title { get; set; }
    public string Describtion { get; set; }

    public IFormFile? FrontImageFile { get; set; }
    public IFormFile? BackImageFile { get; set; }
    public string? FrontImagePath { get; set; }
    public string? BackImagePath { get; set; }
}
