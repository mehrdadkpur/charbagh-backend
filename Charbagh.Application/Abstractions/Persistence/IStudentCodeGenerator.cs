namespace Charbagh.Application.Abstractions.Persistence
{
    public interface IStudentCodeGenerator
    {
        Task<string> GenerateAsync(CancellationToken cancellationToken);
    }
}
