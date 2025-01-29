using DapperLearning.Models;
using System.ComponentModel.DataAnnotations;

namespace DapperLearning.Attribute
{
    public class DesignationValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var designations = new List<string>
            {
                "SD",
                "Manager",
                "Hr",
                "Admin"
            };
            var designation = validationContext.ObjectInstance as Employee;

            if (designations == null)
            {
                return new ValidationResult("Please enter the designation");
            }
            //else if (designations.Select(x => x.ToLower()).Contains(designation.Designation.ToLower())) ;
            else if (!designations.Any(x => x.Equals(designation.Designation, StringComparison.CurrentCultureIgnoreCase)))
            {
                return new ValidationResult("Please enter valid  designation");

            }


            return ValidationResult.Success;
        }
    }
}
