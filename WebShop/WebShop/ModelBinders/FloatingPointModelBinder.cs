namespace WebShop.App.ModelBinders
{
    using System.Globalization;
    using System.Text.RegularExpressions;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public class FloatingPointModelBinder<T> : IModelBinder
        where T : struct, IComparable, IFormattable, IConvertible
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (valueProviderResult == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }

            bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueProviderResult);

            var value = valueProviderResult.FirstValue;

            if (value != null && TryParseFloatingPoint(value, out T parsedValue))
            {
                bindingContext.Result = ModelBindingResult.Success(parsedValue);
            }
            else
            {
                bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, $"Invalid {typeof(T).Name} format");
            }

            return Task.CompletedTask;
        }

        private bool TryParseFloatingPoint<T>(string value, out T parsedValue)
        {
            parsedValue = default(T);

            value = NormalizeValue(value);

            var formats = new[]
            {
                NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowParentheses,
                NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowLeadingSign,
                NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands,
                NumberStyles.AllowCurrencySymbol,
                NumberStyles.Any
            };

            foreach (var format in formats)
            {
                if (typeof(T) == typeof(float) && float.TryParse(value, format, CultureInfo.InvariantCulture, out var floatValue))
                {
                    parsedValue = (T)(object)floatValue;
                    return true;
                }
                else if (typeof(T) == typeof(double) && double.TryParse(value, format, CultureInfo.InvariantCulture, out var doubleValue))
                {
                    parsedValue = (T)(object)doubleValue;
                    return true;
                }
                else if (typeof(T) == typeof(decimal) && decimal.TryParse(value, format, CultureInfo.InvariantCulture, out var decimalValue))
                {
                    parsedValue = (T)(object)decimalValue;
                    return true;
                }
                else if (typeof(T) == typeof(int) && int.TryParse(value, format, CultureInfo.InvariantCulture, out var intValue))
                {
                    parsedValue = (T)(object)intValue;
                    return true;
                }
            }

            return false;
        }

        private static string NormalizeValue(string value)
        {
            string dotSeparatorPattern = @"^(|-)\d+\.\d+$";
            string commaSeparatorPattern = @"^(|-)\d+\,\d+$";
            string dotInThousandsPattern = @"\.\d{0,3}\,\d+$";
            string commaInThousandsPattern = @"\,\d{0,3}\.\d+$";

            if (Regex.IsMatch(value, dotSeparatorPattern))
            {
                value = value.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
            }
            else if (Regex.IsMatch(value, commaSeparatorPattern))
            {
                value = value.Replace(",", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
            }
            else if (Regex.IsMatch(value, dotInThousandsPattern))
            {
                value = value.Replace(".", "");
                value = value.Replace(",", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
            }
            else if (Regex.IsMatch(value, commaInThousandsPattern))
            {
                value = value.Replace(",", "");
                value = value.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
            }

            return value;
        }
    }
}
