using Amazon.S3;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Register AWS S3 Service

var endpointUrl = builder.Configuration["AWS:Url"];
var useHttp = Convert.ToBoolean(builder.Configuration["AWS:UseHttp"]);
var forcePathStyle = Convert.ToBoolean(builder.Configuration["AWS:ForcePathStyle"]);
var awsAccessKeyId = "mykey";
var awsSecretAccessKey = "mycode";

builder.Services.AddSingleton<IAmazonS3>(provider =>
{
    var config = new AmazonS3Config
    {
        UseHttp = useHttp,
        ServiceURL = endpointUrl,
        ForcePathStyle = forcePathStyle
    };

    return new AmazonS3Client(awsAccessKeyId, awsSecretAccessKey, config);
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
