using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Contacts.Common;
using MediatR;

namespace Charbagh.Application.Contacts.GetbyMobile
{
    public sealed class GetContactsByMobileHandler : IRequestHandler<GetContactsByMobileQuery, IReadOnlyList<ContactDto>>
    {
        private readonly IContactRepository _repository;

        public GetContactsByMobileHandler(IContactRepository repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyList<ContactDto>> Handle(GetContactsByMobileQuery request, CancellationToken cancellationToken)
        {
            var contacts = await _repository.GetByMobileAsync(request.Mobile, cancellationToken);

            return contacts.Select(x => x.ToDto()).ToList();
        }
    }
}
