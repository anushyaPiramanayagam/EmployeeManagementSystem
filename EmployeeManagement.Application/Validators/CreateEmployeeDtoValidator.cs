using FluentValidation;
using EmployeeManagement.Application.DTOs;

namespace EmployeeManagement.Application.Validators;

public class CreateEmployeeDtoValidator
    : AbstractValidator<CreateEmployeeDto>
{
    public CreateEmployeeDtoValidator()
    {
        RuleFor(x => x.EmployeeCode)
            .NotEmpty()
            .WithMessage(
                "Employee Code is required");

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage(
                "First Name is required")
            .MaximumLength(50);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage(
                "Last Name is required")
            .MaximumLength(50);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Designation)
            .NotEmpty();
    }
}