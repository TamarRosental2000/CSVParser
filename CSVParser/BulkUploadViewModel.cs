using System.ComponentModel.DataAnnotations;

public class BulkUploadViewModel
{
    [Required]
    public IFormFile JobCsv { get; set; }
    [Required]
    public string UserId { get; set; }
}