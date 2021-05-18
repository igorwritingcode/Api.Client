namespace Api.Common.Requests
{
    public interface IParameter
    {
        string Name { get; }
        bool IsRequired { get; }
        string ParameterType { get; }
    }
}
