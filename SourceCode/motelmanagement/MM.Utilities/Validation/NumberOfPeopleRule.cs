using System.Globalization;
using System.Windows.Controls;

namespace MM.Utilities
{
    /// <summary>
    /// NumberOfPeopleRule
    /// </summary>
    public class NumberOfPeopleRule : ValidationRule
    {
        /// <summary>
        /// ValidationResult
        /// </summary>
        /// <param name="value"></param>
        /// <param name="cultureInfo"></param>
        /// <returns></returns>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int numOfPeopleToCheck;

            if (!int.TryParse((string)value, out numOfPeopleToCheck))
            {
                return new ValidationResult(false, "This field should be a number");
            }
            else if (numOfPeopleToCheck < 1)
            {
                return new ValidationResult(false, "Should not be smaller than 1");
            }
            else if (numOfPeopleToCheck > 10)
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
