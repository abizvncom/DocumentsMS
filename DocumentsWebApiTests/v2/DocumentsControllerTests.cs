using DocumentsWebApiTests.Requests;
using DocumentsWebApiTests.Requests.v2;
using DocumentsWebApiTests.Responses.v2;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;

namespace DocumentsWebApiTests.v2
{
    public class DocumentsControllerTests : BaseFunctionalTest, IClassFixture<CustomWebApiFactory>
    {
        public DocumentsControllerTests(CustomWebApiFactory factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task GetDocuments_ReturnsOk_Empty()
        {
            // Arrange
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/v2/Documents");

            // Act
            var response = await Client.SendAsync(request);
            var documents = await response.Content.ReadFromJsonAsync<PaginatedListResponse<DocumentResponse>>();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            documents.Should().NotBeNull();
            documents.Items.Should().BeNullOrEmpty();
        }

        [Fact]
        public async Task CreateDocument_ReturnsCreated()
        {
            // Arrange
            var request = new NewDocumentRequest("Document 1");

            // Act
            var response = await Client.PostAsync("/api/v2/Documents", request.ToJsonHttpContent());

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
         
            var document = await response.Content.ReadFromJsonAsync<DocumentResponse>();
            document.Should().NotBeNull();
            document.BeCreatedFrom(request);
        }

        [Fact]
        public async Task GetDocument_ReturnsNotFound()
        {
            // Arrange
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/v2/Documents/0");

            // Act
            var response = await Client.SendAsync(request);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
