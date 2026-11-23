using Charbagh.Application.Features.Identity.LoginHistories.Dtos;
using MediatR;

namespace Charbagh.Application.LoginHistories.GetByUserId;

public sealed record GetLoginHistoriesByUserIdQuery(Guid UserId): IRequest<IReadOnlyList<LoginHistoryDto>>;