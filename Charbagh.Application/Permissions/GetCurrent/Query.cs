using Charbagh.Application.Permissions.Current;
using MediatR;

namespace Charbagh.Application.Permissions.GetCurrent;

public sealed record GetCurrentPermissionsQuery : IRequest<CurrentPermissionsDto?>;