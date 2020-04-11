using System;
using FluentValidation;

namespace Student.Public.WebApi.Models.Students
{
    public sealed class StudentsQueryBinding
    {
        public Int32 Offset { get; set; } = 0;
        public Int32 Limit { get; set; } = 20;
        public String Filter { get; set; }
    }

    public sealed class StudentsQueryBindingValidator : AbstractValidator<StudentsQueryBinding>
    {
        public StudentsQueryBindingValidator()
        {
            RuleFor(r => r.Offset)
                .GreaterThanOrEqualTo(0);
            RuleFor(r => r.Offset)
                .InclusiveBetween(0, 100);
        }
    }
}