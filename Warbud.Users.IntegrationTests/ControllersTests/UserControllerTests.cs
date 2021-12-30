using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Warbud.Users.Api;
using Warbud.Users.Application.Commands.User;
using Warbud.Users.Application.DTO;
using Warbud.Users.Domain.Constants;
using Warbud.Users.Domain.Entities;
using Warbud.Users.IntegrationTests.Configuration;
using Warbud.Users.IntegrationTests.Configuration.User;
using Xunit;

namespace Warbud.Users.IntegrationTests.ControllersTests
{
    public class UserControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        public UserControllerTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory
                .WithWebHostBuilder(builder =>
                    builder.ConfigureServices(services =>
                    {
                        // var readDbContext =
                        //     services.SingleOrDefault(x => x.ServiceType == typeof(DbContextOptions<ReadDbContext>));
                        // var writeDbContext = services.SingleOrDefault(x =>
                        //     x.ServiceType == typeof(DbContextOptions<WriteDbContext>));
                        //
                        // if (readDbContext != null) services.Remove(readDbContext);
                        // if (writeDbContext != null) services.Remove(writeDbContext);
                        //
                        // var root = new InMemoryDatabaseRoot();
                        // services.AddDbContext<ReadDbContext>(options => options.UseInMemoryDatabase("db", InMemoryDatabaseRoot));
                        // services.AddDbContext<WriteDbContext>(options => options.UseInMemoryDatabase("db", InMemoryDatabaseRoot));
                        
                        // Fake policy to pass all authorization checks
                        services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();
                        services.AddMvc(options => options.Filters.Add(new FakeUserFilter()));
                    })
                );
            _client = _factory.CreateClient();
        }
        private readonly HttpClient _client;
        private readonly WebApplicationFactory<Startup> _factory;
        private const string Id = "76f02329-75e0-4a42-9bff-8a593187da64";
        private static readonly User User = new (new Guid(Id), "random@random.com", "Password123", "Random", "Random");
       
        [Fact]
        public async Task GetUser_ShouldReturnUser()
        {
            // Arrange
            await _factory.SeedUser(User);

            // Act
            var response = await _client.GetAsync($"/User/GetUser/{Id}");
            
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        
        [Fact]
        public async Task GetUser_ShouldReturnNotFound()
        {
            // Arrange
            await _factory.ClearUsers();
            
            // Act
            var response = await _client.GetAsync($"/User/GetUser/{Id}");
            
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
        

        //TODO: Nick Chapsas dummy data youtube video
        [Theory]
        [InlineData("cezary.kepka@warbud.pl", "Haslo123", "Cezary", "Kępka", "Haslo123")]
        [InlineData("adrianfranczak@gmail.com", "Haslo123", "Adrian", "Franczak", "Haslo123")]
        public async Task AddUserAsync_ShouldAddUser(string email,
            string password,
            string firstName,
            string lastName,
            string confirmPassword)
        {
            // Arrange
            await _factory.ClearUsers();
            var addUser = new AddUser(email, password, firstName, lastName, confirmPassword);
            
            // Act
            var response = await _client.PostAsync("User/AddUser/", addUser.ToJsonContent());

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task RemoveUser_ShouldReturnNotFound()
        {
            await _factory.ClearUsers();
            
            // Act
            var response = await _client.DeleteAsync($"/User/RemoveUser/{Id}");
            
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
        
        [Fact]
        public async Task RemoveUser_ShouldReturnNoContent()
        {
            // Arrange
            await _factory.SeedUser(User);

            // Act
            var response = await _client.DeleteAsync($"/User/RemoveUser/{Id}");
            
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Theory]
        [InlineData("Adrian", "Franczak")]
        [InlineData("Cezary", "Kępka", "cezary.kepka@warbud.pl")]
        public async Task PatchUser_ShouldReturnNoContent(string? firstName = null, string? lastName = null, string? email = null)
        {
            // Arrange
            await _factory.SeedUser(User);
            var patchDoc = new JsonPatchDocument<PatchUserDto>();
            if (firstName is not null) patchDoc.Replace(x => x.FirstName, firstName);
            if (lastName is not null)patchDoc.Replace(x => x.LastName, lastName);
            if (email is not null)patchDoc.Replace(x => x.Email, email);

            // Act
            var response = await _client.PatchAsync($"/User/PatchUser/{Id}", patchDoc.ToJsonPatchContent());
            
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
        [Theory]
        [InlineData("cezary.kepka@warbud.pl", "Cezary")]
        [InlineData( "Franczak", "franczak@gmail.com")]
        [InlineData]
        public async Task PatchUser_ShouldReturnBadRequest_ForInvalidData(string? firstName = null, string? lastName = null, string? email = null)
        {
            // Arrange
            await _factory.SeedUser(User);
            var patchDoc = new JsonPatchDocument<PatchUserDto>();
            if (firstName is not null) patchDoc.Replace(x => x.FirstName, firstName);
            if (lastName is not null)patchDoc.Replace(x => x.LastName, lastName);
            if (email is not null)patchDoc.Replace(x => x.Email, email);

            // Act
            var response = await _client.PatchAsync($"/User/PatchUser/{Id}", patchDoc.ToJsonPatchContent());

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task UpdateUserRole_ShouldReturnNoContent()
        {
            await _factory.SeedUser(User);
            var command = new UpdateUserRole(User.Id, Role.BasicUser);
            // Act
            var response = await _client.PutAsync("/User/UpdateUserRole/", command.ToJsonPatchContent());

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
            
        [Fact]
        public async Task ChangePassword_ShouldReturnNoContent()
        {
            // Arrange
            await _factory.ClearUsers();
            await _factory.SeedUser(User);

            // Act
            var newPassword = "Hello123";
            var command = new ChangePassword(User.Id, User.Password, newPassword, newPassword);
            var response = await _client.PutAsync("/User/ChangePassword/", command.ToJsonPatchContent());

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}