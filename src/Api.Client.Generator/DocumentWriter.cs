using System.IO;

namespace Api.Client.Generator
{
    public class DocumentWriter : IDocumentWriter
    {
        private readonly DirectoryInfo _root;
        public DocumentWriter(DirectoryInfo root)
        {
            _root = root;
        }
        public void WriteDocument(string path, string content)
        {
            _root.Create();
            var filePath = Path.Combine(_root.FullName, path);
            Directory.GetParent(filePath).Create();
            File.WriteAllText(filePath, content);
        }
    }
}
