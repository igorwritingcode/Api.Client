using Api.Client.Generator.Model;
using System.Text;

namespace Api.Client.Generator.CSharp
{
    public class CSharpApiClientGenerator
    {
        private readonly ApiModelContext _context;
        public CSharpApiClientGenerator(ApiModelContext context)
        {
            _context = context;
        }
        private string GenerateClientDefinition(string apiName)
        {
            var builder = new StringBuilder();
            builder.AppendLine($"public class {apiName}Client : BaseClient {{");
            builder.AppendLine($"public override string Name => \"{apiName}\"");
            builder.AppendLine($"public override string Name => \"{apiName}\"");
            //public QuoteInsuranceResource Insurance { get; private set; }

            builder.AppendLine("}}");
            return builder.ToString();
        }

        public string GenerateResourceDefinition(string apiName)
        {
            var builder = new StringBuilder();
            builder.AppendLine($"public override string Name => \"{apiName}\"");
            return builder.ToString();
        }
    }






}
