namespace DocumentsWebApiTests.v2.Responses
{
    public class DocumentResponse
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
