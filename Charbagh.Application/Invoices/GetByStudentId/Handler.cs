
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.Invoices.Common;
using MediatR;

namespace Charbagh.Application.Invoices.GetByStudentId
{
    public sealed class GetAllInvoicesByStudentIdHandler : IRequestHandler<GetAllInvoicesByStudentIdQuery, IReadOnlyList< InvoiceDto>>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IStudentRepository _studentRepository;

        public GetAllInvoicesByStudentIdHandler(IInvoiceRepository invoiceRepository , IStudentRepository studentRepository)
        {
            _invoiceRepository = invoiceRepository;
            _studentRepository = studentRepository;
        }

        public async Task<IReadOnlyList<InvoiceDto>> Handle(GetAllInvoicesByStudentIdQuery request, CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetByIdAsync(request.StudentId , cancellationToken);

            if(student == null)
            {
                throw new StudentNotFoundException();
            }

            var invoices = await _invoiceRepository.GetByStudentIdAsync(request.StudentId,cancellationToken);

            return invoices.Select(x => x.ToDto()).ToList();
        }
    }
}
