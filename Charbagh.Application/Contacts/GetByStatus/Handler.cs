using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Contacts.Common;
using MediatR;

namespace Charbagh.Application.Contacts.GetByStatus
{
    public sealed class GetContactsByStatusHandler : IRequestHandler<GetContactsByStatusQuery, IReadOnlyList<ContactDto>>
    {
        private readonly IContactRepository _repository;

        public GetContactsByStatusHandler(IContactRepository repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyList<ContactDto>> Handle(GetContactsByStatusQuery request, CancellationToken cancellationToken)
        {
            var contacts = await _repository.GetByStatusAsync(request.Status, cancellationToken);

            return contacts.Select(x => x.ToDto()).ToList();
        }
    }
}
