using System;
using FluentValidation;

namespace Student.Public.WebApi.Models.Registrations
{
    public sealed class RegistrationBinding
    {
        /// <summary>
        /// User login
        /// </summary>
        public String Login { get; set; }

        /// <summary>
        /// User name
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// User password
        /// </summary>
        public String Password { get; set; }
    }
    public sealed class RegistrationBindingValidator : AbstractValidator<RegistrationBinding>
    {
        public RegistrationBindingValidator()
        {
            RuleFor(r => r.Login)
                .NotEmpty();
            RuleFor(r => r.Password)
                .NotEmpty()
                .MinimumLength(6);
            RuleFor(r => r.Name)
                .NotEmpty();
        }
    }
}
