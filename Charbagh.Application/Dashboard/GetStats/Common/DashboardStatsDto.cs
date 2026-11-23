namespace Charbagh.Application.Dashboard.GetStats.Common;

public sealed record DashboardStatsDto(
    int TotalUsers,
    int TotalTeachers,
    int TotalStudents,
    int TotalStaffs,
    int TotalBranches,
    int TotalBlogs,
    int TotalBooks,
    int TotalInstruments
    );