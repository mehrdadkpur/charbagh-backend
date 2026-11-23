
using Charbagh.Application.Dashboard.GetStats.Common;
using MediatR;

namespace Charbagh.Application.Dashboard.GetStats
{
    public sealed record GetDashboardStatsQuery(): IRequest<DashboardStatsDto>;
}
