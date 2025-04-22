namespace DocumentsWebApi.Dtos.v2
{
    public record UpdateDocumentDto
    {
        public int Id { get; init; }

        public string Title { get; init; } = string.Empty;
    }
}
