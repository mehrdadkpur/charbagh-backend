using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
public sealed class StudentCodeGenerator    : IStudentCodeGenerator
{
    private readonly CharbaghDbContext _context;

    public StudentCodeGenerator(CharbaghDbContext context)
    {
        _context = context;
    }

    public async Task<string> GenerateAsync( CancellationToken cancellationToken)
    {
        var connection = _context.Database.GetDbConnection();

        await using var command = connection.CreateCommand();

        command.CommandText = "SELECT NEXT VALUE FOR dbo.StudentCodeSequence";

        if (connection.State != System.Data.ConnectionState.Open)
            await connection.OpenAsync(cancellationToken);

        var result = await command.ExecuteScalarAsync(cancellationToken);

        return Convert.ToInt32(result).ToString();
    }
}