using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Models;
using Companies.Models;

[ApiController]
[Route("api/[controller]")]
public class CompanyController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public CompanyController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    private Supabase.Client InitializeSupabaseClient()
    {
        var supabaseUrl = _configuration["Supabase:Url"] ?? throw new InvalidOperationException("Supabase URL is not configured.");
        var supabaseKey = _configuration["Supabase:Key"] ?? throw new InvalidOperationException("Supabase Key is not configured.");
        var options = new Supabase.SupabaseOptions { AutoConnectRealtime = true };
        var client = new Supabase.Client(supabaseUrl, supabaseKey, options);
        client.InitializeAsync().Wait();
        return client;
    }

    [HttpPost("RegisterCompany/{name}")]
    public async Task<IActionResult> RegisterCompany(string name)
    {
        var client = InitializeSupabaseClient();
        var searchResult = await client.From<companies>()
                                       .Where(x => x.name == name)
                                       .Get();

        if (searchResult.Models is not null && searchResult.Models.Any())
        {
            return Problem("Company already exists!");
        }

        var model = new companies { name = name };
        var insertResult = await client.From<companies>().Insert(model);

        if (insertResult.Models is null || !insertResult.Models.Any())
        {
            return Problem("Failed to register company.");
        }
        return Ok("Company registered successfully.");
    }

    // Input: Company Name
    // Output: Company UUID
    [HttpGet("GetCompanyUUID/{name}")]
    public async Task<IActionResult> GetCompanyUUID(string name)
    {
        var client = InitializeSupabaseClient();
        var searchResult = await client.From<companies>()
                                       .Where(x => x.name == name)
                                       .Get();

        if (searchResult.Models is null || !searchResult.Models.Any())
        {
            return NotFound("Company not found.");
        }
        return Ok(new { Id = searchResult.Models.FirstOrDefault()?.id });
    }
}