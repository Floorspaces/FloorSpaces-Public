using Microsoft.AspNetCore.Mvc;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.Runtime;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Numerics;

[ApiController]
[Route("api/[controller]")]
public class FloorspacesController : ControllerBase
{
    private readonly AmazonS3Client _s3Client;

    public FloorspacesController()
    {
        _s3Client = CreateS3Client();
    }

    private AmazonS3Client CreateS3Client()
    {
        var S3Config = new AmazonS3Config()
        {
            ServiceURL = "<Removed>"
        };
        
        var accessKeyID = "<Removed>";
        var accessSecret = "<Removed>";
        
        // var accessKeyID = Environment.GetEnvironmentVariable("AWS_ACCESS_KEY_ID");
        // var accessSecret = Environment.GetEnvironmentVariable("AWS_SECRET_ACCESS_KEY");
        
        var credentials = new BasicAWSCredentials(accessKeyID, accessSecret);
        return new AmazonS3Client(credentials, S3Config);
    }

    private async Task<IActionResult> FetchS3Content(string key)
    {
        var requestDownload = new GetObjectRequest
        {
            BucketName = "shapescripts-bucket-open",
            Key = key
        };

        using (var response = await _s3Client.GetObjectAsync(requestDownload))
        using (var responseStream = response.ResponseStream)
        using (var memoryStream = new MemoryStream())
        {
            await responseStream.CopyToAsync(memoryStream);
            byte[] content = memoryStream.ToArray();
            return File(content, "application/octet-stream"); // Return the content as a byte array
        }
    }

    private async void UploadS3Content(string key, string data)
    {
        var requestUpload = new PutObjectRequest
        {
            BucketName = "shapescripts-bucket-open",
            Key = key,
            ContentBody = data,
        };

        var response = await _s3Client.PutObjectAsync(requestUpload);
    }

    private async Task<string> GetS3Content(string key)
    {
        var requestDownload = new GetObjectRequest
        {
            BucketName = "shapescripts-bucket-open",
            Key = key
        };

        using (var response = await _s3Client.GetObjectAsync(requestDownload))
        using (var responseStream = response.ResponseStream)
        using (var reader = new StreamReader(responseStream))
        {
            var text = reader.ReadToEnd();
            return text;
        }
    }

    [HttpGet("doesFileExist/{key}")]
    public async Task<bool> DoesFileExist(string key)
    {
        var request = new ListObjectsRequest
        {
            BucketName = "shapescripts-bucket-open",
            Prefix = key,
            MaxKeys = 1
        };

        var response = await _s3Client.ListObjectsAsync(request, CancellationToken.None);

        return response.S3Objects.Any();
    }

    [HttpGet("SendLiveMessage/{UUID}/{RoomID}/{Message}")]
    public IActionResult SendLiveMessage(int UUID, string RoomID, string Message)
    {
        string Key = $"{UUID}:{RoomID}";
        RoomMessages.Messages[Key] = Message;
        return Ok();
    }

    [HttpGet("GetLiveMessage/{UUID}/{RoomID}")]
    public IActionResult GetLiveMessage(int UUID, string RoomID)
    {
        string Key = $"{UUID}:{RoomID}";
        if (RoomMessages.Messages.TryGetValue(Key, out string result))
        {
            return Content(result);
        }
        return Content("");
    }

    /*
    [HttpGet("GetPoints/{UUID}")]
    public IActionResult GetPoints(int UUID)
    {
        return Ok(InstancePoints.InstancePointsDict[UUID]);
    }
    */

    /*
    [HttpGet("SendPoints/{UUID}/{ListPointArray}")]
    public IActionResult SendPoints(int UUID, string ListPointArray)
    {
        InstancePoints.InstancePointsDict[UUID] = ListPointArray;
        return Ok(InstancePoints.InstancePointsDict[UUID]);
    }
    */

    [HttpGet("SavePoints/{UUID}/{ListPointArray}")]
    public IActionResult SavePoints(int UUID, string ListPointArray)
    {
        UploadS3Content(UUID + ".txt", ListPointArray);
        return Ok();
    }

    [HttpGet("DownloadPoints/{UUID}")]
    public IActionResult DownloadPoints(int UUID)
    {
        Response.Headers.Add("Access-Control-Allow-Origin", "*");
        var x = GetS3Content(UUID.ToString() + ".txt");
        return Ok(x.Result);
    }

    [HttpDelete("DeletePoints/{UUID}")]
    public async Task<IActionResult> DeletePoints(int UUID)
    {
        try
        {
            var deleteRequest = new DeleteObjectRequest
            {
                BucketName = "shapescripts-bucket-open",
                Key = UUID.ToString() + ".txt"
            };

            await _s3Client.DeleteObjectAsync(deleteRequest);
            return Ok("File deleted successfully.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }
}

public static class RoomMessages
{
    public static Dictionary<string, string> Messages = new Dictionary<string, string>();
}

/*
public static class InstancePoints
{
    //public static Dictionary<int, List<Vector3>> InstancePointsDict = new Dictionary<int, List<Vector3>>();
    public static Dictionary<int, string> InstancePointsDict = new Dictionary<int, string>();
}
*/