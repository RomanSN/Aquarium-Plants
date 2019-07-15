using AquaMarket.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace AquaMarket.Validators
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class FileExistsAttribute : ValidationAttribute
    {
        private const string _DefaultErrorMessage = "File with name: {0} alredy exists.";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            IEnumerable<HttpPostedFileBase> files = value as IEnumerable<HttpPostedFileBase>;
            PlantRepo repo = new PlantRepo();

            if (files != null)
            {
                foreach (HttpPostedFileBase file in files)
                {
                    if (file != null && repo.CheckFileName(file)==true)
                    {
                        ErrorMessage = string.Format(_DefaultErrorMessage, file.FileName);
                        return new ValidationResult(ErrorMessageString);
                    }
                }
            }
            return ValidationResult.Success;
        }
    }
}