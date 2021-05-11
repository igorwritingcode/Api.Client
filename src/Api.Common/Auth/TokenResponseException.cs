using System;
using System.Net;

namespace Api.Common.Auth
{
    public class TokenResponseException : Exception
    {
        public TokenErrorResponse Error { get; }

        public HttpStatusCode? StatusCode { get; }

        public TokenResponseException(TokenErrorResponse error)
            : this(error, null) { }

        public TokenResponseException(TokenErrorResponse error, HttpStatusCode? statusCode)
            : base(error.ToString())
        {
            Error = error;
            StatusCode = statusCode;
        }
    }
}
