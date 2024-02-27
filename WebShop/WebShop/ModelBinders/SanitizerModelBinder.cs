using Ganss.Xss;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebShop.App.ModelBinders
{
    public class SanitizerModelBinder : IModelBinder
    {
        private readonly HtmlSanitizer _sanitizer = new HtmlSanitizer();

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var valueProviderResult = bindingContext
                .ValueProvider
                .GetValue(bindingContext.ModelName);

            if (valueProviderResult == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }

            bindingContext
                .ModelState
                .SetModelValue(bindingContext.ModelName, valueProviderResult);

            var value = valueProviderResult.FirstValue;

            if (!string.IsNullOrWhiteSpace(value))
            {
                var sanitizedValue = _sanitizer.Sanitize(value);

                bindingContext.Result = ModelBindingResult.Success(sanitizedValue);
            }

            return Task.CompletedTask;
        }
    }
}
