using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretApi.Application.Features.CQRSDesignPattern.Commands.ProductCommands
{
    public class CreateProductCommand
    {
        public string ProductName { get; set; }
        public int ProductCategoryID { get; set; }
        public int ProductPrice { get; set; }
        public string ProductImageURL { get; set; }
    }
}
