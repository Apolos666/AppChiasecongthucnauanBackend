using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;

namespace AppChiaSeCongThucNauAnBackend.Services;

public class S3Service
{
    private readonly IAmazonS3 _s3Client;
    private readonly string _bucketName;

    public S3Service(IAmazonS3 s3Client, IConfiguration configuration)
    {
        var awsOptions = configuration.GetAWSOptions();
        awsOptions.Credentials = new BasicAWSCredentials(
            configuration["AWS:AccessKeyId"],
            configuration["AWS:SecretAccessKey"]);
        _s3Client = awsOptions.CreateServiceClient<IAmazonS3>();
        _bucketName = configuration["AWS:BucketName"]!;
    }

    public async Task<string> UploadFileAsync(IFormFile file)
    {
        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);

        var request = new PutObjectRequest
        {
            BucketName = _bucketName,
            Key = fileName,
            InputStream = memoryStream
        };

        await _s3Client.PutObjectAsync(request);

        return $"https://{_bucketName}.s3.amazonaws.com/{fileName}";
    }

    public async Task DeleteFileAsync(string fileUrl)
    {
        var fileName = Path.GetFileName(new Uri(fileUrl).LocalPath);
        var request = new DeleteObjectRequest
        {
            BucketName = _bucketName,
            Key = fileName
        };

        await _s3Client.DeleteObjectAsync(request);
    }
}

