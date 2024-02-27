namespace WebShop.App.ModelBinders
{
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    
    public class FloatingPointBinderProvider : IModelBinderProvider
    {
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Metadata.ModelType == typeof(float)
                || context.Metadata.ModelType == typeof(float?))
            {
                return new FloatingPointModelBinder<float>();
            }

            if (context.Metadata.ModelType == typeof(double)
                    || context.Metadata.ModelType == typeof(double?))
            {
                return new FloatingPointModelBinder<double>();
            }

            if (context.Metadata.ModelType == typeof(decimal)
                || context.Metadata.ModelType == typeof(decimal?))
            {
                return new FloatingPointModelBinder<decimal>();
            }

            return null;
        }
    }
}
