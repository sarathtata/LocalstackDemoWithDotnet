using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Mvc;

namespace LocalstackDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class S3Controller : Controller
    {
        private readonly IAmazonS3 s3Client;
        public S3Controller(IAmazonS3 s3Client)
        {
            this.s3Client = s3Client;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var request = new ListObjectsRequest
            {
                BucketName = "mybucket"
            };

            var response = await s3Client.ListObjectsAsync(request);
            return Ok(response.S3Objects);
        }
    }
}
