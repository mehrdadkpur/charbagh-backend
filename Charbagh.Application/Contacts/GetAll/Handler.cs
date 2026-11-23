using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Contacts.Common;
using MediatR;

namespace Charbagh.Application.Contacts.GetAll
{
    public sealed class GetAllContactsHandler : IRequestHandler<GetAllContactsQuery , IReadOnlyList<ContactDto>>
    {
        private readonly IContactRepository _repository;

        public GetAllContactsHandler(IContactRepository repository)
        {
            _repository = repository; 
        }

        public async Task<IReadOnlyList<ContactDto>> Handle (GetAllContactsQuery request , CancellationToken cancellationToken)
        {
            var contacts = await _repository.GetAllAsync(cancellationToken);

            return contacts.Select(x => x.ToDto()).ToList();
        }
    }
}
