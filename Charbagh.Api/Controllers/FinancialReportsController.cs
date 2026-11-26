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
    private readonly ITransactionRepository _transactionRepository;

    public FinancialReportsController(
        IInvoiceRepository invoiceRepository,
        IPaymentRepository paymentRepository,
        IStudentRepository studentRepository,
        ITransactionRepository transactionRepository)
    {
        _invoiceRepository = invoiceRepository;
        _paymentRepository = paymentRepository;
        _studentRepository = studentRepository;
        _transactionRepository = transactionRepository;
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
        var transactions = await _transactionRepository.GetAllAsync(cancellationToken);

        var invoicePeriod = allInvoices
            .Where(x => x.InvoiceDate >= start && x.InvoiceDate < endExclusive)
            .ToList();

        var validInvoices = invoicePeriod
            .Where(x => x.Status != InvoiceStatus.Draft &&
                        x.Status != InvoiceStatus.Cancelled)
            .ToList();

        var paymentReceiptTransactions = transactions
            .Where(x => x.Type == TransactionType.PaymentReceived)
            .ToList();

        var paymentIdsWithReceiptTransactions = paymentReceiptTransactions
            .Select(x => x.PaymentId)
            .ToHashSet();

        var successfulReceiptAmountsByInvoice = paymentReceiptTransactions
            .Where(x => x.Status == TransactionStatus.Successful)
            .GroupBy(x => x.Payment.InvoiceId)
            .ToDictionary(group => group.Key, group => group.Sum(x => x.Amount));

        var successfulRefundAmountsByInvoice = transactions
            .Where(x => x.Type == TransactionType.Refund &&
                        x.Status == TransactionStatus.Successful)
            .GroupBy(x => x.Payment.InvoiceId)
            .ToDictionary(group => group.Key, group => group.Sum(x => x.Amount));

        // Payments created before receipt transactions existed are recognized as legacy
        // receipts only up to the amount actually reflected in the invoice balance.
        var legacyConfirmedPaymentIds = new HashSet<Guid>();
        foreach (var invoice in allInvoices)
        {
            var legacyCapacity = invoice.PaidAmount;
            if (successfulRefundAmountsByInvoice.TryGetValue(invoice.Id, out var refunded))
                legacyCapacity += refunded;
            if (successfulReceiptAmountsByInvoice.TryGetValue(invoice.Id, out var received))
                legacyCapacity -= received;

            var legacyPayments = allPayments
                .Where(x => x.InvoiceId == invoice.Id &&
                            !paymentIdsWithReceiptTransactions.Contains(x.Id))
                .OrderBy(x => x.CreatedAt)
                .ToList();

            foreach (var legacyPayment in legacyPayments)
            {
                if (legacyCapacity < legacyPayment.Amount)
                    break;

                legacyConfirmedPaymentIds.Add(legacyPayment.Id);
                legacyCapacity -= legacyPayment.Amount;
            }
        }

        var paymentPeriod = transactions
            .Where(x => x.Type == TransactionType.PaymentReceived &&
                        x.Status == TransactionStatus.Successful &&
                        x.TransactionDate >= start &&
                        x.TransactionDate < endExclusive)
            .Select(x => new
            {
                Id = x.Id,
                InvoiceId = x.Payment.InvoiceId,
                PaymentDate = x.TransactionDate,
                Amount = x.Amount,
                PaymentMethodId = x.Payment.PaymentMethodId,
                ReferenceNumber = x.ReferenceNumber ?? x.Payment.ReferenceNumber,
                Note = x.Description ?? x.Payment.Note
            })
            .Concat(allPayments
                .Where(x => legacyConfirmedPaymentIds.Contains(x.Id) &&
                            x.PaymentDate >= start &&
                            x.PaymentDate < endExclusive)
                .Select(x => new
                {
                    Id = x.Id,
                    InvoiceId = x.InvoiceId,
                    PaymentDate = x.PaymentDate,
                    Amount = x.Amount,
                    PaymentMethodId = x.PaymentMethodId,
                    ReferenceNumber = x.ReferenceNumber,
                    Note = x.Note
                }))
            .OrderBy(x => x.PaymentDate)
            .ToList();

        var refundPeriod = transactions
            .Where(x => x.Type == TransactionType.Refund &&
                        x.Status == TransactionStatus.Successful &&
                        x.TransactionDate >= start &&
                        x.TransactionDate < endExclusive)
            .OrderBy(x => x.TransactionDate)
            .ToList();

        var cashFlowEvents = paymentPeriod
            .Select(x => new { Date = x.PaymentDate.Date, Amount = x.Amount })
            .Concat(refundPeriod.Select(x => new
            {
                Date = x.TransactionDate.Date,
                Amount = -x.Amount
            }))
            .ToList();

        var studentNames = students.ToDictionary(
            x => x.Id,
            x => x.User.FullName);

        var dailyPayments = cashFlowEvents
            .GroupBy(x => x.Date)
            .OrderBy(x => x.Key)
            .Select(group => new
            {
                date = group.Key,
                amount = group.Sum(x => x.Amount),
                count = group.Count()
            })
            .ToList();

        var monthlyPayments = cashFlowEvents
            .GroupBy(x => new DateTime(x.Date.Year, x.Date.Month, 1))
            .OrderBy(x => x.Key)
            .Select(group => new
            {
                month = group.Key,
                amount = group.Sum(x => x.Amount),
                count = group.Count()
            })
            .ToList();

        var serviceRevenue = validInvoices
            .SelectMany(invoice => invoice.Items.Select(item => new
            {
                serviceName = item.Description,
                netAmount = invoice.Subtotal > 0
                    ? item.TotalAmount - (item.TotalAmount / invoice.Subtotal * invoice.DiscountAmount)
                    : item.TotalAmount,
                quantity = item.Quantity
            }))
            .GroupBy(item => item.serviceName)
            .Select(group => new
            {
                serviceName = group.Key,
                amount = Math.Round(group.Sum(item => item.netAmount), 2),
                quantity = group.Sum(item => item.quantity)
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
            collectedAmount = paymentPeriod.Sum(x => x.Amount) -
                              refundPeriod.Sum(x => x.Amount),
            paymentCount = paymentPeriod.Count,
            refundAmount = refundPeriod.Sum(x => x.Amount),
            refundCount = refundPeriod.Count,
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
            }),
            refunds = refundPeriod.Select(x => new
            {
                id = x.Id,
                paymentId = x.PaymentId,
                transactionDate = x.TransactionDate,
                amount = x.Amount,
                referenceNumber = x.ReferenceNumber,
                description = x.Description
            })
        });
    }
}
