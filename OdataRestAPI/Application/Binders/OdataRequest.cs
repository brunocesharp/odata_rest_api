using Microsoft.AspNetCore.Mvc;
using OdataRestAPI.Application.Odatas;

namespace OdataRestAPI.Application.Binders
{
    [ModelBinder(BinderType = typeof(OdataBinder))]
    public class OdataRequest
    {
        public string Selects { get; set; }
        public string Filters { get; set; }
        public string Orders { get; set; }

        //Paginação
        public int Skip { get; set; }
        public int Top { get; set; }
        public int Count { get; set; }
        public int PageCount { get; set; } = 0;
        public int TotalCount { get; set; } = 0;
        public int TotalPages { get; set; } = 0;

        public OdataRequest(string selects, string filters, string orders)
        {
            Selects = selects;
            Filters = filters;
            Orders = orders;
        }

        public Odata<TEntity> Odata<TEntity>(OdataRequest request)
        {
            return new Odata<TEntity>(request);
        }
    }
}
