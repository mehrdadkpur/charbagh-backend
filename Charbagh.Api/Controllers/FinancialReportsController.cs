using Charbagh.Application.Abstractions.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace Charbagh.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class FinancialReportsController : ControllerBase
{
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly IPaymentRepository _paymentRepository;
    private readonly IStudentRepository _studentRepository;

    public FinancialReportsController(
        IInvoiceRepository invoiceRepository,
        IPaymentRepository paymentRepository,
        IStudentRepository studentRepository)
    {
        _invoiceRepository = invoiceRepository;
        _paymentRepository = paymentRepository;
        _studentRepository = studentRepository;
    }

    [HttpGet("summary")]
    public async Task<IActionResult> GetSummary(
        [FromQuery] DateTime? fromDate,
        [FromQuery] DateTime? toDate,
        CancellationToken cancellationToken)
    {
        var start = (fromDate ?? DateTime.Today.AddDays(-29)).Date;
        var end = (toDate ?? DateTime.Today).Date;

        if (start > end)
            return BadRequest("تاریخ شروع نمی‌تواند بعد از تاریخ پایان باشد.");

        var endExclusive = end.AddDays(1);

        var allInvoices = await _invoiceRepository.GetAllAsync(cancellationToken);
        var allPayments = await _paymentRepository.GetAllAsync(cancellationToken);
        var students = await _studentRepository.GetAllAsync(cancellationToken);

        var invoicePeriod = allInvoices
            .Where(x => x.InvoiceDate >= start && x.InvoiceDate < endExclusive)
            .ToList();

        var validInvoices = invoicePeriod
            .Where(x => x.Status != InvoiceStatus.Draft &&
                        x.Status != InvoiceStatus.Cancelled)
            .ToList();

        var paymentPeriod = allPayments
            .Where(x => x.PaymentDate >= start && x.PaymentDate < endExclusive)
            .OrderBy(x => x.PaymentDate)
            .ToList();

        var studentNames = students.ToDictionary(
            x => x.Id,
            x => x.User.FullName);

        var dailyPayments = paymentPeriod
            .GroupBy(x => x.PaymentDate.Date)
            .OrderBy(x => x.Key)
            .Select(group => new
            {
                date = group.Key,
                amount = group.Sum(x => x.Amount),
                count = group.Count()
            })
            .ToList();

        var monthlyPayments = paymentPeriod
            .GroupBy(x => new DateTime(x.PaymentDate.Year, x.PaymentDate.Month, 1))
            .OrderBy(x => x.Key)
            .Select(group => new
            {
                month = group.Key,
                amount = group.Sum(x => x.Amount),
                count = group.Count()
            })
            .ToList();

        var serviceRevenue = validInvoices
            .SelectMany(invoice => invoice.Items)
            .GroupBy(item => item.Description)
            .Select(group => new
            {
                serviceName = group.Key,
                amount = group.Sum(item => item.TotalAmount),
                quantity = group.Sum(item => item.Quantity)
            })
            .OrderByDescending(x => x.amount)
            .ToList();

        var outstandingInvoices = allInvoices
            .Where(x => x.Status == InvoiceStatus.Unpaid ||
                        x.Status == InvoiceStatus.PartiallyPaid)
            .Where(x => x.RemainingAmount > 0.01m)
            .OrderByDescending(x => x.InvoiceDate)
            .Select(x => new
            {
                invoiceId = x.Id,
                invoiceDate = x.InvoiceDate,
                studentId = x.StudentId,
                studentName = studentNames.TryGetValue(x.StudentId, out var name)
                    ? name
                    : "هنرجوی نامشخص",
                totalAmount = x.TotalAmount,
                paidAmount = x.PaidAmount,
                remainingAmount = x.RemainingAmount,
                status = x.Status
            })
            .ToList();

        var studentBalances = outstandingInvoices
            .GroupBy(x => new { x.studentId, x.studentName })
            .Select(group => new
            {
                studentId = group.Key.studentId,
                studentName = group.Key.studentName,
                invoiceCount = group.Count(),
                remainingAmount = group.Sum(x => x.remainingAmount)
            })
            .OrderByDescending(x => x.remainingAmount)
            .ToList();

        return Ok(new
        {
            fromDate = start,
            toDate = end,
            invoiceCount = validInvoices.Count,
            invoicedAmount = validInvoices.Sum(x => x.TotalAmount),
            discountAmount = validInvoices.Sum(x => x.DiscountAmount),
            collectedAmount = paymentPeriod.Sum(x => x.Amount),
            paymentCount = paymentPeriod.Count,
            outstandingAmount = outstandingInvoices.Sum(x => x.remainingAmount),
            outstandingInvoiceCount = outstandingInvoices.Count,
            dailyPayments,
            monthlyPayments,
            serviceRevenue,
            studentBalances,
            outstandingInvoices,
            payments = paymentPeriod.Select(x => new
            {
                id = x.Id,
                invoiceId = x.InvoiceId,
                paymentDate = x.PaymentDate,
                amount = x.Amount,
                paymentMethodId = x.PaymentMethodId,
                referenceNumber = x.ReferenceNumber,
                note = x.Note
            })
        });
    }
}
