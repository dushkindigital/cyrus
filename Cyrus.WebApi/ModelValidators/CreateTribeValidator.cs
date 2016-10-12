using Cyrus.WebApi.ViewModels;
using FluentValidation;

namespace Cyrus.WebApi.ModelValidators
{
    public class CreateTribeValidator : AbstractValidator<CreateOrUpdateTribeViewModel>
    {
        public CreateTribeValidator()
        {
            RuleFor(x => x.Name).Length(0, 10);
        }
    }
}