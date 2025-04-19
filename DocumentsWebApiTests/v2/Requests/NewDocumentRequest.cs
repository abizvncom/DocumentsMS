namespace DocumentsWebApiTests.v2.Requests
{
    public sealed record NewDocumentRequest
    {
        public string Title { get; init; } = string.Empty;
    }
}
