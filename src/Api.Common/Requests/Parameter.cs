namespace Api.Common.Requests
{
    public class Parameter : IParameter
    {
        public string Name { get; set; }

        public bool IsRequired { get; set; }

        public string ParameterType { get; set; }
    }
}
