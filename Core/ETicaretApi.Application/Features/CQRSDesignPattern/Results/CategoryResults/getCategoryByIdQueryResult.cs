using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretApi.Application.Features.CQRSDesignPattern.Results.CategoryResults
{
    public class getCategoryByIdQueryResult
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
    }
}
