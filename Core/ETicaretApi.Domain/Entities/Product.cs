using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretApi.Domain.Entities
{
    public class Product
    {
        public Guid ProductID { get; set; }
        public string ProductName { get; set; }
        public Guid ProductCategoryID { get; set; }

        [ForeignKey("ProductCategoryID")]
        public Category Category { get; set; }

        public int ProductPrice { get; set; }
        public string ProductImageURL { get; set; }
    }
}
