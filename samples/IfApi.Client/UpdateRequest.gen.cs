using System;
using Api.Common;
using Api.Common.Auth;
using Api.Client.IfCarApiSe.Dtos;

public class UpdateRequest 
{
public override string MethodName => "update";
public override string RestPath => "/Insurance/quote/{id}";
public override string HttpMethod => "PATCH";
private UpdateRequestBody { get; init; }
public UpdateRequest (IClient client, UpdateRequestBody, id) : base(client)
{
Body = body;
base.InitParameters();
RequestParameters.Add("id", new Parameter(Name = "id", IsRequired = true, ParameterType="path"));
}
protected override object GetBody() => Body;
}

