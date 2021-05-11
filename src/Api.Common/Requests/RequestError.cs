namespace Api.Common
{
    public class RequestError
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public override string ToString() => $"Error {Message}, Code {Code}";
    }
}
