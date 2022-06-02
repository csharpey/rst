# LTree claims

## Install 

```c#
<ItemGroup>
    <PackageReference Include="Rst.Auth.Utils" Version="0.0.2" />
</ItemGroup>
```

## Services

- npgsql based requirements verification

```c#
services.AddNpgsqlClaimRequirements(options =>
{
    options.ConnectionString = "<npgsql-connection-string>";
});
```

## Policies

```c#
options.AddPolicy("Admin.Sample", policy =>
{
    policy.RequirePerm("perm", "sample.resource.*.create", "sample.resource.*.update");
    
});
```

## Attributes

```c#
[HttpGet]
[RequireClaim("perm", "sample.resource.read")]
public async Task<IActionResult> Get()
{
    return Ok();
}
```

## Additional resources

[Postgres Docs](https://postgrespro.ru/docs/postgrespro/14/ltree)

