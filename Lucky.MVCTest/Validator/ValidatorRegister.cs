
using Lucky.Core.IOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unity;
using FluentValidation;
using FluentValidation.Mvc;
using Lucky.MVCTest.Properties;
using Unity.Lifetime;

namespace Lucky.MVCTest.Validator
{
    public class ValidatorRegister : IDependencyRegister
    {
        public void RegisterTypes(IUnityContainer container)
        {
            var validatorTypes = this.GetType().Assembly.GetTypes().Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IValidator<>)));

            //FluentValidation.Mvc.FluentValidationModelValidatorProvider.Configure();

            ValidatorOptions.ResourceProviderType = typeof(Resources);

            ValidatorOptions.DisplayNameResolver = (type, memberInfo, lambdaExpression) =>
            {
                string key = type.Name + memberInfo.Name + "DisplayName";
                string displayName = Resources.ResourceManager.GetString(key);

                return displayName;
            };

            foreach (Type type in validatorTypes)
            {
                container.RegisterType(typeof(IValidator<>),type,type.BaseType.GetGenericArguments().First().FullName,new ContainerControlledLifetimeManager());
            }
        }
    }
}