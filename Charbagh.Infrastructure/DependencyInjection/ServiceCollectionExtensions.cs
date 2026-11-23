using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Abstractions.Persistence.Repositories.Identity;
using Charbagh.Application.Abstractions.Persistence.Repositories.People;
using Charbagh.Application.Abstractions.Security;
using Charbagh.Application.Common.Interfaces;
using Charbagh.Infrastructure.Persistence;
using Charbagh.Infrastructure.Persistence.Context;
using Charbagh.Infrastructure.Persistence.Repositories;
using Charbagh.Infrastructure.Persistence.Repositories.Identity;
using Charbagh.Infrastructure.Security;
using Charbagh.Infrastructure.Services;
using Charbagh.Infrastructure.Setup;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Charbagh.Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure( this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CharbaghDbContext>(options =>
        {
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddScoped<NationalitySeeder>();
        services.AddScoped<PermissionSeeder>();
        services.AddScoped<RoleSeeder>();
        services.AddScoped<RolePermissionSeeder>();
        services.AddScoped<InitialAdminBootstrapper>();
        services.AddScoped<IBranchRepository, BranchRepository>();
        services.AddScoped<IInstrumentRepository, InstrumentRepository>();
        services.AddScoped<IInstrumentCategoryRepository, InstrumentCategoryRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<INationalityRepository, NationalityRepository>();
        services.AddScoped<IStudentRepository , StudentRepository>();
        services.AddScoped<ITeacherRepository, TeacherRepository>();
        services.AddScoped<IParentRepository , ParentRepository>();
        services.AddScoped<IStaffRepository, StaffRepository>();
        services.AddScoped<IStudentParentRepository , StudentParentRepository>();
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IBookItemRepository , BookItemRepository>();
        services.AddScoped<ILessonAssignmentRepository, LessonAssignmentRepository>();
        services.AddScoped<ITeachingPlanRepository, TeachingPlanRepository>();
        services.AddScoped<ILessonSessionRepository, LessonSessionRepository>();
        services.AddScoped<ILessonContentRepository, LessonContentRepository>();
        services.AddScoped<IAttendanceRepository, AttendanceRepository>();
        services.AddScoped<IPracticeAssignmentRepository, PracticeAssignmentRepository>();
        services.AddScoped<IEvaluationRepository, EvaluationRepository>();
        services.AddScoped<IInvoiceItemRepository, InvoiceItemRepository>();
        services.AddScoped<IEducationServiceRepository, EducationServiceRepository>();
        services.AddScoped<IServicePriceRepository, ServicePriceRepository>();
        services.AddScoped<IInvoiceRepository, InvoiceRepository>();
        services.AddScoped<IInvoiceItemRepository, InvoiceItemRepository>();
        services.AddScoped<IBlogRepository, BlogRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<IContactRepository, ContactRepository>();
        services.AddScoped<IGalleryRepository, GalleryRepository>();
        services.AddScoped<ITagRepository, TagRepository>();
        services.AddScoped<IRolePermissionRepository , RolePermissionRepository>();
        services.AddScoped<IRoleRepository , RoleRepository>();
        services.AddScoped< IRoleManagementRepository,RoleManagementRepository>();
        services.AddScoped<IPermissionRepository, PermissionRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IGalleryItemRepository, GalleryItemRepository>();
        services.AddScoped<ILoginHistoryRepository, LoginHistoryRepository>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IRolePermissionRepository , RolePermissionRepository>();
        services.AddScoped<ISecurityAuditRepository , SecurityAuditRepository>();
        services.AddScoped<IUserBranchRoleRepository , UserBranchRoleRepository>();
        services.AddScoped<IUserBranchRepository , UserBranchRepository>();
        services.AddScoped<ITeacherInstrumentRepository , TeacherInstrumentRepository>();
        services.AddScoped<IClassroomRepository, ClassroomRepository>();
        services.AddScoped<IPaymentMethodRepository, PaymentMethodRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddScoped<IDiscountTypeRepository, DiscountTypeRepository>();
        services.AddScoped<IDiscountRepository, DiscountRepository>(); 
        services.AddScoped<ICurrentUser, CurrentUser>();
        services.AddScoped<ICurrentBranch, CurrentBranch>();
        services.AddScoped<IRefreshTokenGenerator, RefreshTokenGenerator>();
        services.AddScoped<IPermissionChecker, PermissionChecker>();
        services.AddScoped<ICurrentPermissionsService,CurrentPermissionsService>();
        services.AddScoped<ITeacherPersonnelCodeGenerator, TeacherPersonnelCodeGenerator>();
        services.AddScoped<IStudentCodeGenerator, StudentCodeGenerator>();
        services.AddScoped<IStaffPersonnelCodeGenerator, StaffPersonnelCodeGenerator>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}