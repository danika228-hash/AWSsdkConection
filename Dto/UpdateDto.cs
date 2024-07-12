using System.ComponentModel.DataAnnotations;

namespace MinioQuickstar.Dto;

public class UpdateDto
{
    [Required]
    public IFormFile File { get; set; } = null!;

    [Required,MinLength(3)]
    public string FileName { get; set; } = string.Empty;
}
