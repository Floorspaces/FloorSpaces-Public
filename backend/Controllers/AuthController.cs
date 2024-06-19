using System;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Accounts.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IHttpClientFactory _httpClientFactory;

    public AuthController(IConfiguration configuration, IHttpClientFactory httpClientFactory)
    {
        _configuration = configuration;
        _httpClientFactory = httpClientFactory;
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

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        try
        {
            var supabase = InitializeSupabaseClient();
            var session = await supabase.Auth.SignIn(loginRequest.Email, loginRequest.Password);
            return Ok(session);
        }
        catch (Supabase.Gotrue.Exceptions.GotrueException ex)
        {
            // Log the exception details for internal tracking
            // Return a more generic error message to the client
            return BadRequest(new { message = "Login failed. Please check your credentials." });
        }
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] LoginRequest registerRequest)
    {
        try
        {
            var supabase = InitializeSupabaseClient();
            var session = await supabase.Auth.SignUp(registerRequest.Email, registerRequest.Password);
            return Ok(session);
        }
        catch (Supabase.Gotrue.Exceptions.GotrueException ex)
        {
            // Log the exception details for internal tracking
            // Return a more generic error message to the client
            return BadRequest(new { message = "SignUp failed. Invalid parameters or connection issue." });
        }
    }

    [HttpGet("Profile")]
    [Authorize]
    public async Task<IActionResult> GetUserProfile()
    {
        // Directly use the HttpContext to access the bearer token
        var authorizationHeader = HttpContext.Request.Headers["Authorization"].ToString();
        if (string.IsNullOrEmpty(authorizationHeader))
        {
            return Unauthorized("Authorization header is missing.");
        }

        var token = authorizationHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase)
                    ? authorizationHeader.Substring("Bearer ".Length).Trim()
                    : string.Empty;

        if (string.IsNullOrEmpty(token))
        {
            return Unauthorized("Bearer token is missing.");
        }

        Guid userId;
        try
        {
            userId = DecodeAccessToken(token);
            if (userId == Guid.Empty)
            {
                return Unauthorized("Invalid Access Token.");
            }
        }
        catch (Exception ex)
        {
            // Log the exception details for internal tracking
            // Consider how much information you want to return in the response for security reasons
            return Unauthorized($"Error decoding token: {ex.Message}");
        }

        try
        {
            var supabase = InitializeSupabaseClient();
            var response = await supabase.From<profiles>()
                                         .Where(x => x.user_id == userId)
                                         .Single(); 

            if (response == null)
            {
                return NotFound("User profile not found.");
            }

            return Ok(response);
        }
        catch (Exception ex)
        {
            // Log this exception as well
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    private static Guid DecodeAccessToken(string accessToken)
    {
        // Placeholder for actual JWT decoding logic
        // This would involve using a library like System.IdentityModel.Tokens.Jwt
        // to parse the token and extract the claim for the user ID
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(accessToken);
        var userIdClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "id")?.Value;

        if (userIdClaim != null && Guid.TryParse(userIdClaim, out var userId))
        {
            return userId;
        }

        return Guid.Empty;
    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}