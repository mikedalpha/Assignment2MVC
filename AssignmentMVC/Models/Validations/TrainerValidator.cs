using FluentValidation;

namespace AssignmentMVC.Models.Validations
{
    public class TrainerValidator : AbstractValidator<Trainer>
    {
        public TrainerValidator()
        {
            RuleFor(t=>t.FirstName)
                .NotEmpty().WithMessage("Required Field")
                .Length(2, 50).WithMessage("Length 2-50 characters")
                .Matches("^[a-zA-Z_ ]*$").WithMessage("Only Letters");

            RuleFor(t => t.LastName)
                .NotEmpty().WithMessage("Required Field")
                .Length(2, 50).WithMessage("Length 2-50 characters")
                .Matches("^[a-zA-Z_ ]*$").WithMessage("Only Letters");

            RuleFor(t => t.Subject)
                .NotEmpty().WithMessage("Required Field")
                .Length(2, 50).WithMessage("Length 2-50 characters")
                .Matches("^[a-zA-Z_ ]*$").WithMessage("Only Letters");
        }
    }
}