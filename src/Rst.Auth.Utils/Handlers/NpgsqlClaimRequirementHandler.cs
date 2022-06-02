using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Npgsql;
using NpgsqlTypes;
using Rst.Auth.Utils.Options;
using Rst.Auth.Utils.Requirements;

namespace Rst.Auth.Utils.Handlers;

public class NpgsqlClaimRequirementHandler : AuthorizationHandler<ClaimRequirement>
{
    private readonly NpgsqlConnection _connection;

    public NpgsqlClaimRequirementHandler(IOptions<ClaimRequirementsOptions> options)
    {
        _connection = new NpgsqlConnection(options.Value.ConnectionString);
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context, ClaimRequirement requirement)
    {
        await _connection.OpenAsync();
        var claims = new NpgsqlParameter("claims", NpgsqlDbType.Array | NpgsqlDbType.LTree)
        {
            Value = context.User.Claims.Where(c => c.Type == requirement.Type).Select(c => c.Value).ToArray()
        };
        var query = new NpgsqlParameter("query", NpgsqlDbType.LQuery)
        {
            Value = requirement.Query
        };
        var value = new NpgsqlParameter("value", NpgsqlDbType.LTree)
        {
            Value = requirement.Query
        };

        var command = _connection.CreateCommand();
        var builder = new StringBuilder("select ");
        if (requirement.Flags.HasFlag(TreeFlags.Child))
        {
            builder.AppendJoin(' ', '@' + claims.ParameterName, "<@", '@' + value.ParameterName, "or ");
        }

        if (requirement.Flags.HasFlag(TreeFlags.Pattern))
        {
            builder.AppendJoin(' ', '@' + claims.ParameterName,  '~', '@' + query.ParameterName, "or ");
        }

        builder.Append("false");

        command.CommandText = builder.ToString();
        command.Parameters.Add(claims);
        command.Parameters.Add(query);
        command.Parameters.Add(value);

        await using var reader = await command.ExecuteReaderAsync();
        await reader.ReadAsync();

        if (!reader.GetBoolean(0))
        {
            context.Fail(new AuthorizationFailureReason(this, "Claims forbid"));
        }

        await _connection.CloseAsync();
    }
}