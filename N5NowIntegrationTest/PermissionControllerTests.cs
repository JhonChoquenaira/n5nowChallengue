using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using N5NowChallengue.BusinessService.DTO;
using N5NowChallengue.DataService.Models;
using N5NowChallengue.ErrorHandler;
using Xunit;

namespace N5NowIntegrationTest
{
    public class PermissionControllerTests : IntegrationTest
    {
        [Fact]
        public async Task GetByIdPermissionReturnEmptyResponse()
        {
            //Act
            var response = await _httpClient.GetAsync("https://localhost:5001/api/Permission?id=1");
            var contentStream = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
            var permissionDto = JsonSerializer.Deserialize<BaseResult<PermissionDto>>(contentStream, options);
            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            permissionDto.Data.Should().BeNull();
        }

        [Fact]
        public async Task GetAllReturnEmptyResponse()
        {
            //Act
            var response = await _httpClient.GetAsync("https://localhost:5001/api/Permission/GetAll");
            var contentStream = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
            var listOfPermissions = JsonSerializer.Deserialize<BaseResult<List<PermissionDto>>>(contentStream, options);
            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            listOfPermissions.Data.Should().BeEmpty();
        }

        [Fact]
        public async Task GetByIdPermissionReturnNotEmptyResponse()
        {
            //Arrange
            var permissionDtoRequest = new PermissionDto()
                { PermissionCode = "RTL43", PermissionName = "Login", Method = "POST", Status = true };
            var body = JsonSerializer.Serialize(permissionDtoRequest);
            var content = new StringContent(body, Encoding.UTF8, "application/json");
            var create = await _httpClient.PostAsync("https://localhost:5001/api/Permission", content);

            //Act
            var response = await _httpClient.GetAsync("https://localhost:5001/api/Permission?id=1");
            var contentStream = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
            var permissionDto = JsonSerializer.Deserialize<BaseResult<PermissionDto>>(contentStream, options);
            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            permissionDto.Data.Should().NotBeNull();
        }

        [Fact]
        public async Task GetAllReturnNotEmptyResponse()
        {
            //Arrange
            var permissionDtoRequest = new PermissionDto()
                { PermissionCode = "RTL43", PermissionName = "Login", Method = "POST", Status = true };
            var body = JsonSerializer.Serialize(permissionDtoRequest);
            var content = new StringContent(body, Encoding.UTF8, "application/json");
            var create = await _httpClient.PostAsync("https://localhost:5001/api/Permission", content);

            //Act
            var response = await _httpClient.GetAsync("https://localhost:5001/api/Permission/GetAll");
            var contentStream = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
            var listOfPermissions = JsonSerializer.Deserialize<BaseResult<List<PermissionDto>>>(contentStream, options);
            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            listOfPermissions.Data.Should().NotBeEmpty().And.NotBeNull();
        }

        [Fact]
        public async Task UpdatePermissionSucceeds()
        {
            //Arrange
            var permissionDtoRequest = new PermissionDto()
                { PermissionCode = "RTL43", PermissionName = "Login", Method = "POST", Status = true };
            var body = JsonSerializer.Serialize(permissionDtoRequest);
            var content = new StringContent(body, Encoding.UTF8, "application/json");
            var create = await _httpClient.PostAsync("https://localhost:5001/api/Permission", content);

            //Act
            var permissionRequest = new Permission()
            {
                PermissionId = 1, PermissionCode = "RTL43", PermissionName = "Login", Method = "POST", Status = true
            };
            var bodyPermission = JsonSerializer.Serialize(permissionRequest);
            var contentUpdatePermission = new StringContent(bodyPermission, Encoding.UTF8, "application/json");
            
            var update = await _httpClient.PutAsync("https://localhost:5001/api/Permission", contentUpdatePermission);
            var contentStream = await update.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
            var baseResult = JsonSerializer.Deserialize<BaseResult<MessageDto>>(contentStream, options);

            //Asserts
            update.StatusCode.Should().Be(HttpStatusCode.OK);
            string expected = "The permission was updated successfully";
            baseResult.Data.message.Should().Be(expected);

        }
    }
}