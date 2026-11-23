namespace Charbagh.Application.Abstractions.Persistence
{
    public interface ITeacherPersonnelCodeGenerator
    {
        Task<string> GenerateAsync(CancellationToken cancellationToken);
    }
}
