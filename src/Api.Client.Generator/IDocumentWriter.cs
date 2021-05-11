namespace Api.Client.Generator
{
    public interface IDocumentWriter
    {
        void WriteDocument(string path, string content);
    }
}
