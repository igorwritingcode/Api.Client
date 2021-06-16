using Api.Client.Generator.Model;

namespace Api.Client.Generator.CSharp.Extension
{
    public static class CSharpFieldTypeExtensions
    {
        public static string ToPrimitiveType(this ApiFieldType.Primitive subject) =>
            subject.Type switch
            {
                "string" => "string",
                "boolean" => "boolean",
                "bool" => "bool",
                "number" => "integer",
                _ => "object"
            };

        public static string ToArrayType(this ApiFieldType.Array subject) => $"{subject.Type}[]";
    }
}
