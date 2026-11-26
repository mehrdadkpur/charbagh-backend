using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.Contacts.Common;
using MediatR;

namespace Charbagh.Application.Contacts.StartProcessing
{
    public sealed class StartProcessingContactHandler : IRequestHandler<StartProcessingContactCommand, ContactDto>
    {
        private readonly IContactRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public StartProcessingContactHandler(IContactRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ContactDto> Handle(StartProcessingContactCommand request, CancellationToken cancellationToken)
        {
            var contact = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (contact == null)
            {
                throw new ContactNotFoundException(request.Id);
            }

            contact.StartProcessing();
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return contact.ToDto();
        }
    }
}
