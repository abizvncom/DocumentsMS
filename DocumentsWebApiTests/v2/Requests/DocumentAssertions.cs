using DocumentsWebApi.Models;
using DocumentsWebApiTests.v2.Responses;
using FluentAssertions;

namespace DocumentsWebApiTests.v2.Requests
{
    internal static class DocumentAssertions
    {
        public static void BeCreatedFrom(this DocumentResponse response, NewDocumentRequest request)
        {
            response.Should().NotBeNull();
            response.Id.Should().BeGreaterThan(0);
            response.Title.Should().Be(request.Title);
            response.UpdatedAt.Should().BeBefore(DateTime.Now);
        }

        public static void BeSerialisedFrom(this DocumentResponse response, Document entity)
        {
            response.Should().NotBeNull();
            response.Id.Should().Be(entity.Id);
            response.Title.Should().Be(entity.Title);
            response.UpdatedAt.Should().BeCloseTo(entity.UpdatedAt, TimeSpan.FromSeconds(1));
        }

        public static void BeSerialisedFrom(this IList<DocumentResponse> responses, IList<Document> entities)
        {
            responses.Count.Should().Be(entities.Count);
            for (int index = 0; index < responses.Count; index++)
            {
                responses[index].BeSerialisedFrom(entities[index]);
            }
        }
    }
}
