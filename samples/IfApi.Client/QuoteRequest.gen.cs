using System;
using Api.Common;
using Api.Common.Auth;
using Api.Client.IfCarApiSe.Dtos;

public class QuoteRequest 
{
    public override string MethodName => "quote";
    public override string RestPath => "/Insurance/quote";
    public override string HttpMethod => "POST";
    private QuoteRequestBody QuoteRequestBody { get; init; }
    public QuoteRequest (IClient client, QuoteRequestBody quoteRequestBody) : base(client)
    {
        Body = body;
        base.InitParameters();
    }
    protected override object GetBody() => Body;
}

