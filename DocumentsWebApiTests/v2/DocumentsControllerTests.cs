using DocumentsWebApiTests.Common;
using DocumentsWebApiTests.v2.Fakers;
using DocumentsWebApiTests.v2.Requests;
using DocumentsWebApiTests.v2.Responses;
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
        public async Task GetDocuments_ReturnsOk_WithAPageOfDocuments()
        {
            // Arrange
            var pageNumber = 1;
            var pageSize = 3;
            var documentsCount = 10;
            var request = new HttpRequestMessage(HttpMethod.Get, $"/api/v2/Documents?pn={pageNumber}&ps={pageSize}");
            var documents = await Factory.CreateSampleDocuments(documentsCount);

            // Act
            var response = await Client.SendAsync(request);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var pagedDocuments = await response.Content.ReadFromJsonAsync<PaginatedListResponse<DocumentResponse>>();
            pagedDocuments.Should().NotBeNull();

            var expectedDocuments = documents.OrderByDescending(d => d.UpdatedAt).TakePage(pageNumber, pageSize);
            pagedDocuments.Items.BeSerialisedFrom(expectedDocuments);

            pagedDocuments.TotalCount.Should().Be(documentsCount);
        }

        [Fact]
        public async Task CreateDocument_ReturnsCreated_WhenDataIsValid()
        {
            // Arrange
            var request = new NewDocumentRequestFaker().Generate(1).First();

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

        [Fact]
        public async Task GetDocument_ReturnsOk_WhenExists()
        {
            // Arrange
            var document = await Factory.CreateSampleDocument();
            var request = new HttpRequestMessage(HttpMethod.Get, $"/api/v2/Documents/{document.Id}");

            // Act
            var response = await Client.SendAsync(request);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var documentResponse = await response.Content.ReadFromJsonAsync<DocumentResponse>();
            documentResponse.Should().NotBeNull();
            documentResponse.BeSerialisedFrom(document);
        }
    }
}
