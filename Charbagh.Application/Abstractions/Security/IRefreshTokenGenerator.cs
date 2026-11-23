namespace Charbagh.Application.Abstractions.Security;

public interface IRefreshTokenGenerator
{
    string Generate();
}