using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretApi.Application.Features.CQRSDesignPattern.Queries.ProductQueries
{
    public class getProductByIdQuery
    {
        public getProductByIdQuery(int productID)
        {
            ProductID = productID;
        }

        public int ProductID { get; set; }
    }
}
