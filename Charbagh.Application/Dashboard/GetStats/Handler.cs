using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Dashboard.GetStats.Common;
using MediatR;

namespace Charbagh.Application.Dashboard.GetStats;

public sealed class GetDashboardStatsHandler
    : IRequestHandler<GetDashboardStatsQuery, DashboardStatsDto>
{
    private readonly IUserRepository _userRepository;
    private readonly ITeacherRepository _teacherRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly IStaffRepository _staffRepository;
    private readonly IBranchRepository _branchRepository;
    private readonly IBlogRepository _blogRepository;
    private readonly IBookRepository _bookRepository;
    private readonly IInstrumentRepository _instrumentRepository;

    public GetDashboardStatsHandler(
        IUserRepository userRepository,
        ITeacherRepository teacherRepository,
        IStudentRepository studentRepository,
        IStaffRepository staffRepository,
        IBranchRepository branchRepository,
        IBlogRepository blogRepository,
        IBookRepository bookRepository,
        IInstrumentRepository instrumentRepository)
    {
        _userRepository = userRepository;
        _teacherRepository = teacherRepository;
        _studentRepository = studentRepository;
        _staffRepository = staffRepository;
        _branchRepository = branchRepository;
        _blogRepository = blogRepository;
        _bookRepository = bookRepository;
        _instrumentRepository = instrumentRepository;
    }

    public async Task<DashboardStatsDto> Handle(
        GetDashboardStatsQuery request,
        CancellationToken cancellationToken)
    {
        var totalUsers =
            await _userRepository.CountAsync(cancellationToken);

        var totalTeachers =
            await _teacherRepository.CountAsync(cancellationToken);

        var totalStudents =
            await _studentRepository.CountAsync(cancellationToken);

        var totalStaffs =
            await _staffRepository.CountAsync(cancellationToken);

        var totalBranches =
            await _branchRepository.CountAsync(cancellationToken);

        var totalBlogs =
            await _blogRepository.CountAsync(cancellationToken);

        var totalBooks =
            await _bookRepository.CountAsync(cancellationToken);

        var totalInstruments =
            await _instrumentRepository.CountAsync(cancellationToken);

        return new DashboardStatsDto(
            TotalUsers: totalUsers,
            TotalTeachers: totalTeachers,
            TotalStudents: totalStudents,
            TotalStaffs: totalStaffs,
            TotalBranches: totalBranches,
            TotalBlogs: totalBlogs,
            TotalBooks: totalBooks,
            TotalInstruments: totalInstruments);
    }
}