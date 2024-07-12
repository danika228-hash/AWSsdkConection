using Amazon.S3;
using Amazon.S3.Model;
using MinioQuickstar.Dto;

namespace MinioQuickstar.Services;

public class MinioServices(IAmazonS3 client, string bucketName) : IMinioServices
{
    public async Task<string> UploadFilesAsync(IFormFile formFile)
    {
        await using (var stream = new MemoryStream())
        {
            formFile.CopyTo(stream);

            var uploadRequest = new PutObjectRequest
            {
                BucketName = bucketName,
                Key = formFile.FileName,
                InputStream = stream,
                ContentType = formFile.ContentType
            };

            await client.PutObjectAsync(uploadRequest);

            var getUrlRequest = new GetPreSignedUrlRequest
            {
                BucketName = bucketName,
                Key = formFile.FileName,
                Expires = DateTime.UtcNow.AddMinutes(1)
            };

            return client.GetPreSignedURL(getUrlRequest);
        }
    }

    public string GetFile(string key)
    {
        var getUrlRequest = new GetPreSignedUrlRequest
        {
            BucketName = bucketName,
            Key = key,
            Expires = DateTime.UtcNow.AddMinutes(1)
        };

        return client.GetPreSignedURL(getUrlRequest);
    }

    public async Task DeleteFileAsync(string key)
    {
        var deleteRequest = new DeleteObjectRequest
        {
            BucketName = bucketName,
            Key = key,
        };

        await client.DeleteObjectAsync(deleteRequest);
    }

    public async Task UpdateFileAsync(UpdateDto updateDto)
    {
        using (var stream = new MemoryStream())
        {
            updateDto.File.CopyTo(stream);

            var request = new PutObjectRequest
            {
                BucketName = bucketName,
                Key = updateDto.FileName,
                InputStream = stream,
                ContentType = updateDto.File.ContentType
            };

            await client.PutObjectAsync(request);
        }
    }
}