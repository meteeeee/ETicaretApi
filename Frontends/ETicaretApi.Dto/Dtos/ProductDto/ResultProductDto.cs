using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretApi.Dto.Dtos.ProductDtos
{
    public class ResultProductDto
    {
        public Guid ProductID { get; set; }
        public string ProductName { get; set; }
        public Guid ProductCategoryID { get; set; }
        public int ProductPrice { get; set; }
        public string ProductImageURL { get; set; }
    }
}
