using FluentValidation;

namespace Charbagh.Application.Instruments.Create;

public sealed class CreateInstrumentValidator : AbstractValidator<CreatebookCommand>
{
    public CreateInstrumentValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.EnglishName)
            .MaximumLength(20);

        RuleFor(x => x.Description)
            .MaximumLength(500);
    }
}