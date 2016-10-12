using Cyrus.Core.DomainModels;
using FluentValidation;

namespace Cyrus.WebApi.ModelValidators
{
    public class StudentValidator : AbstractValidator<Tribe>
    {
        public StudentValidator()
        {
            RuleFor(x => x.Name).Length(0, 10);
        }
    }
}