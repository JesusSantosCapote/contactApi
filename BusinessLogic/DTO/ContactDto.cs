using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTO
{
    public class ContactDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Phone { get; set; }
    }

    public class ContactDtoValidator: AbstractValidator<ContactDto>
    {
        public ContactDtoValidator()
        {
            RuleFor(x => x.FirstName).NotNull().NotEmpty().WithMessage("First Name is required");
            RuleFor(x => x.FirstName).MaximumLength(128).WithMessage("First Name is to large");

            RuleFor(x => x.LastName).MaximumLength(128).WithMessage("Last Name is to large");

            RuleFor(x => x.Email).EmailAddress().WithMessage("Value of Email field is not a valid email address");

            RuleFor(x => x.Phone).NotNull().NotEmpty().WithMessage("Phone is required");

            RuleFor(x => x.DateOfBirth).NotNull().NotEmpty().WithMessage("Date of Birth is required");
            RuleFor(x => x.DateOfBirth).Must(date => AgeIsValid(date)).WithMessage("The contact is younger than 18 years old");
            
        }

        private static bool AgeIsValid(DateTime dateOfBirth)
        {
            var maxDateOfBirth = DateTime.Now.AddYears(BusinessLogicConstants.minimalAge * -1);

            if (dateOfBirth > maxDateOfBirth)
            {
                return false;
            }
            return true;
        }
    }
}
