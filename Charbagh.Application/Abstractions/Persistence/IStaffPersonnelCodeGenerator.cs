namespace Charbagh.Application.Abstractions.Persistence
{
    public interface IStaffPersonnelCodeGenerator
    {
        Task<string> GenerateAsync(CancellationToken cancellationToken);
    }
}
