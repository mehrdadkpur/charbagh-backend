Charbagh Music School
Ubiquitous Language v1.0

این سند، فرهنگ لغت رسمی پروژه است.


Identity
User

تعریف:

هر شخصی که بتواند وارد سیستم شود.

ممکن است:

مدیر
استاد
هنرجو
والد
کارمند

باشد.

User هویت ورود به سیستم است، نه نقش آموزشی.

Role

مجموعه‌ای از مجوزها که در یک شعبه به کاربر داده می‌شود.

مثال:

Teacher

Reception

BranchManager

Admin
Permission

کوچک‌ترین واحد مجوز.

مثال:

Lesson.Create

Lesson.Edit

Student.View

Payment.Receive
UserBranchRole

مشخص می‌کند:

یک User

در یک Branch

چه Roleهایی دارد.

People
Student

کاربری که به عنوان هنرجوی آموزشگاه شناخته می‌شود.

Student یک User است.

Teacher

کاربری که مجاز به تدریس است.

Teacher یک User است.

Parent

کاربری که سرپرست یک یا چند هنرجو است.

Staff

کاربری که کارمند آموزشگاه است.

Education Catalog
InstrumentCategory

دسته‌بندی سازها.

مثال:

سازهای زهی

سازهای بادی

سازهای کوبه‌ای

سازهای کلاویه‌ای
Instrument

یک ساز آموزشی.

مثال:

پیانو

ویولن

تار

سه‌تار
Book

یک کتاب آموزشی رسمی.

همیشه متعلق به یک ساز است.

BookItem

کوچک‌ترین بخش آموزشی یک کتاب.

ممکن است:

درس
قطعه
اتود
تمرین
تکنیک

باشد.

Education
LessonAssignment

مهم‌ترین مفهوم پروژه.

تعریف:

قرارداد آموزشی بین یک هنرجو و یک استاد برای یادگیری یک ساز.

تا زمانی که این قرارداد فعال نباشد، آموزش آغاز نشده است.

TeachingPlan

برنامه آموزشی یک Assignment.

مشخص می‌کند:

از کدام کتاب
از کدام BookItem
تا کجا
با چه هدفی

آموزش انجام شود.

LessonSession

یک جلسه واقعی آموزش.

ممکن است:

برگزار شود.
لغو شود.
هنرجو غایب باشد.
استاد غایب باشد.
LessonContent

مطالبی که در یک جلسه تدریس شده‌اند.

ممکن است چند LessonContent در یک جلسه ثبت شوند.

Attendance

وضعیت حضور افراد در جلسه.

PracticeAssignment

تمرین‌هایی که استاد برای منزل مشخص می‌کند.

Evaluation

ارزیابی استاد از عملکرد هنرجو در همان جلسه.

Finance
Invoice

صورتحساب مالی.

InvoiceItem

یک ردیف از صورتحساب.

Payment

ثبت پرداخت هنرجو.

Transaction

تراکنش مالی.

Discount

تخفیف اعمال‌شده روی صورتحساب یا پرداخت.

CMS
Blog

مقاله آموزشی یا خبری.

Comment

نظر کاربران درباره مقاله.