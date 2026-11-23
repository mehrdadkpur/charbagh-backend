using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.Contacts.Common;
using MediatR;

namespace Charbagh.Application.Contacts.Close
{
    public sealed class CloseContactHandler : IRequestHandler<CloseContactCommand, ContactDto>
    {
        private readonly IContactRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CloseContactHandler(IContactRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ContactDto> Handle(CloseContactCommand request, CancellationToken cancellationToken)
        {
            var contact = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (contact == null)
            {
                throw new ContactNotFoundException(request.Id);
            }

            contact.Close();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return contact.ToDto();
        }
    }
}
