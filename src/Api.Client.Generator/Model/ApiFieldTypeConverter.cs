namespace Api.Client.Generator.Model
{
    public class ApiFieldTypeConverter
    {
        public static ApiFieldType Convert(string type) => type switch
        {
            "object" => new ApiFieldType.Object(),
            "array" => new ApiFieldType.Array(),
            "string" => new ApiFieldType.Primitive(),
            "boolean" => new ApiFieldType.Primitive(),
            _ => null
        };
    }
}
