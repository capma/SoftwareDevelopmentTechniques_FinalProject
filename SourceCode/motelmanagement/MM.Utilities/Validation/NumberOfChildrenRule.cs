using System.Globalization;
using System.Windows.Controls;

namespace MM.Utilities
{
    /// <summary>
    /// NumberOfChildrenRule
    /// </summary>
    public class NumberOfChildrenRule : ValidationRule
    {
        /// <summary>
        /// ValidationResult
        /// </summary>
        /// <param name="value"></param>
        /// <param name="cultureInfo"></param>
        /// <returns></returns>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int numOfChildrenToCheck;

            if (!int.TryParse((string)value, out numOfChildrenToCheck))
            {
                return new ValidationResult(false, "This field should be a number");
            }
            else if (numOfChildrenToCheck < 0)
            {
                return new ValidationResult(false, "It should not be less than 0");
            }
            else if (numOfChildrenToCheck > 10)
            {
                return new ValidationResult(false, "Should not be greater than 10");
            }
            else
            {
                return ValidationResult.ValidResult;
            }
        }
    }
}
