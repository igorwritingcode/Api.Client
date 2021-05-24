namespace Api.Client.Generator.Model
{
    //Represents response in client API model
    public class ApiResponse
    {
        public string StatusCode { get; set; }
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public ApiFieldType.Object? Body { get; set; } = new();
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
    }
}
