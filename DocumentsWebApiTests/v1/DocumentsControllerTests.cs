using FluentAssertions;
using System.Net;

namespace DocumentsWebApiTests.v1
{
    public class DocumentsControllerTests : BaseFunctionalTest, IClassFixture<CustomWebApiFactory>
    {
        public DocumentsControllerTests(CustomWebApiFactory factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task GetDocuments_ReturnsOk()
        {
            // Arrange
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/v1/Documents");

            // Act
            var response = await Client.SendAsync(request);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            
            var content = await response.Content.ReadAsStringAsync();
            content.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task GetDocument_ReturnsOk()
        {
            // Arrange
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/v1/Documents/1");

            // Act
            var response = await Client.SendAsync(request);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await response.Content.ReadAsStringAsync();
            content.Should().Be("Document 1");
        }
    }
}
