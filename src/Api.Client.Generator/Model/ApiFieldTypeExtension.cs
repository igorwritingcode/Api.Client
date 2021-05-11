using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Client.Generator.Model
{
    public static class ApiFieldTypeExtension
    {
        public static Type ToCSharpPrimitiveType(this ApiFieldType.Primitive subject) =>
           subject.Type switch
           {
               "boolean" => typeof(bool),
               "dateTime" => typeof(DateTime),
               "decimal" => typeof(decimal),
               "int" => typeof(int),
               "string" => typeof(string),
               _ => default!
           };
    }
}
