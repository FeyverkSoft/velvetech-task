using System;
using FluentValidation;
using Student.Types;

namespace Student.Public.WebApi.Models.Students
{
    public sealed class StudentAddBinding
    {
        /// <summary>
        /// Student unique identificator 
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Student unique public identificator 
        /// </summary>
        public String PublicId { get; set; }

        /// <summary>
        /// First name
        /// </summary>
        public String FirstName { get; set; }

        /// <summary>
        /// Last name
        /// </summary>
        public String LastName { get; set; }

        /// <summary>
        /// Second name
        /// </summary>
        public String SecondName { get; set; }

        /// <summary>
        /// gender
        /// </summary>
        public StudentGender Gender { get; set; }
    }

    public sealed class StudentAddBindingValidator : AbstractValidator<StudentAddBinding>
    {
        public StudentAddBindingValidator()
        {
            RuleFor(r => r.Id)
                .NotEmpty();

            When(_ => !String.IsNullOrEmpty(_.PublicId), () =>
            {
                RuleFor(r => r.PublicId)
                    .MaximumLength(16)
                    .MinimumLength(6);
            });

            RuleFor(r => r.FirstName)
                .NotEmpty()
                .MaximumLength(40);
            RuleFor(r => r.LastName)
                .NotEmpty()
                .MaximumLength(40);
            
            When(_ => !String.IsNullOrEmpty(_.SecondName), () =>
            {
                RuleFor(r => r.SecondName)
                    .MaximumLength(40);
            });
        }
    }
}