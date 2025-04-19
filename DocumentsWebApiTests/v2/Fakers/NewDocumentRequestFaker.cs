using Bogus;
using DocumentsWebApiTests.v2.Requests;

namespace DocumentsWebApiTests.v2.Fakers
{
    public class NewDocumentRequestFaker : Faker<NewDocumentRequest>
    {
        public NewDocumentRequestFaker()
        {
            RuleFor(x => x.Title, f => f.Lorem.Sentence());
        }
    }
}
