using System.ComponentModel.DataAnnotations;

namespace TraineeHub.Web.Validation
{
    public class FutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is DateTime date)
            {
                return date.Date > DateTime.Today;
            }

            return false;
        }

        public override string FormatErrorMessage(string name)
        {
            return $"{name} must be a future date.";
        }
    }
}
