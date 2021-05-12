using System;
using Api.Common;
using Api.Common.Auth;

{
public class ClassName : BaseClient
{
public override string Name => "ClassName"
public InsuranceResource Insurance { get; private set; }
public ClassName(ICredential credentials, string baseUrl) : base(credentials, baseUrl)
Insurance = new InsuranceResource(this);
}
}

}
