using Charbagh.Domain.Entities.Branches;
using Charbagh.Domain.Entities.CMS;
using Charbagh.Domain.Entities.Education.EducationCatalog;
using Charbagh.Domain.Entities.Education.StudentEducation;
using Charbagh.Domain.Entities.Finance;
using Charbagh.Domain.Entities.Identity;
using Charbagh.Domain.Entities.People;
using Charbagh.Domain.Entities.Settings;
using Microsoft.EntityFrameworkCore;

namespace Charbagh.Infrastructure.Persistence.Context;

public sealed class CharbaghDbContext : DbContext
{
    public CharbaghDbContext(DbContextOptions<CharbaghDbContext> options): base(options)
    {
    }

    public DbSet<Branch> Branches => Set<Branch>();
    public DbSet<Instrument> Instruments => Set<Instrument>();
    public DbSet<InstrumentCategory> InstrumentCategories => Set<InstrumentCategory>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Nationality> Nationalities => Set<Nationality>();
    public DbSet <Student> Students => Set<Student>();
    public DbSet<Teacher> Teachers => Set<Teacher>();
    public DbSet<Parent> Parents => Set<Parent>();
    public DbSet<Staff> Staffs => Set<Staff>();
    public DbSet <StudentParent> StudentParents => Set<StudentParent>();
    public DbSet <Book> Books => Set<Book>();
    public DbSet <BookItem> BookItems => Set<BookItem>();
    public DbSet <LessonAssignment> LessonAssignments => Set<LessonAssignment>();
    public DbSet<TeachingPlan> TeachingPlans => Set<TeachingPlan>();
    public DbSet<LessonSession> LessonSessions => Set<LessonSession>();
    public DbSet<LessonContent> LessonContents => Set<LessonContent>();
    public DbSet<Attendance> Attendances => Set<Attendance>();
    public DbSet<PracticeAssignment> PracticeAssignments => Set<PracticeAssignment>();
    public DbSet<Evaluation> Evaluations => Set<Evaluation>();
    public DbSet<Invoice> Invoices => Set<Invoice>();
    public DbSet<InvoiceItem> InvoiceItems => Set<InvoiceItem>();
    public DbSet<EducationService> EducationServices => Set<EducationService>();
    public DbSet<ServicePrice> ServicePrices => Set<ServicePrice>();
    public DbSet<Blog> Blogs => Set<Blog>();
    public DbSet<AnnouncementBanner> AnnouncementBanners => Set<AnnouncementBanner>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Comment> Comments => Set<Comment>();
    public DbSet<Contact> Contacts => Set<Contact>();
    public DbSet<Gallery> Galleries => Set<Gallery>();
    public DbSet<GalleryItem> GalleryItems => Set<GalleryItem>();
    public DbSet<Tag> Tags => Set<Tag>();
    public DbSet<Permission> Permissions => Set<Permission>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<RolePermission> RolePermissions => Set<RolePermission>();
    public DbSet<UserBranchRole> UserBranchRoles => Set<UserBranchRole>();
    public DbSet<UserBranch> UserBranches => Set<UserBranch>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    public DbSet<SecurityAudit> SecurityAudits => Set<SecurityAudit>();
    public DbSet<LoginHistory> LoginHistories => Set<LoginHistory>();
    public DbSet<TeacherInstrument> TeacherInstruments => Set<TeacherInstrument>();
    public DbSet<Classroom> Classrooms => Set<Classroom>();
    public DbSet<PaymentMethod> PaymentMethods => Set<PaymentMethod>();
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<Transaction> Transactions => Set<Transaction>();
    public DbSet<DiscountType> DiscountTypes => Set<DiscountType>();
    public DbSet<Discount> Discounts => Set<Discount>();



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CharbaghDbContext).Assembly);

        foreach (var foreignKey in modelBuilder.Model
                     .GetEntityTypes()
                     .SelectMany(e => e.GetForeignKeys()))
        {
            if (foreignKey.DeleteBehavior == DeleteBehavior.Cascade)
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<BaseEntity>();

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.MarkAsCreated();
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Entity.MarkAsUpdated();
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}