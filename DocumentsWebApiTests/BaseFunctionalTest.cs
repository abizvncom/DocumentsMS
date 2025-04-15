namespace DocumentsWebApiTests
{
    public abstract class BaseFunctionalTest(CustomWebApiFactory factory)
    {
        protected CustomWebApiFactory Factory => factory;
        protected HttpClient Client => factory.CreateClient();
    }
}
