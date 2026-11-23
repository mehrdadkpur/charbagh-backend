using Charbagh.Application.Features.Identity.LoginHistories.Dtos;
using MediatR;

namespace Charbagh.Application.LoginHistories.RecordLogout;

public sealed record RecordLogoutCommand(Guid Id) : IRequest<LoginHistoryDto>;