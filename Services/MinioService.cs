using Minio;
using Minio.DataModel.Args;

namespace forms.Services;

public class MinioService
{
    private readonly IMinioClient _minioClient;
    private readonly string _endpoint;
    private readonly string _bucketName;

    public MinioService(IConfiguration configuration)
    {
        _endpoint = configuration["Minio:Endpoint"]!;
        _bucketName = configuration["Minio:BucketName"]!;

        _minioClient = new MinioClient()
            .WithEndpoint(_endpoint)
            .WithCredentials(configuration["Minio:AccessKey"], configuration["Minio:SecretKey"])
            .WithSSL(true)
            .Build();
    }

    public async Task<string> UploadImageAsync(Stream fileStream, string fileName, string contentType)
    {
        var objectName = $"images/{Guid.NewGuid()}_{fileName}";

        var putObjectArgs = new PutObjectArgs()
            .WithBucket(_bucketName)
            .WithObject(objectName)
            .WithStreamData(fileStream)
            .WithObjectSize(fileStream.Length)
            .WithContentType(contentType);

        await _minioClient.PutObjectAsync(putObjectArgs);

        return $"https://{_endpoint}/{_bucketName}/{objectName}";
    }
}
