using System;
using System.Web.Http;
using FluentValidation;

namespace Cyrus.WebApi.App_Start
{
    public class FluentValidatorFactory : ValidatorFactoryBase
    {
        public override IValidator CreateInstance(Type validatorType)
        {
            // possible error using 'GlobalConfiguration.Configuration' with OWIN
            return GlobalConfiguration.Configuration.DependencyResolver.GetService(validatorType) as IValidator;
        }
    }
}