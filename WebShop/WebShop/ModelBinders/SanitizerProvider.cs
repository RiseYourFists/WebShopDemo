using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebShop.App.ModelBinders
{
    public class SanitizerProvider : IModelBinderProvider
    {
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Metadata.ModelType == typeof(string))
            {
                return new SanitizerModelBinder();
            }

            return null;
        }
    }
}
