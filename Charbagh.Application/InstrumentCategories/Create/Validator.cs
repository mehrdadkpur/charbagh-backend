using Charbagh.Application.Bookitems.Create;
using FluentValidation;

namespace Charbagh.Application.Instruments.Create;

public sealed class CreateInstrumentValidator : AbstractValidator<CreateBookCommand>
{
    public CreateInstrumentValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.EnglishTitle)
            .MaximumLength(20);
    }
}