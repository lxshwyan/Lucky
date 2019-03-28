using CarManager.Web.Validator;
using FluentValidation.Mvc;
using Lucky.MVCTest.Mvc;
using Lucky.WebCore.IOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;  

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Lucky.MVCTest.App_Start.ExensionsActivator), "Start")]

namespace Lucky.MVCTest.App_Start
{
    public static class ExensionsActivator
    {
        public static void Start()
        {
            DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;
            UntiyValidatorFactory untiyValidatorFactory = new UntiyValidatorFactory(UnityConfig.GetConfiguredContainer());
            ModelValidatorProviders.Providers.Insert(0,new FluentValidationModelValidatorProvider(untiyValidatorFactory));
           // ModelMetadataProviders.Current = new CustomModelMetadataProvider();
        }
    }
}