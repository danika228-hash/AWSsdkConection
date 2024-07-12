using Microsoft.AspNetCore.Mvc;
using MinioQuickstar.Services;
using MinioQuickstar.Dto;

namespace MinioQuickstar.Controllers;

[Route("api/files")]
[ApiController]
public class MinioController(IMinioServices minioServices) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> UploadFileAsync(IFormFile file)
    {
        var dowloadUrl = await minioServices.UploadFilesAsync(file);

        var response = new
        {
            Key = file.FileName,
            DownloadUrl = dowloadUrl,
        };

        return Ok(response);
    }

    [HttpGet("{key}")]
    public IActionResult GetFileAsync(string key)
    {
        var dowloadUrl = minioServices.GetFile(key);

        return Ok(dowloadUrl);
    }

    [HttpDelete("{key}")]
    public async Task<IActionResult> DeleteFileAsync(string key)
    {
        await minioServices.DeleteFileAsync(key);

        return Ok();
    }
    [HttpPut("{key}")]
    public async Task<IActionResult> UpdateFileAsync(UpdateDto updateDto)
    {
        await minioServices.UpdateFileAsync(updateDto);

        return Ok();
    }
}
