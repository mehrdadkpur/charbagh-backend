using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Contacts.Common;
using Charbagh.Domain.Entities.CMS;
using MediatR;

namespace Charbagh.Application.Contacts.Create
{
    public sealed class CreateContactHandler : IRequestHandler<CreateContactCommand , ContactDto>
    {
        private readonly IContactRepository _contactRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateContactHandler (IContactRepository contactRepository, IUnitOfWork unitOfWork)
        {
            _contactRepository = contactRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ContactDto> Handle (CreateContactCommand request , CancellationToken cancellationToken)
        {
            var contact = Contact.Create(request.FullName, request.Mobile);

            await _contactRepository.AddAsync(contact, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return contact.ToDto();
        }
    }
}
