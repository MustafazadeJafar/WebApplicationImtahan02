namespace WebApplication4.Extensions;


public static class FileExtensions
{
    public static string WebRootPath { get; set; } 

    public static bool isCorrectType(this IFormFile file, string type = "image")
    {
        return file.ContentType.Contains(type);
    }
    public static async Task<string> SaveAsync(this IFormFile file, string path = "datas", string customName = "")
    {
        string filename, filepath;
        
        if (!String.IsNullOrWhiteSpace(customName)) filename = Path.Combine(path, customName);
        else
        {
            filename = Path.GetFileNameWithoutExtension(file.FileName).Length > 31 ? 
                file.FileName.Substring(0, 31) + Path.GetExtension(file.FileName) : file.FileName;
            filename = Path.Combine(path, Path.GetRandomFileName() + filename);
        }

        filepath = Path.Combine(WebRootPath, filename);
        if (File.Exists(filepath)) File.Delete(filepath);

        using (FileStream fs = File.Create(filepath))
        {
            await file.CopyToAsync(fs);
        }

        return filename;
    }
}
