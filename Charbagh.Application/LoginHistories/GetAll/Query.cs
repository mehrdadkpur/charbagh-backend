using Charbagh.Application.Features.Identity.LoginHistories.Dtos;
using MediatR;

namespace Charbagh.Application.LoginHistories.GetAll;

public sealed record GetAllLoginHistoriesQuery: IRequest<IReadOnlyList<LoginHistoryDto>>;