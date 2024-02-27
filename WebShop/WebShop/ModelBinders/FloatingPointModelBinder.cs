using System.Globalization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebShop.App.ModelBinders
{
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

            if (value != null && TryParseFloatingPoint(value, out var parsedValue))
            {
                var formattedValue = parsedValue.ToString("0.00", CultureInfo.InvariantCulture);

                bindingContext.Result = ModelBindingResult.Success(formattedValue);
            }
            else
            {
                bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, $"Invalid {typeof(T).Name} format");
            }

            return Task.CompletedTask;
        }

        private bool TryParseFloatingPoint(string value, out T parsedValue)
        {
            parsedValue = default(T);

            var formats = new[]
            {
                NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands,
                NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowLeadingSign,
                NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowParentheses,
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
            }

            return false;
        }
    }
}
