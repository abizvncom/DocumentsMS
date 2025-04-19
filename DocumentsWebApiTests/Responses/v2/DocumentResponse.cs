namespace DocumentsWebApiTests.Responses.v2
{
    public class DocumentResponse
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
