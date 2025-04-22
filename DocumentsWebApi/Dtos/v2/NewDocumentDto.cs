namespace DocumentsWebApi.Dtos.v2
{
    public record NewDocumentDto
    {
        public string Title { get; init; } = string.Empty;
    }
}
