using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.Contacts.Common;
using MediatR;

namespace Charbagh.Application.Contacts.GetById
{
    public sealed class GetContactByIdHandler : IRequestHandler<GetContactByIdQuery,ContactDto>
    {
        private readonly IContactRepository _repository;

        public GetContactByIdHandler(IContactRepository repository)
        {
            _repository = repository;
        }

        public async Task<ContactDto> Handle(GetContactByIdQuery request, CancellationToken cancellationToken)
        {
            var contact = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (contact == null)
            {
                throw new ContactNotFoundException(request.Id);
            }

            return contact.ToDto();
        }
    }
}
