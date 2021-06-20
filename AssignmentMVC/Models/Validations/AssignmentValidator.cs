using FluentValidation;

namespace AssignmentMVC.Models.Validations
{
    public class AssignmentValidator : AbstractValidator<Assignment>
    {
        public AssignmentValidator()
        {
            RuleFor(a => a.Title)
                .NotEmpty().WithMessage("Required Field")
                .Length(2, 50).WithMessage("Length 2-50 characters")
                .Matches("^[a-zA-Z_ ]*$").WithMessage("Only Letters");

            RuleFor(a => a.Description)
                .NotEmpty().WithMessage("Required Field")
                .Length(2, 100).WithMessage("Length 2-100 characters")
                .Matches("^[a-zA-Z_ ]*$").WithMessage("Only Letters");

            RuleFor(a => a.SubDateTime)
                .NotEmpty().WithMessage("Required Field");

            RuleFor(a => a.OralMark)
                .NotEmpty().WithMessage("Required Field")
                .InclusiveBetween(1, 100).WithMessage("Invalid Mark! Must be 0-100");

            RuleFor(a => a.TotalMark)
                .NotEmpty().WithMessage("Required Field")
                .InclusiveBetween(1, 100).WithMessage("Invalid Mark! Must be 0-100");
        }
    }
}