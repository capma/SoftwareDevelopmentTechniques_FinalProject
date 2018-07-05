using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace MM.Utilities
{
    /// <summary>
    /// PhoneNumberRule
    /// </summary>
    public class PhoneNumberRule : ValidationRule
    {        
        /// <summary>
        /// ValidationResult
        /// </summary>
        /// <param name="value"></param>
        /// <param name="cultureInfo"></param>
        /// <returns></returns>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Regex regexPhoneRule = new Regex(@"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}");

            if (regexPhoneRule.IsMatch(value.ToString()))
            {
                return ValidationResult.ValidResult;
            }
            else
            {
                return new ValidationResult(false, "Invalid Phone number. Should be 1234567890");
            }
        }
    }
}
