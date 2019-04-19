using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace BeltExam{

    public class PasswordValidate :ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value is null)
            {
                return new ValidationResult("Password Required");
            }
            // DateTime dateTimeValue = (DateTime) value;
            // int result = DateTime.Compare(dateTimeValue, DateTime.Now);
            //if result is less than 0, the value is before present date.
            var input = (string)value;

            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMinimum8Chars = new Regex(@".{8,}");

            bool isValidated = hasNumber.IsMatch(input) && hasUpperChar.IsMatch(input) && hasMinimum8Chars.IsMatch(input);
            Console.WriteLine(isValidated);
            if(isValidated)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Password must be minimum 8 length and have 1 special character and upper character");
            }
        }
    }
}