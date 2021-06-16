#nullable enable
#pragma warning disable CS8618

using System.Collections.Generic;

namespace Api.Client.Generator.Model
{
    //Represents request in client API model
    public class ApiRequest
    {
        public string Name { get; set; }
        public string RestPath { get; set; }
        public string HttpMethod { get; set; }
        public ApiFieldType.Object? Body { get; set; }
        public string[] Parameters { get; set; }
        public IEnumerable<ApiResponse> Responses { get; set; }
    }

    public class ApiField
    {
        public string Name { get; set; } = default!;
        public bool Required { get; set; }
        public ApiFieldType Type { get; set; }
    }

    public abstract class ApiFieldType
    {
        public string ClassName { get; set; }
        public bool Nullable { get; set; }
        public class Array : ApiFieldType
        {
            public string Type { get; set; } = default!;
            public Array(string type)
            {
                Type = type;
            }
        }

        public class Object : ApiFieldType
        {
            public List<ApiField> Fields { get; set; } = new List<ApiField>();
        }

        public class Primitive : ApiFieldType
        {
            public string Type { get; set; } = default!;

            public Primitive(string type)
            {
                Type = type;
            }
        }

        public class Number : ApiFieldType
        {
            public string Type { get; set; } = default!;
            public string Format { get; set; } = default!;

            public Number(string type, string format)
            {
                Type = type;
                Format = format;
            }
        }
    }
}
