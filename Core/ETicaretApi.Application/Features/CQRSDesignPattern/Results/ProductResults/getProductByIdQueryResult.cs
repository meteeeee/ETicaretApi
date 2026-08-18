using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretApi.Application.Features.CQRSDesignPattern.Results.ProductResults
{
    public class getProductByIdQueryResult
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int ProductCategoryID { get; set; }
        public int ProductPrice { get; set; }
        public string ProductImageURL { get; set; }
    }
}
