using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.Contacts.Common;
using MediatR;

namespace Charbagh.Application.Contacts.Reopen
{
    public sealed class ReopenContactHandler : IRequestHandler<ReopenContactCommand, ContactDto>
    {
        private readonly IContactRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public ReopenContactHandler(IContactRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ContactDto> Handle(ReopenContactCommand request, CancellationToken cancellationToken)
        {
            var contact = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (contact == null)
            {
                throw new ContactNotFoundException(request.Id);
            }

            contact.Reopen();

            return contact.ToDto();
        }
    }
}
