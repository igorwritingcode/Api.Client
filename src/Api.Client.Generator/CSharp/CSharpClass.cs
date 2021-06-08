using System.Text;

namespace Api.Client.Generator.CSharp
{
    public class CSharpClass
    {
        private readonly StringBuilder _builder = new();
        public void AppendLine(string content) => _builder.AppendLine(content);

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine($"using System;");
            builder.AppendLine($"using Api.Common;");
            builder.AppendLine($"using Api.Common.Auth;");
            builder.AppendLine($"using Api.Client.IfCarApiSe.Dtos;");

            builder.AppendLine("namespace Api.Client {");

            builder.Append(_builder);

            builder.AppendLine("}");
            return builder.ToString();
        }
    }
}
