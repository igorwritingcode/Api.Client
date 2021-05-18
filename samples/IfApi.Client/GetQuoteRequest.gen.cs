using System;
using Api.Common;
using Api.Common.Auth;
using Api.Client.IfCarApiSe.Dtos;

public class GetQuoteRequest 
{
public override string MethodName => "getQuote";
public override string RestPath => "/Insurance/quote/{id}";
public override string HttpMethod => "GET";
private GetQuoteRequestBody { get; init; }
public GetQuoteRequest (IClient client, id) : base(client)
{
base.InitParameters();
RequestParameters.Add("id", new Parameter(Name = "id", IsRequired = true, ParameterType="path"));
}
protected override object GetBody() => Body;
}

