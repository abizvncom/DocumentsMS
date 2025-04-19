using DocumentsWebApi.Data;
using DocumentsWebApi.Models;
using DocumentsWebApiTests.v2.Fakers;
using Microsoft.Extensions.DependencyInjection;

namespace DocumentsWebApiTests.Common
{
    public static class SampleDataHelper
    {
        public static async Task<Document> CreateSampleDocument(this CustomWebApiFactory factory)
        {
            var documents = await factory.CreateSampleDocuments(1);
            return documents.First();
        }

        public static async Task<IList<Document>> CreateSampleDocuments(this CustomWebApiFactory factory, int count)
        {
            var newDocumentRequests = new NewDocumentRequestFaker().Generate(count);

            var newDocuments = newDocumentRequests.Select(x => new Document
            {
                Title = x.Title,
            }).ToList();


            using (var scope = factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<DocumentDbContext>();
                newDocumentRequests.ForEach(documentRequest =>
                {
                    context.Documents.AddRange(newDocuments);
                });

                await context.SaveChangesAsync();

                return newDocuments;
            }
        }
    }
}
