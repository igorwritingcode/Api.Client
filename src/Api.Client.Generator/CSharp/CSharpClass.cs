using System.Text;

namespace Api.Client.Generator.CSharp
{
    public class CSharpClass
    {
        private readonly StringBuilder _builder = new StringBuilder();
        public void AppendLine(string content) => _builder.AppendLine(content);

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine($"using System;");
            builder.AppendLine($"using Api.Common;");
            builder.AppendLine($"using Api.Common.Auth;");
            builder.AppendLine($"using Api.Client.IfCarApiSe.Dtos;");

            builder.AppendLine($"");

            builder.Append(_builder.ToString());

            return builder.ToString();
        }
    }
}
