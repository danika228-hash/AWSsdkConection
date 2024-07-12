using MinioQuickstar.Dto;

namespace MinioQuickstar.Services;

public interface IMinioServices
{
    public string GetFile(string key);
    public Task DeleteFileAsync(string key);
    public Task<string> UploadFilesAsync(IFormFile formFile);
    public Task UpdateFileAsync(UpdateDto updateDto);
}
