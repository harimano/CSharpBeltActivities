using System;
using System.ComponentModel.DataAnnotations;

namespace BeltExam{

    public class FutureDateAttribute :ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value is null)
            {
                return new ValidationResult("Date Required");
            }
            DateTime dateTimeValue = (DateTime) value;
            int result = DateTime.Compare(dateTimeValue, DateTime.Now);
            //if result is less than 0, the value is before present date.
            if(result >0)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Date must be in the future");
            }
        }
    }
}