using System;
using Api.Common;
using Api.Common.Auth;
using Api.Client.IfCarApiSe.Dtos;

public class DeleteQuoteRequest 
{
    public override string MethodName => "deleteQuote";
    public override string RestPath => "/Insurance/quote/{id}";
    public override string HttpMethod => "DELETE";
    public DeleteQuoteRequest (IClient client, string id) : base(client)
    {
        base.InitParameters();
        RequestParameters.Add("id", new Parameter(Name = "id", IsRequired = true, ParameterType="path"));
    }
    protected override object GetBody() => Body;
}

