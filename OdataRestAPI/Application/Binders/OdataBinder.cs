using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace OdataRestAPI.Application.Binders
{
    public class OdataBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var filterQuery = bindingContext.ValueProvider.GetValue("$filter").FirstValue;
            var orderByQuery = bindingContext.ValueProvider.GetValue("$orderby").FirstValue;
            var selectQuery = bindingContext.ValueProvider.GetValue("$select").FirstValue;

            bindingContext.Result = ModelBindingResult.Success(new OdataRequest(filterQuery, orderByQuery, selectQuery));
            return Task.CompletedTask;
        }
    }
}