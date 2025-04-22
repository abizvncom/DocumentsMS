namespace DocumentsWebApi.Infrastructure
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string modelName, string message)
            : base(message)
        {
            ModelName = modelName;
        }

        public string ModelName { get; }
    }
}
