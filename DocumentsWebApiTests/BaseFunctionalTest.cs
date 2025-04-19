
namespace DocumentsWebApiTests
{
    public abstract class BaseFunctionalTest(CustomWebApiFactory factory) : IAsyncLifetime
    {
        protected CustomWebApiFactory Factory => factory;

        protected HttpClient Client => factory.CreateClient();

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }

        public async Task InitializeAsync()
        {
            await Factory.ResetDatabaseAsync();
        }
    }
}
