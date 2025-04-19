using DocumentsWebApiTests.Responses.v2;
using FluentAssertions;

namespace DocumentsWebApiTests.Requests.v2
{
    internal static class DocumentAssertions
    {
        public static void BeCreatedFrom(this DocumentResponse document, NewDocumentRequest request)
        {
            document.Should().NotBeNull();
            document.Id.Should().BeGreaterThan(0);
            document.Title.Should().Be(request.Title);
            document.UpdatedAt.Should().BeBefore(DateTime.Now);
        }
    }
}
