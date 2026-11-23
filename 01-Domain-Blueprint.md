Domain Blueprint v1.0

۱. Identity & Access
مسئولیت

مدیریت هویت، احراز هویت، مجوزها و امنیت.

Identity
│
├── User
├── Role
├── Permission
├── RolePermission
├── UserBranchRole
├── RefreshToken
├── LoginHistory
└── SecurityAudit
قوانین
فقط User وارد سیستم می‌شود.
هر User می‌تواند در چند شعبه نقش‌های متفاوت داشته باشد.
نقش‌ها مجموعه‌ای از Permissionها هستند.
هیچ بخشی مستقیماً Permission را بررسی نمی‌کند؛ همیشه از Role استفاده می‌شود.
همه عملیات حساس در SecurityAudit ثبت می‌شوند.

۲. People
مسئولیت

اطلاعات تخصصی اشخاص.

People
│
├── Teacher
├── Student
├── Parent
├── Staff
├── StudentParent
└── TeacherInstrument
قوانین
هر Teacher یک User است.
هر Student یک User است.
هر Parent یک User است.
هر Staff یک User است.
یک هنرجو می‌تواند چند والد داشته باشد.
یک استاد می‌تواند چند ساز تدریس کند.
۳. Education Catalog
مسئولیت

تمام محتوای آموزشی.

Education Catalog
│
├── InstrumentCategory
├── Instrument
├── Book
└── BookItem
قوانین
هر ساز متعلق به یک دسته‌بندی است.
هر کتاب متعلق به یک ساز است.
هر BookItem متعلق به یک کتاب است.
BookItem دارای ترتیب (Sequence) است.
فقط مدیر سیستم محتوای آموزشی را مدیریت می‌کند.
۴. Student Education
مسئولیت

تمام فرآیند آموزش.

Student Education
│
├── LessonAssignment
├── TeachingPlan
├── LessonSession
├── LessonContent
├── Attendance
├── PracticeAssignment
├── Evaluation
LessonAssignment

مسئول:

قرارداد آموزشی.

قوانین
هر هنرجو برای هر ساز فقط یک Assignment فعال دارد.
تغییر استاد ⇒ Assignment جدید.
تغییر ساز ⇒ Assignment جدید.
پایان همکاری ⇒ Assignment پایان می‌یابد.
Assignment تاریخچه حذف نمی‌شود.
TeachingPlan

مسئول:

برنامه آموزشی.

قوانین
هر Plan فقط یک کتاب دارد.
یک Assignment می‌تواند چند Plan داشته باشد.
چند Plan می‌توانند هم‌زمان فعال باشند.
تغییر کتاب ⇒ Plan جدید.
تغییر هدف آموزشی ⇒ ویرایش همان Plan.
LessonSession

مسئول:

ثبت یک جلسه واقعی.

قوانین

هر جلسه متعلق به یک Assignment است.
جلسه حتی اگر برگزار نشود، ثبت می‌شود.
وضعیت جلسه مشخص می‌کند که برگزار شده، لغو شده یا هنرجو غایب بوده است.
LessonContent

مسئول:

ثبت مطالب تدریس‌شده در همان جلسه.

قوانین
هر Content متعلق به یک Session است.
هر Content به یک TeachingPlan اشاره می‌کند.
چند Content می‌توانند در یک جلسه وجود داشته باشند.
Attendance

مسئول:

وضعیت حضور.

PracticeAssignment

مسئول:

تمرین‌های منزل.

Evaluation

مسئول:

ارزیابی همان جلسه.

۵. Finance
Finance
│
├── Invoice
├── InvoiceItem
├── Payment
├── PaymentMethod
├── Transaction
└── Discount
قوانین
صورتحساب بر اساس LessonAssignment ایجاد می‌شود.
پرداخت‌ها انعطاف‌پذیر هستند (پیش‌پرداخت، هر جلسه، بسته‌ای و ...).
هیچ اطلاعات مالی داخل LessonSession نگهداری نمی‌شود؛ در صورت نیاز فقط مبلغ واقعی همان جلسه به‌صورت استثنا ثبت می‌شود.
۶. CMS
CMS
│
├── Blog
├── Comment
├── Category
└── Tag
۷. Branch
Branch
│
├── Branch
├── Classroom (آینده)
└── Holiday (آینده)
Aggregateها
Identity
        │
        ▼
People
        │
        ▼
Student Education
        │
 ┌──────┴──────────┐
 ▼                 ▼
Education      Finance

Identity ─────► CMS
Identity ─────► Branch