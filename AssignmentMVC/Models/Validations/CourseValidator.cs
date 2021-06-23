using FluentValidation;

namespace AssignmentMVC.Models.Validations
{
    public class CourseValidator : AbstractValidator<Course>
    {
        public CourseValidator()
        {
            RuleFor(c => c.Title)
                .NotEmpty().WithMessage("Required Field")
                .Length(2, 50).WithMessage("Length 2-50 characters")
                .Matches("^[a-zA-Z_ ]*$").WithMessage("Only Letters");

            RuleFor(c => c.Stream)
                .NotEmpty().WithMessage("Required Field")
                .Length(2, 50).WithMessage("Length 2-50 characters")
                .Matches("^[a-zA-Z_ ]*$").WithMessage("Only Letters");

            RuleFor(t => t.Type)
                .NotEmpty().WithMessage("Required Field")
                .Length(2, 50).WithMessage("Length 2-50 characters")
                .Matches("^[a-zA-Z_ ]*$").WithMessage("Only Letters");

            RuleFor(c => c.StartDate).NotEmpty().WithMessage("Required Field");

            RuleFor(c => c.EndDate).NotEmpty().WithMessage("Required Field")
                .GreaterThanOrEqualTo(c => c.StartDate.Date)
                .WithMessage("End date must be greater than or equal to Start date");
        }
    }
}