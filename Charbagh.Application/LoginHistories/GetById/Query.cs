using Charbagh.Application.Features.Identity.LoginHistories.Dtos;
using MediatR;

namespace Charbagh.Application.LoginHistories.GetById;

public sealed record GetLoginHistoryByIdQuery( Guid Id) : IRequest<LoginHistoryDto>;