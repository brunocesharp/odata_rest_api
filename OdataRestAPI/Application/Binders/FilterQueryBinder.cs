using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace OdataRestAPI.Application.Binders
{
    public class FilterQueryBinder : IModelBinderProvider
    {
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.ModelType == typeof(OdataBinder))
            {
                return new OdataBinder();
            }

            return null;
        }
    }
}
