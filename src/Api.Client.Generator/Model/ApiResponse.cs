namespace Api.Client.Generator.Model
{
    //Represents response in client API model
    public class ApiResponse
    {
        public string StatusCode { get; set; }
        public ApiFieldType.Object? Body { get; set; } = new();
    }
}
