using NUnit.Framework;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using backend;
using Microsoft.VisualStudio.TestPlatform.TestHost;


namespace backend.Tests
{
    [TestFixture]
    public class AuthControllerTests
    {
        private HttpClient _client;
        private WebApplicationFactory<Program> _factory;

        [SetUp]
        public void SetUp()
        {
            // Initialize the WebApplicationFactory and HttpClient
            _factory = new WebApplicationFactory<Program>();
            _client = _factory.CreateClient();
        }

        [Test]
        public async Task Login_ReturnsSuccess_WithValidCredentials()
        {
            // Arrange
            var loginRequest = new
            {
                Email = "test@floorspaces.io",
                Password = "test"
            };
            var content = new StringContent(JsonConvert.SerializeObject(loginRequest), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/Auth/Login", content);

            // Assert
            Assert.That(response.IsSuccessStatusCode, Is.True);
        }

        [Test]
        public async Task Register_ReturnsSuccess_WithValidParameters()
        {
            // Arrange
            var timestamp = DateTime.Now.ToString("yyyyMMddHHmmssffff");
            var email = $"testuser_{timestamp}@example.com";

            var registerRequest = new
            {
                Email = email,
                Password = "test"
            };
            var content = new StringContent(JsonConvert.SerializeObject(registerRequest), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/Auth/Register", content);

            // Assert
            Assert.That(response.IsSuccessStatusCode, Is.True);
        }

        [TearDown]
        public void TearDown()
        {
            _client?.Dispose();
            _factory?.Dispose();    
        }
    }
}