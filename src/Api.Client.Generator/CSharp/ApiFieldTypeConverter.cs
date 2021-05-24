using Api.Client.Generator.Model;

namespace Api.Client.Generator.CSharp
{
    public class ApiFieldTypeConverter
    {
        public static ApiFieldType Convert(string type) => type switch
        {
            "object" => new ApiFieldType.Object(),
            "array" => new ApiFieldType.Array(type),
            "string" => new ApiFieldType.Primitive(type),
            "boolean" => new ApiFieldType.Primitive(type),
            _ => null
        };
    }
}
