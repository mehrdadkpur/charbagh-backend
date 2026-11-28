namespace Charbagh.Infrastructure.Setup;

internal sealed record PermissionDefinition(
    string Module,
    string Name,
    PermissionAction Action,
    string DisplayName,
    string? Description = null);

internal static class PermissionDefinitions
{
    public static readonly IReadOnlyList<PermissionDefinition> All =
    [
        // Identity - Users
        new("Users", "users.view", PermissionAction.View, "مشاهده کاربران"),
        new("Users", "users.create", PermissionAction.Create, "ایجاد کاربر"),
        new("Users", "users.update", PermissionAction.Update, "ویرایش کاربر"),
        new("Users", "users.delete", PermissionAction.Delete, "حذف کاربر"),

        // Identity - Roles
        new("Roles", "roles.view", PermissionAction.View, "مشاهده نقش‌ها"),
        new("Roles", "roles.create", PermissionAction.Create, "ایجاد نقش"),
        new("Roles", "roles.update", PermissionAction.Update, "ویرایش نقش‌ها"),
        new("Roles", "roles.delete", PermissionAction.Delete, "حذف نقش‌ها"),

        // Identity - Permissions
        new("Permissions", "permissions.view", PermissionAction.View, "مشاهده مجوزها"),
        new("Permissions", "permissions.manage", PermissionAction.Manage, "مدیریت مجوزها"),

        // Identity - Branches
        new("Branches", "branches.view", PermissionAction.View, "مشاهده شعبه‌ها"),
        new("Branches", "branches.create", PermissionAction.Create, "ایجاد شعبه"),
        new("Branches", "branches.update", PermissionAction.Update, "ویرایش شعبه"),
        new("Branches", "branches.delete", PermissionAction.Delete, "حذف شعبه"),

        // Identity - User branches
        new("UserBranches", "user-branches.view", PermissionAction.View, "مشاهده عضویت شعبه‌ها"),
        new("UserBranches", "user-branches.create", PermissionAction.Create, "افزودن کاربر به شعبه"),
        new("UserBranches", "user-branches.update", PermissionAction.Update, "ویرایش عضویت شعبه"),
        new("UserBranches", "user-branches.delete", PermissionAction.Delete, "حذف عضویت شعبه"),

        // Identity - User branch roles
        new("UserBranchRoles", "user-branch-roles.view", PermissionAction.View, "مشاهده نقش‌های کاربران در شعبه"),
        new("UserBranchRoles", "user-branch-roles.create", PermissionAction.Create, "اختصاص نقش به کاربر"),
        new("UserBranchRoles", "user-branch-roles.update", PermissionAction.Update, "ویرایش نقش کاربر در شعبه"),
        new("UserBranchRoles", "user-branch-roles.delete", PermissionAction.Delete, "حذف نقش کاربر از شعبه"),

        // Identity - Nationalities
        new("Nationalities", "nationalities.view", PermissionAction.View, "مشاهده ملیت‌ها"),
        new("Nationalities", "nationalities.create", PermissionAction.Create, "ایجاد ملیت"),
        new("Nationalities", "nationalities.update", PermissionAction.Update, "ویرایش ملیت‌ها"),
        new("Nationalities", "nationalities.delete", PermissionAction.Delete, "حذف ملیت‌ها"),

        // People - Teachers
        new("Teachers", "teachers.view", PermissionAction.View, "مشاهده اساتید"),
        new("Teachers", "teachers.create", PermissionAction.Create, "ایجاد استاد"),
        new("Teachers", "teachers.update", PermissionAction.Update, "ویرایش استاد"),
        new("Teachers", "teachers.delete", PermissionAction.Delete, "حذف استاد"),

        // People - Students
        new("Students", "students.view", PermissionAction.View, "مشاهده هنرجویان"),
        new("Students", "students.create", PermissionAction.Create, "ثبت هنرجو"),
        new("Students", "students.update", PermissionAction.Update, "ویرایش هنرجو"),
        new("Students", "students.delete", PermissionAction.Delete, "حذف هنرجو"),

        // People - Parents
        new("Parents", "parents.view", PermissionAction.View, "مشاهده والدین"),
        new("Parents", "parents.create", PermissionAction.Create, "ثبت والد"),
        new("Parents", "parents.update", PermissionAction.Update, "ویرایش والد"),
        new("Parents", "parents.delete", PermissionAction.Delete, "حذف والد"),

        // People - Staff
        new("Staff", "staffs.view", PermissionAction.View, "مشاهده کارکنان"),
        new("Staff", "staffs.create", PermissionAction.Create, "ثبت کارمند"),
        new("Staff", "staffs.update", PermissionAction.Update, "ویرایش کارمند"),
        new("Staff", "staffs.delete", PermissionAction.Delete, "حذف کارمند"),

        // People - Student parents
        new("StudentParents", "student-parents.view", PermissionAction.View, "مشاهده ارتباط هنرجو و والد"),
        new("StudentParents", "student-parents.create", PermissionAction.Create, "ثبت ارتباط هنرجو و والد"),
        new("StudentParents", "student-parents.update", PermissionAction.Update, "ویرایش ارتباط هنرجو و والد"),
        new("StudentParents", "student-parents.delete", PermissionAction.Delete, "حذف ارتباط هنرجو و والد"),

        // People - Teacher instruments
        new("TeacherInstruments", "teacher-instruments.view", PermissionAction.View, "مشاهده سازهای اساتید"),
        new("TeacherInstruments", "teacher-instruments.create", PermissionAction.Create, "اختصاص ساز به استاد"),
        new("TeacherInstruments", "teacher-instruments.update", PermissionAction.Update, "ویرایش سازهای استاد"),
        new("TeacherInstruments", "teacher-instruments.delete", PermissionAction.Delete, "حذف ساز استاد"),

        // Education catalog - Instrument categories
        new("InstrumentCategories", "instrument-categories.view", PermissionAction.View, "مشاهده دسته‌بندی سازها"),
        new("InstrumentCategories", "instrument-categories.create", PermissionAction.Create, "ایجاد دسته‌بندی ساز"),
        new("InstrumentCategories", "instrument-categories.update", PermissionAction.Update, "ویرایش دسته‌بندی ساز"),
        new("InstrumentCategories", "instrument-categories.delete", PermissionAction.Delete, "حذف دسته‌بندی ساز"),

        // Education catalog - Instruments
        new("Instruments", "instruments.view", PermissionAction.View, "مشاهده سازها"),
        new("Instruments", "instruments.create", PermissionAction.Create, "ایجاد ساز"),
        new("Instruments", "instruments.update", PermissionAction.Update, "ویرایش ساز"),
        new("Instruments", "instruments.delete", PermissionAction.Delete, "حذف ساز"),

        // Education catalog - Books
        new("Books", "books.view", PermissionAction.View, "مشاهده کتاب‌ها"),
        new("Books", "books.create", PermissionAction.Create, "ایجاد کتاب"),
        new("Books", "books.update", PermissionAction.Update, "ویرایش کتاب"),
        new("Books", "books.delete", PermissionAction.Delete, "حذف کتاب"),

        // Education catalog - Book items
        new("BookItems", "book-items.view", PermissionAction.View, "مشاهده قطعات و آیتم‌های کتاب"),
        new("BookItems", "book-items.create", PermissionAction.Create, "ایجاد قطعه یا آیتم کتاب"),
        new("BookItems", "book-items.update", PermissionAction.Update, "ویرایش قطعه یا آیتم کتاب"),
        new("BookItems", "book-items.delete", PermissionAction.Delete, "حذف قطعه یا آیتم کتاب"),

        // Student education - Lesson assignments
        new("LessonAssignments", "lesson-assignments.view", PermissionAction.View, "مشاهده تخصیص‌های آموزشی"),
        new("LessonAssignments", "lesson-assignments.create", PermissionAction.Create, "ایجاد تخصیص آموزشی"),
        new("LessonAssignments", "lesson-assignments.update", PermissionAction.Update, "ویرایش تخصیص آموزشی"),
        new("LessonAssignments", "lesson-assignments.delete", PermissionAction.Delete, "حذف تخصیص آموزشی"),

        // Student education - Teaching plans
        new("TeachingPlans", "teaching-plans.view", PermissionAction.View, "مشاهده برنامه‌های تدریس"),
        new("TeachingPlans", "teaching-plans.create", PermissionAction.Create, "ایجاد برنامه تدریس"),
        new("TeachingPlans", "teaching-plans.update", PermissionAction.Update, "ویرایش برنامه تدریس"),
        new("TeachingPlans", "teaching-plans.delete", PermissionAction.Delete, "حذف برنامه تدریس"),

        // Student education - Lesson sessions
        new("LessonSessions", "lesson-sessions.view", PermissionAction.View, "مشاهده جلسات درس"),
        new("LessonSessions", "lesson-sessions.create", PermissionAction.Create, "ایجاد جلسه درس"),
        new("LessonSessions", "lesson-sessions.update", PermissionAction.Update, "ویرایش جلسه درس"),
        new("LessonSessions", "lesson-sessions.delete", PermissionAction.Delete, "حذف جلسه درس"),

        // Student education - Lesson content
        new("LessonContents", "lesson-contents.view", PermissionAction.View, "مشاهده محتوای جلسات"),
        new("LessonContents", "lesson-contents.create", PermissionAction.Create, "ثبت محتوای جلسه"),
        new("LessonContents", "lesson-contents.update", PermissionAction.Update, "ویرایش محتوای جلسه"),
        new("LessonContents", "lesson-contents.delete", PermissionAction.Delete, "حذف محتوای جلسه"),

        // Student education - Attendance
        new("Attendance", "attendance.view", PermissionAction.View, "مشاهده حضور و غیاب"),
        new("Attendance", "attendance.create", PermissionAction.Create, "ثبت حضور و غیاب"),
        new("Attendance", "attendance.update", PermissionAction.Update, "ویرایش حضور و غیاب"),
        new("Attendance", "attendance.delete", PermissionAction.Delete, "حذف رکورد حضور و غیاب"),

        // Student education - Practice assignments
        new("PracticeAssignments", "practice-assignments.view", PermissionAction.View, "مشاهده تمرین‌ها"),
        new("PracticeAssignments", "practice-assignments.create", PermissionAction.Create, "ایجاد تمرین"),
        new("PracticeAssignments", "practice-assignments.update", PermissionAction.Update, "ویرایش تمرین"),
        new("PracticeAssignments", "practice-assignments.delete", PermissionAction.Delete, "حذف تمرین"),

        // Student education - Evaluations
        new("Evaluations", "evaluations.view", PermissionAction.View, "مشاهده ارزیابی‌ها"),
        new("Evaluations", "evaluations.create", PermissionAction.Create, "ثبت ارزیابی"),
        new("Evaluations", "evaluations.update", PermissionAction.Update, "ویرایش ارزیابی"),
        new("Evaluations", "evaluations.delete", PermissionAction.Delete, "حذف ارزیابی"),
        new("Evaluations", "evaluations.approve", PermissionAction.Approve, "تأیید ارزیابی"),

        // Finance - Education services
        new("EducationServices", "education-services.view", PermissionAction.View, "مشاهده خدمات آموزشی"),
        new("EducationServices", "education-services.create", PermissionAction.Create, "ایجاد خدمت آموزشی"),
        new("EducationServices", "education-services.update", PermissionAction.Update, "ویرایش خدمت آموزشی"),
        new("EducationServices", "education-services.delete", PermissionAction.Delete, "حذف خدمت آموزشی"),

        // Finance - Service prices
        new("ServicePrices", "service-prices.view", PermissionAction.View, "مشاهده قیمت خدمات"),
        new("ServicePrices", "service-prices.create", PermissionAction.Create, "ثبت قیمت خدمت"),
        new("ServicePrices", "service-prices.update", PermissionAction.Update, "ویرایش قیمت خدمت"),
        new("ServicePrices", "service-prices.delete", PermissionAction.Delete, "حذف قیمت خدمت"),

        // Finance - Invoices
        new("Invoices", "invoices.view", PermissionAction.View, "مشاهده فاکتورها"),
        new("Invoices", "invoices.create", PermissionAction.Create, "ایجاد فاکتور"),
        new("Invoices", "invoices.update", PermissionAction.Update, "ویرایش فاکتور"),
        new("Invoices", "invoices.delete", PermissionAction.Delete, "حذف فاکتور"),
        new("Invoices", "invoices.approve", PermissionAction.Approve, "تأیید فاکتور"),
        new("Invoices", "invoices.print", PermissionAction.Print, "چاپ فاکتور"),
        new("Invoices", "invoices.export", PermissionAction.Export, "خروجی فاکتورها"),

        // Finance - Invoice items
        new("InvoiceItems", "invoice-items.view", PermissionAction.View, "مشاهده اقلام فاکتور"),
        new("InvoiceItems", "invoice-items.create", PermissionAction.Create, "افزودن قلم فاکتور"),
        new("InvoiceItems", "invoice-items.update", PermissionAction.Update, "ویرایش قلم فاکتور"),
        new("InvoiceItems", "invoice-items.delete", PermissionAction.Delete, "حذف قلم فاکتور"),

        // Finance - Payments
        new("Payments", "payments.view", PermissionAction.View, "مشاهده پرداخت‌ها"),
        new("Payments", "payments.create", PermissionAction.Create, "ثبت پرداخت"),
        new("Payments", "payments.update", PermissionAction.Update, "ویرایش پرداخت"),
        new("Payments", "payments.delete", PermissionAction.Delete, "حذف پرداخت"),
        new("Payments", "payments.approve", PermissionAction.Approve, "تأیید پرداخت"),
        new("Payments", "payments.print", PermissionAction.Print, "چاپ رسید پرداخت"),
        new("Payments", "payments.export", PermissionAction.Export, "خروجی پرداخت‌ها"),

        // Finance - Payment methods
        new("PaymentMethods", "payment-methods.view", PermissionAction.View, "مشاهده روش‌های پرداخت"),
        new("PaymentMethods", "payment-methods.create", PermissionAction.Create, "ایجاد روش پرداخت"),
        new("PaymentMethods", "payment-methods.update", PermissionAction.Update, "ویرایش روش پرداخت"),
        new("PaymentMethods", "payment-methods.delete", PermissionAction.Delete, "حذف روش پرداخت"),

        // Finance - Transactions
        new("Transactions", "transactions.view", PermissionAction.View, "مشاهده تراکنش‌ها"),
        new("Transactions", "transactions.create", PermissionAction.Create, "ثبت تراکنش"),
        new("Transactions", "transactions.update", PermissionAction.Update, "ویرایش تراکنش"),
        new("Transactions", "transactions.delete", PermissionAction.Delete, "حذف تراکنش"),
        new("Transactions", "transactions.export", PermissionAction.Export, "خروجی تراکنش‌ها"),

        // Finance - Discounts
        new("Discounts", "discounts.view", PermissionAction.View, "مشاهده تخفیف‌ها"),
        new("Discounts", "discounts.create", PermissionAction.Create, "ایجاد تخفیف"),
        new("Discounts", "discounts.update", PermissionAction.Update, "ویرایش تخفیف"),
        new("Discounts", "discounts.delete", PermissionAction.Delete, "حذف تخفیف"),

        // Finance - Discount types
        new("DiscountTypes", "discount-types.view", PermissionAction.View, "مشاهده انواع تخفیف"),
        new("DiscountTypes", "discount-types.create", PermissionAction.Create, "ایجاد نوع تخفیف"),
        new("DiscountTypes", "discount-types.update", PermissionAction.Update, "ویرایش نوع تخفیف"),
        new("DiscountTypes", "discount-types.delete", PermissionAction.Delete, "حذف نوع تخفیف"),

        // CMS - Blogs
        new("Blogs", "blogs.view", PermissionAction.View, "مشاهده مطالب"),
        new("Blogs", "blogs.create", PermissionAction.Create, "ایجاد مطلب"),
        new("Blogs", "blogs.update", PermissionAction.Update, "ویرایش مطلب"),
        new("Blogs", "blogs.delete", PermissionAction.Delete, "حذف مطلب"),
        new("Blogs", "blogs.publish", PermissionAction.Approve, "انتشار مطلب"),

        // CMS - Announcement banners
        new("Banners", "banners.view", PermissionAction.View, "مشاهده بنرهای اطلاع‌رسانی"),
        new("Banners", "banners.create", PermissionAction.Create, "ایجاد بنر اطلاع‌رسانی"),
        new("Banners", "banners.update", PermissionAction.Update, "ویرایش بنر اطلاع‌رسانی"),
        new("Banners", "banners.delete", PermissionAction.Delete, "بایگانی بنر اطلاع‌رسانی"),
        new("Banners", "banners.publish", PermissionAction.Approve, "انتشار بنر اطلاع‌رسانی"),

        // CMS - Comments
        new("Comments", "comments.view", PermissionAction.View, "مشاهده دیدگاه‌ها"),
        new("Comments", "comments.create", PermissionAction.Create, "ثبت دیدگاه"),
        new("Comments", "comments.update", PermissionAction.Update, "ویرایش دیدگاه"),
        new("Comments", "comments.delete", PermissionAction.Delete, "حذف دیدگاه"),
        new("Comments", "comments.approve", PermissionAction.Approve, "تأیید دیدگاه"),

        // CMS - Categories
        new("Categories", "categories.view", PermissionAction.View, "مشاهده دسته‌بندی‌ها"),
        new("Categories", "categories.create", PermissionAction.Create, "ایجاد دسته‌بندی"),
        new("Categories", "categories.update", PermissionAction.Update, "ویرایش دسته‌بندی"),
        new("Categories", "categories.delete", PermissionAction.Delete, "حذف دسته‌بندی"),

        // CMS - Tags
        new("Tags", "tags.view", PermissionAction.View, "مشاهده برچسب‌ها"),
        new("Tags", "tags.create", PermissionAction.Create, "ایجاد برچسب"),
        new("Tags", "tags.update", PermissionAction.Update, "ویرایش برچسب‌ها"),
        new("Tags", "tags.delete", PermissionAction.Delete, "حذف برچسب‌ها"),
        
        // CMS - Contact requests
        new("Contacts", "contacts.view", PermissionAction.View, "مشاهده درخواست‌های پشتیبانی"),
        new("Contacts", "contacts.manage", PermissionAction.Manage, "مدیریت درخواست‌های پشتیبانی"),

        // CMS - Galleries
        new("Galleries", "galleries.view", PermissionAction.View, "مشاهده گالری‌ها"),
        new("Galleries", "galleries.create", PermissionAction.Create, "ایجاد گالری"),
        new("Galleries", "galleries.update", PermissionAction.Update, "ویرایش گالری"),
        new("Galleries", "galleries.delete", PermissionAction.Delete, "بایگانی گالری"),
        new("Galleries", "galleries.publish", PermissionAction.Approve, "انتشار گالری"),

        // CMS - Gallery items
        new("GalleryItems", "gallery-items.view", PermissionAction.View, "مشاهده رسانه‌های گالری"),
        new("GalleryItems", "gallery-items.create", PermissionAction.Create, "افزودن رسانه به گالری"),
        new("GalleryItems", "gallery-items.update", PermissionAction.Update, "ویرایش رسانه گالری"),
        new("GalleryItems", "gallery-items.delete", PermissionAction.Delete, "حذف رسانه گالری"),

        // Security - Login histories
        new("LoginHistories", "login-histories.view", PermissionAction.View, "مشاهده تاریخچه ورود"),
        new("LoginHistories", "login-histories.manage", PermissionAction.Manage, "مدیریت تاریخچه ورود"),

        // Reports
        new("Reports", "reports.view", PermissionAction.View, "مشاهده گزارش‌ها"),

        // CMS - Generic exports
        new("Reports", "reports.export", PermissionAction.Export, "خروجی گزارش‌ها")
    ];
}