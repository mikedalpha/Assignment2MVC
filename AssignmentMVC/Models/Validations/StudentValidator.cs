using FluentValidation;

namespace AssignmentMVC.Models.Validations
{
    public class StudentValidator : AbstractValidator<Student>
    {
        public StudentValidator()
        {
            RuleFor(s => s.FirstName)
                .NotEmpty().WithMessage("Required Field")
                .Length(2, 50).WithMessage("Length 2-50 characters")
                .Matches("^[a-zA-Z_ ]*$").WithMessage("Only Letters");

            RuleFor(s => s.LastName)
                .NotEmpty().WithMessage("Required Field")
                .Length(2, 50).WithMessage("Length 2-50 characters")
                .Matches("^[a-zA-Z_ ]*$").WithMessage("Only Letters");

            RuleFor(s => s.DateOfBirth)
                .NotEmpty().WithMessage("Required Field");

            RuleFor(s => s.TuitionFees)
                .NotEmpty().WithMessage("Required Field")
                .InclusiveBetween(0, 2500).WithMessage("Invalid Tuition Fee! Must be 0-2500");
        }
    }
}