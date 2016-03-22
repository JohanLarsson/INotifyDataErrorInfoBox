namespace ValidationBox
{
    using System.Globalization;
    using System.Windows.Controls;

    public class MustStartWithValidationRule : ValidationRule
    {
        public string StartsWith { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var text = value as string;
            if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(this.StartsWith))
            {
                return ValidationResult.ValidResult;
            }

            return !text.StartsWith(this.StartsWith)
                       ? new ValidationResult(false, $"Must start with {this.StartsWith}")
                       : ValidationResult.ValidResult;
        }
    }
}
