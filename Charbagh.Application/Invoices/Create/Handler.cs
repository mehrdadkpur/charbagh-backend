
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.Invoices.Common;
using Charbagh.Domain.Entities.Finance;
using MediatR;

namespace Charbagh.Application.Invoices.Create
{
    public sealed class CreateInvoiceHandler:IRequestHandler<CreateInvoiceCommand , InvoiceDto>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateInvoiceHandler(IInvoiceRepository invoiceRepository, IStudentRepository studentRepository, IUnitOfWork unitOfWork)
        {
            _invoiceRepository = invoiceRepository;
            _studentRepository =studentRepository; 
            _unitOfWork = unitOfWork;
        }

        public async Task<InvoiceDto> Handle(CreateInvoiceCommand request , CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetByIdAsync(request.StudentId, cancellationToken);

            if(student == null)
            {
                throw new StudentNotFoundException();
            }

            var invoice = Invoice.Create(request.StudentId, DateTime.UtcNow, request.Description);

            await _invoiceRepository.AddAsync(invoice, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return invoice.ToDto();
        }
    }
}
